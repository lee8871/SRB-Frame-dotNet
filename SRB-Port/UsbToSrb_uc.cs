using System;
using System.Drawing;
using System.Windows.Forms;

namespace SRB.port
{
    public partial class UsbToSrb_uc : UserControl
    {
        private UsbToSrb backstage;
        public UsbToSrb_uc(UsbToSrb p)
        {
            InitializeComponent();
            backstage = p;
            comSelectCB.Click += new EventHandler(comSelectCB_Click);
            // comSelectCB.TextChanged += new EventHandler(comSelectCB_TextChanged);
            comSelectCB.SelectedIndexChanged += new EventHandler(comSelectCB_TextChanged);
            getUartTable();
            this.comSelectCB.Text = backstage.getPortName();
            this.EnterNameTB.KeyDown += EnterNameTB_KeyDown;
            setPortState();
        }

        private void setPortState()
        {
            if (backstage.Is_opened)
            {
                this.comSelectCB.BackColor = Color.LightGreen;
                renameBT.Enabled = true;
            }
            else
            {
                this.comSelectCB.BackColor = Color.LightPink;
                renameBT.Enabled = false;
            }
        }

        public void getUartTable()
        {
            comSelectCB.Items.Clear();
            string[] comNumTable = backstage.getPortTable();
            comSelectCB.Items.AddRange(comNumTable);
        }
        public override void Refresh()
        {
            setPortState();
            base.Refresh();
        }


        private void comSelectCB_Click(object sender, EventArgs e)
        {
            getUartTable();
            if ((comSelectCB.Text != "---") && (comSelectCB.Text != ""))
            {
                backstage.openPort(comSelectCB.Text);
            }
            setPortState();
        }
        private void comSelectCB_TextChanged(object sender, EventArgs e)
        {
            if ((comSelectCB.Text != "---") && (comSelectCB.Text != ""))
            {
                backstage.openPort(comSelectCB.Text);
            }
            setPortState();
        }

        private void CloseConnectBTN_Click(object sender, EventArgs e)
        {
            backstage.ClosePort();
            setPortState();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void renameHardware_click(object sender, EventArgs e)
        {
            if (this.EnterNameTB.Visible)
            {
                rename();
            }
            else
            {
                if (backstage.Is_opened == false)
                {
                    MessageBox.Show("The Port is Not Opened!");

                }
                this.EnterNameTB.Text = this.comSelectCB.Text;
                this.EnterNameTB.Visible = true;
            }
        }
        private void EnterNameTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                rename();
            }
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                this.EnterNameTB.Visible = false;
                this.EnterNameTB.Text = "";
            }
        }
        private bool rename()
        {
            if (this.EnterNameTB.Text.Length > 30)
            {
                MessageBox.Show("The Name Length shold less than 30.");
                return false;
            }
            if (this.EnterNameTB.Text.Length == 0)
            {
                MessageBox.Show("Please Enter New Name.");
                return false;
            }
            try
            {
                backstage.changeName(this.EnterNameTB.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            this.EnterNameTB.Visible = false;
            return true;
        }

    }
}
