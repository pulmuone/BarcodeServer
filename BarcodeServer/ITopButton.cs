using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeServer
{
    public interface ITopButton
    {
        void Search();
        void Save();
        void Download();
//        void Print();
        void Close();
        void Upload();
    }
}
