using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR.SRB_Frame
{
    partial class scanNodeState : UserControl
    {
        private SrbFrame backlogic;

        public scanNodeState(SrbFrame frame = null)
        {
            this.backlogic = frame;
            InitializeComponent();
            this.scanPB.ForeColor = Support.Color_blue;
            this.scanPB.BackColor = Support.Color_red;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            scanPB.Value = (int)(100 * backlogic.Scan_progress);
            switch (backlogic.Scan_addr)
            {
                case -1:
                    this.scanL.Text = "Scaning is not begin."; break;
                case -2:
                    this.scanL.Text = "Scaning is break."; break;
                case -3:
                    this.scanL.Text = "Scaning done"; break;
                default:
                    this.scanL.Text = string.Format("Scaning At Node {0}.",backlogic.Scan_addr); break;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            backlogic.scan_stop = true;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}
