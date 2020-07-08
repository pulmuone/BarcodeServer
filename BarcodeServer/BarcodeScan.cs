using BarcodeServer.Helper;
using BarcodeServer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace BarcodeServer
{
    public partial class BarcodeScan : Form, ITopButton
    {
        private int _activeInvoiceId = 0;

        public BarcodeScan()
        {
            InitializeComponent();
        }


        private void BarcodeScan_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = System.DateTime.Today;
            dtpTo.Value = System.DateTime.Today;

            Search();
        }

        public void Download()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Search()
        {
            try
            {
                dgvInvoiceItems.Enabled = false;

                int rowCount = dgvInvoiceItems.Rows.Count;
                for (int n = 0; n < rowCount; n++)
                {
                    if (dgvInvoiceItems.Rows[0].IsNewRow == false)
                        dgvInvoiceItems.Rows.RemoveAt(0);
                }

                DataTable dt = LocalDB.Instance.InvoiceSearch(dtpFrom.Value.ToString("yyyy-MM-dd"), dtpTo.Value.ToString("yyyy-MM-dd"));
                Console.WriteLine(dt.Rows.Count);

                dgvInvoices.DataSource = dt;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void Upload()
        {
            throw new NotImplementedException();
        }

        private void dgvInvoices_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string invoice_date = string.Empty;

            Console.WriteLine("dgvInvoices_CellEndEdit " + e.ColumnIndex);
            Console.WriteLine("dgvInvoices_CellEndEdit " + e.RowIndex);
            Console.WriteLine(dgvInvoices.Rows[e.RowIndex].Cells["InvoiceTitle"].Value);

            invoice_date = dgvInvoices.Rows[e.RowIndex].Cells["InvoiceDate"].Value?.ToString();
            Console.WriteLine(invoice_date);
            if(string.IsNullOrEmpty(invoice_date))
            {
                InvoiceModel invoiceModel = new InvoiceModel();
                invoiceModel.InvoiceTitle = dgvInvoices.Rows[e.RowIndex].Cells["InvoiceTitle"].Value?.ToString();
                invoiceModel.InvoiceDate = DateTime.Now.ToString("yyyy-MM-dd");
                invoiceModel.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                dgvInvoices.Rows[e.RowIndex].Cells["InvoiceDate"].Value = invoiceModel.InvoiceDate;
                dgvInvoices.Rows[e.RowIndex].Cells["CreateDate"].Value = invoiceModel.CreateDate;

                if (!string.IsNullOrEmpty(invoiceModel.InvoiceTitle))
                {
                    string invoice_id = LocalDB.Instance.InvoiceInsert(invoiceModel);
                    //Search();
                    dgvInvoices.Rows[e.RowIndex].Cells["InvoiceId"].Value = invoice_id;
                    dgvInvoices.Rows[e.RowIndex].Cells["InvoiceDate"].Value = invoiceModel.InvoiceDate;
                    dgvInvoices.Rows[e.RowIndex].Cells["CreateDate"].Value = invoiceModel.CreateDate;
                }
            }
            else
            {
                InvoiceModel invoiceModel = new InvoiceModel();
                invoiceModel.InvoiceId = Convert.ToInt32(dgvInvoices.Rows[e.RowIndex].Cells["InvoiceId"].Value);
                invoiceModel.InvoiceTitle = dgvInvoices.Rows[e.RowIndex].Cells["InvoiceTitle"].Value.ToString();
                invoiceModel.InvoiceDate = dgvInvoices.Rows[e.RowIndex].Cells["InvoiceDate"].Value.ToString();
                invoiceModel.CreateDate = dgvInvoices.Rows[e.RowIndex].Cells["CreateDate"].Value.ToString();

                LocalDB.Instance.InvoiceUpdate(invoiceModel);
                //Search();
                dgvInvoices.Rows[e.RowIndex].Cells["InvoiceTitle"].Value = invoiceModel.InvoiceTitle;

            }
        }

        private void dgvInvoiceItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            InvoiceItemModel itemModel = new InvoiceItemModel();

            if (string.IsNullOrEmpty(dgvInvoiceItems.Rows[e.RowIndex].Cells["InvoiceLineId"].Value?.ToString()))
            {
                itemModel.InvoiceLineId = 0;
            }
            else
            {
                if (string.IsNullOrEmpty(dgvInvoiceItems.Rows[e.RowIndex].Cells["CreateDate2"].Value?.ToString()))
                {
                    itemModel.InvoiceLineId = 0;
                }
                else
                {
                    itemModel.InvoiceLineId = Convert.ToInt32(dgvInvoiceItems.Rows[e.RowIndex].Cells["InvoiceLineId"].Value);
                }
            }

            if (string.IsNullOrEmpty(dgvInvoiceItems.Rows[e.RowIndex].Cells["InvoiceId2"].Value?.ToString()))
            {
                itemModel.InvoiceId = _activeInvoiceId;
                dgvInvoiceItems.Rows[e.RowIndex].Cells["InvoiceId2"].Value = _activeInvoiceId;
            }
            else
            {
                itemModel.InvoiceId = Convert.ToInt32(dgvInvoiceItems.Rows[e.RowIndex].Cells["InvoiceId2"].Value);
            }

            itemModel.ItemId = dgvInvoiceItems.Rows[e.RowIndex].Cells["ItemId"].Value?.ToString();
            itemModel.ItemNm = dgvInvoiceItems.Rows[e.RowIndex].Cells["ItemNm"].Value?.ToString();

            if (string.IsNullOrEmpty(itemModel.ItemId))
            {
                dgvInvoiceItems.Rows[e.RowIndex].Cells["ItemId"].Value = string.Empty;
            }

            if (string.IsNullOrEmpty(itemModel.ItemNm))
            {
                dgvInvoiceItems.Rows[e.RowIndex].Cells["ItemNm"].Value = string.Empty;
            }

            if (string.IsNullOrEmpty(dgvInvoiceItems.Rows[e.RowIndex].Cells["OrderQty"].Value?.ToString()))
            {
                itemModel.OrderQty = 0;
                dgvInvoiceItems.Rows[e.RowIndex].Cells["OrderQty"].Value = 0;
            }
            else
            {
                itemModel.OrderQty = Convert.ToInt32(dgvInvoiceItems.Rows[e.RowIndex].Cells["OrderQty"].Value?.ToString());
            }

            if (string.IsNullOrEmpty(dgvInvoiceItems.Rows[e.RowIndex].Cells["ScanQty"].Value?.ToString()))
            {
                itemModel.ScanQty = 0;
                dgvInvoiceItems.Rows[e.RowIndex].Cells["ScanQty"].Value = 0;
            }
            else
            {
                itemModel.ScanQty = Convert.ToInt32(dgvInvoiceItems.Rows[e.RowIndex].Cells["ScanQty"].Value?.ToString());
            }
                        
            itemModel.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            itemModel.ModifyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (!string.IsNullOrEmpty(itemModel.ItemId) && !string.IsNullOrEmpty(itemModel.ItemNm))
            {
                //List<InvoiceItemModel> lst = new List<InvoiceItemModel>();
                //lst.Add(itemModel);

                if (itemModel.InvoiceLineId == 0) //Insert
                {
                    var row = LocalDB.Instance.InvoiceItemInsert(_activeInvoiceId, itemModel);

                    //수정의 기준이 되는 입력일자 화면에 업데이트 해준다.
                    dgvInvoiceItems.Rows[e.RowIndex].Cells["InvoiceLineId"].Value = row;
                    dgvInvoiceItems.Rows[e.RowIndex].Cells["CreateDate2"].Value = itemModel.CreateDate;
                }
                else //Update
                {
                    LocalDB.Instance.InvoiceItemUpdate(itemModel);
                }

                //DataTable dt = LocalDB.Instance.InvoiceItemSearch(_activeInvoiceId);
                //Console.WriteLine(dt.Rows.Count);

                //this.BeginInvoke(new MethodInvoker(() =>
                //{
                //    dgvInvoiceItems.DataSource = dt;
                //}));
            }
        }

        private void dgvInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine("dgvInvoices_CellClick " + e.RowIndex);
            Console.WriteLine("dgvInvoices_CellClick " + e.ColumnIndex);

            string invoice_date = string.Empty;

            if (e.RowIndex >= 0)
            {
                if (dgvInvoices.Rows[e.RowIndex].Cells["InvoiceId"].Value == null)
                {
                    _activeInvoiceId = 0;
                }
                else
                {
                    //invoice_date = dgvInvoices.Rows[e.RowIndex].Cells["InvoiceDate"].Value?.ToString();
                    if (dgvInvoices.Rows[e.RowIndex].Cells["InvoiceDate"].Value != null)
                    {
                        _activeInvoiceId = Convert.ToInt32(dgvInvoices.Rows[e.RowIndex].Cells["InvoiceId"].Value);

                        DataTable dt = LocalDB.Instance.InvoiceItemSearch(_activeInvoiceId);
                        Console.WriteLine(dt.Rows.Count);

                        dgvInvoiceItems.DataSource = dt;
                        dgvInvoiceItems.Enabled = true;
                    }
                }
            }
            else
            {
                _activeInvoiceId = 0;
            }
        }

        private void dgvInvoices_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInvoices.SelectedRows.Count > 0)
            {
                Console.WriteLine("dgvInvoices_SelectionChanged " + dgvInvoices.SelectedRows[0].Cells["InvoiceId"].Value);
                string invoice_date = string.Empty;
                
                if (dgvInvoices.SelectedRows[0].Cells["InvoiceId"].Value == null)
                {
                    _activeInvoiceId = 0;
                }
                else
                {
                    invoice_date = dgvInvoices.SelectedRows[0].Cells["InvoiceDate"].Value?.ToString();
                    if (!string.IsNullOrEmpty(invoice_date))
                    {
                        _activeInvoiceId = Convert.ToInt32(dgvInvoices.SelectedRows[0].Cells["InvoiceId"].Value);

                        DataTable dt = LocalDB.Instance.InvoiceItemSearch(_activeInvoiceId);
                        Console.WriteLine(dt.Rows.Count);

                        dgvInvoiceItems.DataSource = dt;
                        dgvInvoiceItems.Enabled = true;
                    }
                }
            }
        }
    }
}
