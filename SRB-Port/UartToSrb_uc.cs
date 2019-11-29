using System;
using System.Drawing;
using System.Windows.Forms;

namespace SRB.port
{
    public partial class UartToSrb_uc : UserControl
    {
        private UartToSrb background;
        public UartToSrb_uc(UartToSrb bs)
        {
            InitializeComponent();
            background = bs;
            comSelectCB.Click += new EventHandler(comSelectCB_Click);
            //comSelectCB.TextChanged += new EventHandler(comSelectCB_TextChanged);
            comSelectCB.SelectedIndexChanged += new EventHandler(comSelectCB_TextChanged);
            getUartTable();
            this.comSelectCB.Text = background.getPortName();
        }

        private void setPortState()
        {
            if (background.Is_opened)
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
            string[] comNumTable = background.getPortTable();
            comSelectCB.Items.Clear();
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
            setPortState();
        }

        private void comSelectCB_TextChanged(object sender, EventArgs e)
        {
            if (comSelectCB.Text != "")
            {
                background.openPort(comSelectCB.Text);
            }
            setPortState();
        }

        private void CloseConnectBTN_Click(object sender, EventArgs e)
        {
            background.ClosePort();
            setPortState();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}
