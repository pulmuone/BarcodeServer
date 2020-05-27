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
        private int _activeInvoiceId = 0;

        public BarcodeScan()
        {
            InitializeComponent();
        }


        private void BarcodeScan_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = System.DateTime.Today;
            dtpTo.Value = System.DateTime.Today;

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
            DataTable dt = LocalDB.Instance.InvoiceSearch(dtpFrom.Value.ToString("yyyy-MM-dd"), dtpTo.Value.ToString("yyyy-MM-dd"));
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

            invoice_date = dgvInvoices.Rows[e.RowIndex].Cells["InvoiceDate"].Value?.ToString();

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

                dgvInvoices.Rows[e.RowIndex].Cells["InvoiceTitle"].Value = invoiceModel.InvoiceTitle;
               
            }
        }

        private void dgvInvoiceItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //dgvInvoices.SelectedRows
            InvoiceItemModel itemModel = new InvoiceItemModel();

            if (string.IsNullOrEmpty(dgvInvoiceItems.Rows[e.RowIndex].Cells["InvoiceLineId"].Value?.ToString()))
            {
                itemModel.InvoiceLineId = 0;
            }
            else
            {
                if (string.IsNullOrEmpty(dgvInvoiceItems.Rows[e.RowIndex].Cells["ScanDate"].Value?.ToString()))
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

            
            itemModel.CreateDate = DateTime.Now.ToString("yyyy-MM-dd");
            itemModel.ModifyDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if(!string.IsNullOrEmpty(itemModel.ItemId) && !string.IsNullOrEmpty(itemModel.ItemNm))
            {
                List<InvoiceItemModel> lst = new List<InvoiceItemModel>();
                lst.Add(itemModel);

                if (itemModel.InvoiceLineId == 0) //Insert
                {
                    LocalDB.Instance.InvoiceItemInsert(_activeInvoiceId, lst);
                }
                else //Update
                {
                    LocalDB.Instance.InvoiceItemUpdate(lst);
                }
            }


            /*
            foreach (DataGridViewRow row in dgvInvoiceItems.Rows)
            {
                Console.WriteLine(row.Cells["InvoiceLineId"].Value?.ToString());
                Console.WriteLine(row.Cells["InvoiceId2"].Value?.ToString());
                Console.WriteLine(row.Cells["ItemId"].Value?.ToString());
                Console.WriteLine(row.Cells["ItemNm"].Value?.ToString());
                Console.WriteLine(row.Cells["OrderQty"].Value?.ToString());
                Console.WriteLine(row.Cells["ScanQty"].Value?.ToString());

                itemModel.InvoiceLineId = row.Cells["InvoiceLineId"].Value == null ? 0 : Convert.ToInt32(row.Cells["InvoiceLineId"].Value);
                itemModel.InvoiceId = row.Cells["InvoiceId"].Value == null ? _activeInvoiceId : Convert.ToInt32(row.Cells["InvoiceId"].Value);
                itemModel.ItemId = row.Cells["ItemId"].Value?.ToString();
                itemModel.ItemNm = row.Cells["ItemNm"].Value?.ToString();
                itemModel.OrderQty = row.Cells["OrderQty"].Value == null ? 0 : Convert.ToInt32(row.Cells["OrderQty"].Value);
                itemModel.ScanQty = row.Cells["ScanQty"].Value == null ? 0 : Convert.ToInt32(row.Cells["ScanQty"].Value);

                if(itemModel.InvoiceLineId == 0)
                {
                    //Insert


                }
                else
                {
                    //Update

                }
            }
            */
        }

        private void dgvInvoices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Console.WriteLine(e.RowIndex);
            Console.WriteLine(e.ColumnIndex);

            dgvInvoiceItems.Enabled = true;

            string invoice_date = string.Empty;

            if (e.RowIndex >= 0)
            {
                if (dgvInvoices.Rows[e.RowIndex].Cells["InvoiceId"].Value == null)
                {
                    _activeInvoiceId = 0;
                }
                else
                {
                    invoice_date = dgvInvoices.Rows[e.RowIndex].Cells["InvoiceDate"].Value?.ToString();
                    if (!string.IsNullOrEmpty(invoice_date))
                    {
                        _activeInvoiceId = Convert.ToInt32(dgvInvoices.Rows[e.RowIndex].Cells["InvoiceId"].Value);

                        DataTable dt = LocalDB.Instance.InvoiceItemSearch(_activeInvoiceId);
                        Console.WriteLine(dt.Rows.Count);

                        dgvInvoiceItems.DataSource = dt;
                    }
                }
            }
            else
            {
                _activeInvoiceId = 0;
            }
        }
    }
}
