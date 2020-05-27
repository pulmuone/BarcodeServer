using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeServer.Models
{
    public class InvoiceItemModel
    {
        public int InvoiceLineId { get; set; }
        public int InvoiceId { get; set; }
        public string ItemId {get; set;}
        public string ItemNm { get; set; }
        public int OrderQty { get; set; }
        public int ScanQty { get; set; }
        public string CreateDate { get; set; }
        public string ModifyDate { get; set; }
        public string ScanDate { get; set; }
        
    }
}
