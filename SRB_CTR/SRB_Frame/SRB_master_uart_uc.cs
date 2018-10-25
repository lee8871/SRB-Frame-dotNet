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
    internal partial class SRB_master_uart_uc  : UserControl
    {
        SRB_Master_Uart bacestage;
        internal SRB_master_uart_uc(SRB_Master_Uart bs)
        {
            InitializeComponent();
            bacestage = bs; 
            comSelectCB.Click += new EventHandler(comSelectCB_Click);
            comSelectCB.TextChanged += new EventHandler(comSelectCB_TextChanged);
            getUartTable();
            if (bacestage.Record_port_data)
            {
                this.OpenRecordDataBTN.Visible = false;
                this.CloseRecordDataBTN.Visible = true;
            }
            else
            {
                this.OpenRecordDataBTN.Visible = true;
                this.CloseRecordDataBTN.Visible = false;
            }
            this.comSelectCB.Text = bacestage.getPortName();
        }


        void setPortState() 
        {
            if (bacestage.Is_opened())
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
            string[] comNumTable = bacestage.getPortTable();
            comSelectCB.Items.AddRange(comNumTable);
        }


        void comSelectCB_Click(object sender, EventArgs e)
        {
            getUartTable();
            if (comSelectCB.Text != "")
            {
                bacestage.OpenPort(comSelectCB.Text);
            }
            setPortState();

        }
        void comSelectCB_TextChanged(object sender, EventArgs e)
        {
            if (comSelectCB.Text != "")
            {
                bacestage.OpenPort(comSelectCB.Text);
            }
            setPortState();
        }

        private void CloseConnectBTN_Click(object sender, EventArgs e)
        {
            bacestage.ClosePort();
            setPortState();
        }

        private void CloseRecordDataBTN_Click(object sender, EventArgs e)
        {
            bacestage.Record_port_data = false;
                this.OpenRecordDataBTN.Visible = true;
                this.CloseRecordDataBTN.Visible = false;
        }

        private void OpenRecordDataBTN_Click(object sender, EventArgs e)
        {
            bacestage.Record_port_data = true;
            this.OpenRecordDataBTN.Visible = false;
            this.CloseRecordDataBTN.Visible = true;
        }
    }
}
