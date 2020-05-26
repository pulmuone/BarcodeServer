using BarcodeServer.Helper;
using BarcodeServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarcodeServer
{
    public partial class BarcodeScan : Form, ITopButton
    {
        public BarcodeScan()
        {
            InitializeComponent();
        }


        private void BarcodeScan_Load(object sender, EventArgs e)
        {

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
            DataTable dt = LocalDB.Instance.InvoiceSearch("2020-05-26", "2020-05-26");
            Console.WriteLine(dt.Rows.Count);

            dgvInvoices.DataSource = dt;
        }

        public void Upload()
        {
            throw new NotImplementedException();
        }

        private void dgvInvoices_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string invoice_date = string.Empty;

            Console.WriteLine(e.ColumnIndex);
            Console.WriteLine(e.RowIndex);
            Console.WriteLine(dgvInvoices.Rows[e.RowIndex].Cells["InvoiceTitle"].Value);

            invoice_date = dgvInvoices.Rows[e.RowIndex].Cells["InvoiceDate"].Value.ToString();

            if(string.IsNullOrEmpty(invoice_date))
            {
                InvoiceModel invoiceModel = new InvoiceModel();
                invoiceModel.InvoiceTitle = dgvInvoices.Rows[e.RowIndex].Cells["InvoiceTitle"].Value.ToString();
                invoiceModel.InvoiceDate = DateTime.Now.ToString("yyyy-MM-dd");
                invoiceModel.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                
                string invoice_id = LocalDB.Instance.InvoiceInsert(invoiceModel);

                dgvInvoices.Rows[e.RowIndex].Cells["InvoiceId"].Value = invoice_id;
                dgvInvoices.Rows[e.RowIndex].Cells["InvoiceDate"].Value = invoiceModel.InvoiceDate;
                dgvInvoices.Rows[e.RowIndex].Cells["CreateDate"].Value = invoiceModel.CreateDate;
            }
            else
            {
                InvoiceModel invoiceModel = new InvoiceModel();
                invoiceModel.InvoiceId = Convert.ToInt32(dgvInvoices.Rows[e.RowIndex].Cells["InvoiceId"].Value);
                invoiceModel.InvoiceTitle = dgvInvoices.Rows[e.RowIndex].Cells["InvoiceTitle"].Value.ToString();
                invoiceModel.InvoiceDate = dgvInvoices.Rows[e.RowIndex].Cells["InvoiceDate"].Value.ToString();
                invoiceModel.CreateDate = dgvInvoices.Rows[e.RowIndex].Cells["CreateDate"].Value.ToString();

                LocalDB.Instance.InvoiceUpdate(invoiceModel);

                dgvInvoices.Rows[e.RowIndex].Cells["InvoiceTitle"].Value = invoiceModel.InvoiceTitle;
               
            }
        }
    }
}
