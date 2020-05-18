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
            throw new NotImplementedException();
        }

        public void Upload()
        {
            throw new NotImplementedException();
        }
    }
}
