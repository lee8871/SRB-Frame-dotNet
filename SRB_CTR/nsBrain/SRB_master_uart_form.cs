using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR.nsBrain
{
    internal partial class SRB_master_uart_form : Form
    {
        SRB_Master_Uart parent;
        internal SRB_master_uart_form(SRB_Master_Uart p)
        {
            InitializeComponent();
            parent = p; 
            comSelectCB.Click += new EventHandler(comSelectCB_Click);
            comSelectCB.TextChanged += new EventHandler(comSelectCB_TextChanged);
            getUartTable();
            if (parent.Record_port_data)
            {
                this.OpenRecordDataBTN.Visible = false;
                this.CloseRecordDataBTN.Visible = true;
            }
            else
            {
                this.OpenRecordDataBTN.Visible = true;
                this.CloseRecordDataBTN.Visible = false;
            }
            this.comSelectCB.Text = parent.getPortName();
        }

        protected override void OnClosed(EventArgs e)
        {
            parent.configFormClosed();
            base.OnClosed(e);
        }

        void setPortState() 
        {
            if (parent.Is_opened())
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
            string[] comNumTable = parent.getPortTable();
            comSelectCB.Items.AddRange(comNumTable);
        }


        void comSelectCB_Click(object sender, EventArgs e)
        {
            getUartTable();
            if (comSelectCB.Text != "")
            {
                parent.OpenPort(comSelectCB.Text);
            }
            setPortState();

        }
        void comSelectCB_TextChanged(object sender, EventArgs e)
        {
            if (comSelectCB.Text != "")
            {
                parent.OpenPort(comSelectCB.Text);
            }
            setPortState();
        }

        private void CloseConnectBTN_Click(object sender, EventArgs e)
        {
            parent.ClosePort();
            setPortState();
        }

        private void CloseRecordDataBTN_Click(object sender, EventArgs e)
        {
            parent.Record_port_data = false;
                this.OpenRecordDataBTN.Visible = true;
                this.CloseRecordDataBTN.Visible = false;
        }

        private void OpenRecordDataBTN_Click(object sender, EventArgs e)
        {
            parent.Record_port_data = true;
            this.OpenRecordDataBTN.Visible = false;
            this.CloseRecordDataBTN.Visible = true;
        }
    }
}
