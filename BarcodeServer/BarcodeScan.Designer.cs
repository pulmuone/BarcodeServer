namespace BarcodeServer
{
    partial class BarcodeScan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.InvoiceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvInvoiceItems = new System.Windows.Forms.DataGridView();
            this.InvoiceLineId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceId2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifyDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScanDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceItems)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dtpTo);
            this.splitContainer1.Panel1.Controls.Add(this.dtpFrom);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1002, 701);
            this.splitContainer1.SplitterDistance = 72;
            this.splitContainer1.TabIndex = 0;
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(158, 18);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(100, 21);
            this.dtpTo.TabIndex = 1;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(24, 18);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(100, 21);
            this.dtpFrom.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dgvInvoices);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvInvoiceItems);
            this.splitContainer2.Size = new System.Drawing.Size(1002, 625);
            this.splitContainer2.SplitterDistance = 497;
            this.splitContainer2.TabIndex = 0;
            // 
            // dgvInvoices
            // 
            this.dgvInvoices.AllowUserToDeleteRows = false;
            this.dgvInvoices.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInvoices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InvoiceId,
            this.InvoiceDate,
            this.InvoiceTitle,
            this.CreateDate});
            this.dgvInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInvoices.Location = new System.Drawing.Point(0, 0);
            this.dgvInvoices.MultiSelect = false;
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.RowTemplate.Height = 23;
            this.dgvInvoices.Size = new System.Drawing.Size(497, 625);
            this.dgvInvoices.TabIndex = 0;
            this.dgvInvoices.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInvoices_CellClick);
            this.dgvInvoices.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInvoices_CellEndEdit);
            // 
            // InvoiceId
            // 
            this.InvoiceId.DataPropertyName = "InvoiceId";
            this.InvoiceId.HeaderText = "InvoiceId";
            this.InvoiceId.Name = "InvoiceId";
            this.InvoiceId.ReadOnly = true;
            this.InvoiceId.Width = 50;
            // 
            // InvoiceDate
            // 
            this.InvoiceDate.DataPropertyName = "InvoiceDate";
            this.InvoiceDate.HeaderText = "전표일자";
            this.InvoiceDate.Name = "InvoiceDate";
            this.InvoiceDate.ReadOnly = true;
            // 
            // InvoiceTitle
            // 
            this.InvoiceTitle.DataPropertyName = "InvoiceTitle";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.InvoiceTitle.DefaultCellStyle = dataGridViewCellStyle2;
            this.InvoiceTitle.HeaderText = "제목";
            this.InvoiceTitle.Name = "InvoiceTitle";
            this.InvoiceTitle.Width = 200;
            // 
            // CreateDate
            // 
            this.CreateDate.DataPropertyName = "CreateDate";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.CreateDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.CreateDate.HeaderText = "생성일시";
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.ReadOnly = true;
            this.CreateDate.Width = 200;
            // 
            // dgvInvoiceItems
            // 
            this.dgvInvoiceItems.AllowUserToDeleteRows = false;
            this.dgvInvoiceItems.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Gulim", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInvoiceItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvInvoiceItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoiceItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InvoiceLineId,
            this.InvoiceId2,
            this.ItemId,
            this.ItemNm,
            this.OrderQty,
            this.ScanQty,
            this.CreateDate2,
            this.ModifyDate,
            this.ScanDate});
            this.dgvInvoiceItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInvoiceItems.Enabled = false;
            this.dgvInvoiceItems.Location = new System.Drawing.Point(0, 0);
            this.dgvInvoiceItems.Name = "dgvInvoiceItems";
            this.dgvInvoiceItems.RowTemplate.Height = 23;
            this.dgvInvoiceItems.Size = new System.Drawing.Size(501, 625);
            this.dgvInvoiceItems.TabIndex = 0;
            this.dgvInvoiceItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInvoiceItems_CellEndEdit);
            // 
            // InvoiceLineId
            // 
            this.InvoiceLineId.DataPropertyName = "InvoiceLineId";
            this.InvoiceLineId.HeaderText = "InvoiceLineId";
            this.InvoiceLineId.Name = "InvoiceLineId";
            this.InvoiceLineId.ReadOnly = true;
            // 
            // InvoiceId2
            // 
            this.InvoiceId2.DataPropertyName = "InvoiceId";
            this.InvoiceId2.HeaderText = "InvoiceId";
            this.InvoiceId2.Name = "InvoiceId2";
            this.InvoiceId2.ReadOnly = true;
            // 
            // ItemId
            // 
            this.ItemId.DataPropertyName = "ItemId";
            this.ItemId.HeaderText = "상품코드";
            this.ItemId.Name = "ItemId";
            // 
            // ItemNm
            // 
            this.ItemNm.DataPropertyName = "ItemNm";
            this.ItemNm.HeaderText = "상품명";
            this.ItemNm.Name = "ItemNm";
            // 
            // OrderQty
            // 
            this.OrderQty.DataPropertyName = "OrderQty";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = "0";
            this.OrderQty.DefaultCellStyle = dataGridViewCellStyle5;
            this.OrderQty.HeaderText = "주문수량";
            this.OrderQty.Name = "OrderQty";
            // 
            // ScanQty
            // 
            this.ScanQty.DataPropertyName = "ScanQty";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = "0";
            this.ScanQty.DefaultCellStyle = dataGridViewCellStyle6;
            this.ScanQty.HeaderText = "스캔수량";
            this.ScanQty.Name = "ScanQty";
            // 
            // CreateDate2
            // 
            this.CreateDate2.DataPropertyName = "CreateDate";
            this.CreateDate2.HeaderText = "입력시간";
            this.CreateDate2.Name = "CreateDate2";
            this.CreateDate2.ReadOnly = true;
            // 
            // ModifyDate
            // 
            this.ModifyDate.DataPropertyName = "ModifyDate";
            this.ModifyDate.HeaderText = "수정시간";
            this.ModifyDate.Name = "ModifyDate";
            this.ModifyDate.ReadOnly = true;
            // 
            // ScanDate
            // 
            this.ScanDate.DataPropertyName = "ScanDate";
            this.ScanDate.HeaderText = "스캔시간";
            this.ScanDate.Name = "ScanDate";
            this.ScanDate.ReadOnly = true;
            // 
            // BarcodeScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 701);
            this.Controls.Add(this.splitContainer1);
            this.Name = "BarcodeScan";
            this.Text = "BarcodeScan";
            this.Load += new System.EventHandler(this.BarcodeScan_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dgvInvoices;
        private System.Windows.Forms.DataGridView dgvInvoiceItems;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceLineId;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceId2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNm;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScanQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifyDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScanDate;
    }
}