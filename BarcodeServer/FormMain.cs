using BarcodeServer.Services;
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
    public partial class FormMain : Form
    {
        private int childFormNumber = 0;

        public FormMain()
        {
            InitializeComponent();

            //Task.Factory.StartNew(() => AsyncSocketListener.StartListening());
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void prodMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            subNewMdiChildren(new ProdMaster(), "상품 마스터");
        }
        private void scanListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            subNewMdiChildren(new BarcodeScan(), "바코드 스캔");
        }

        public void subNewMdiChildren(Form childForm, string frmText)
        {
            //if (Login._login_menu_opt.ToString() == "S") //싱글창 조건일 경우
            //{
            // 같은 메뉴가 이미 떠 있으면 띄우지 않는다.
            foreach (Form ChildFormList in this.MdiChildren)
            {
                if (ChildFormList.Name == childForm.Name)
                {
                    ChildFormList.Activate();
                    return;
                }
            }
            //}

            Form newChildFrm = childForm;

            //newChildFrm.Text = childForm.Name + "-" + frmText;
            newChildFrm.Text = frmText;
            newChildFrm.MdiParent = this;
            newChildFrm.WindowState = FormWindowState.Maximized;
            newChildFrm.Show();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Text = "Ver. " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            tabControl.Visible = false;

        }

        private void FormMain_MdiChildActivate(object sender, EventArgs e)
        {

            Form activeChild = (Form)this.ActiveMdiChild;

            if (activeChild == null)
            {
                tabControl.Visible = false;
            }
            else
            {
                if (this.ActiveMdiChild.Tag == null)
                {
                    // Add a tabPage to tabControl with child form caption
                    TabPage tp = new TabPage(this.ActiveMdiChild.Text);
                    tp.Tag = this.ActiveMdiChild;
                    tp.Parent = tabControl;
                    tabControl.SelectedTab = tp;

                    this.ActiveMdiChild.Tag = tp;
                    this.ActiveMdiChild.FormClosed += new FormClosedEventHandler(ActiveMdiChild_FormClosed);
                }
                else
                {
                    tabControl.SelectedTab = (TabPage)this.ActiveMdiChild.Tag;
                }

                if (!tabControl.Visible)
                    tabControl.Visible = true;
            }
        }

        void ActiveMdiChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((sender as Form).Tag as TabPage).Dispose();
        }


        private void Upload(object sender, EventArgs e)
        {

            object frmChild = this.ActiveMdiChild;

            if ((frmChild != null))
            {
                (frmChild as ITopButton).Upload();
            }
        }

        private void Download(object sender, EventArgs e)
        {
            object frmChild = this.ActiveMdiChild;

            if ((frmChild != null))
            {
                (frmChild as ITopButton).Download();
            }
        }

        private void Save(object sender, EventArgs e)
        {
            object frmChild = this.ActiveMdiChild;

            if ((frmChild != null))
            {
                (frmChild as ITopButton).Save();
            }
        }

        private void Search(object sender, EventArgs e)
        {
            object frmChild = this.ActiveMdiChild;

            if ((frmChild != null))
            {
                (frmChild as ITopButton).Search();
            }
        }
        private void Close(object sender, EventArgs e)
        {
            object frmChild = this.ActiveMdiChild;

            if ((frmChild != null))
            {
                (frmChild as ITopButton).Close();
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2 || e.KeyCode == Keys.F4 || e.KeyCode == Keys.F8 || e.KeyCode == Keys.F9 || e.KeyCode == Keys.F12)
            {
                switch (e.KeyCode)
                {
                    case Keys.F2: //Search
                        e.Handled = true;
                        searchToolStripButton.PerformClick();
                        break;
                    case Keys.F4: //Close
                        e.Handled = true;
                        closeToolStripButton.PerformClick();
                        break;
                    //case Keys.F8: //Print
                    //    e.Handled = true;
                    //    //toolStripBtnPrint.PerformClick();
                    //    break;
                    case Keys.F8: //Upload
                        e.Handled = true;
                        uploadToolStripButton.PerformClick();
                        break;
                    case Keys.F9: //Download
                        e.Handled = true;
                        downloadToolStripButton.PerformClick();
                        break;
                    case Keys.F12: //Save
                        e.Handled = true;
                        saveToolStripButton.PerformClick();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
