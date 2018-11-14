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
            getUartTable();
            this.comSelectCB.Text = backstage.getPortName();
        }


        void setPortState() 
        {
            if (backstage.Is_opened())
            {
                this.comSelectCB.BackColor = Color.LightGreen;
            }
            else
            {
                this.comSelectCB.BackColor = Color.LightPink;
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
    }
}
