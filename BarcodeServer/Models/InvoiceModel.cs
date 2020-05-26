using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeServer.Models
{
    public class InvoiceModel
    {
        public int InvoiceId { get; set; }
        public string InvoiceDate { get; set; }
        public string InvoiceTitle { get; set; }
        public string CreateDate { get; set; }
    }
}
