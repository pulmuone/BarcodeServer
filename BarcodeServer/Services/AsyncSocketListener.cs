using BarcodeServer.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarcodeServer.Services
{
    public class StateObject
    {
        // Client  socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();

        public List<byte[]> listBuffer = new List<byte[]>();
    }

    public class AsyncSocketListener
    {
        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public AsyncSocketListener()
        {

        }

        public static void StartListening()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 7000);
            Socket listener = new Socket(SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100); //동시 접속자 Client

                while (true)
                {
                    allDone.Reset();

                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

                    allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();

            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;
            bool isContain;

            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                state.sb.Append(Encoding.Unicode.GetString(state.buffer, 0, bytesRead));
                content = state.sb.ToString();

                if (content.IndexOf("<EOF>") > -1)
                //if (content.IndexOf("\n  }\n]") > -1) //<EOF>
                //if (content.IndexOf("}]") > -1) //<EOF>
                {
                    Console.WriteLine("Text received : {0}", content);

                    Dictionary<string, string> pObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

                    if(pObj.ContainsKey("CMD"))
                    {
                        var cmd = pObj["CMD"].ToString();

                        if(cmd == "PULL")
                        {
                            if (pObj.ContainsKey("SCANDATE"))
                            {
                                var scanDate = pObj["SCANDATE"].ToString();
                                //ToDo
                                //헤더와 상세 데이터를 모바일로 전송한다.

                                DataTable dt = LocalDB.Instance.InvoiceSearch(scanDate, scanDate);
                                string jsonHeader = JsonConvert.SerializeObject(dt, Formatting.Indented);
                            }
                        }
                    }

                    foreach (var item in pObj.Keys)
                    {
                        Console.WriteLine(item.ToString()); //key
                        Console.WriteLine(pObj[item]);  //value

                        if(item.ToString().Equals("CMD"))
                        {
                            var cmd = pObj[item].ToString();

                            if(cmd == "PULL")
                            {
                                var scanDate = "";
                            }
                            else if(cmd == "PUSH")
                            {

                            }
                        }

                        //if (item.ToString().Equals("P_JSON"))
                        //{
                        //    List<EmpModel> lst = new List<EmpModel>();
                        //    lst = JsonConvert.DeserializeObject<List<EmpModel>>(pObj[item]);

                        //    StringBuilder sb = new StringBuilder();
                        //    foreach (var row in lst)
                        //    {
                        //        string binaryString = string.Empty;

                        //        //Console.WriteLine(string.Format("{0}, {1}",item.EmpId, ByteToString(item.EmpName)));
                        //        Console.WriteLine(string.Format("{0}, {1}", row.EmpId, row.EmpName));
                        //        sb.AppendLine(string.Format("{0}, {1}", row.EmpId, row.EmpName));
                        //    }

                        //    //File.WriteAllText("test.txt", sb.ToString());
                        //}
                    }

                    Send(handler, content);
                    //Send(handler, "OK");
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private static void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.Unicode.GetBytes(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
