using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR.SRB_Frame
{
    internal partial class SRB_Master_USB_Uc  : UserControl
    {
        SRB_Master_USB backstage;
        internal SRB_Master_USB_Uc(SRB_Master_USB p)
        {
            InitializeComponent();
            backstage = p; 
            comSelectCB.Click += new EventHandler(comSelectCB_Click);
            comSelectCB.TextChanged += new EventHandler(comSelectCB_TextChanged);
            comSelectCB.SelectedIndexChanged+= new EventHandler(comSelectCB_TextChanged);
            getUartTable();
            this.comSelectCB.Text = backstage.getPortName();
            this.EnterNameTB.KeyDown += EnterNameTB_KeyDown;
            setPortState();
        }



        void setPortState() 
        {
            if (backstage.Is_opened())
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


        void comSelectCB_Click(object sender, EventArgs e)
        {
            getUartTable();
            if ((comSelectCB.Text != "---")&& (comSelectCB.Text != ""))
            {
                backstage.OpenPort(comSelectCB.Text);
            }
            setPortState();
        }
        void comSelectCB_TextChanged(object sender, EventArgs e)
        {
            if ((comSelectCB.Text != "---")&& (comSelectCB.Text != "") )
            {
                backstage.OpenPort(comSelectCB.Text);
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
                rename() ;
            }
            else
            {
                if (backstage.Is_opened() == false)
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
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
            this.EnterNameTB.Visible = false;
            return true;
        }

    }
}
