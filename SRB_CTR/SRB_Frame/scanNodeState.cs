using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR
{
    partial class scanNodeState : UserControl
    {
        private SrbFrame backlogic;

        public scanNodeState(SrbFrame frame = null)
        {
            this.backlogic = frame;
            InitializeComponent();
            this.scanPB.ForeColor = support.Color_blue;
            this.scanPB.BackColor = support.Color_red;
        }
        public override void Refresh()
        {
            scanPB.Value = (int)(100 * backlogic.Scan_progress);
            switch (backlogic.Scan_status)
            {
                case -1:
                    ContrulsEnable = true;
                    this.scanL.Text = "Scan is not begin."; break;
                case -2:
                    ContrulsEnable = true;
                    this.scanL.Text = "Scan Stoped"; break;
                case -3:
                    ContrulsEnable = true;
                    this.scanL.Text = "Scan Done"; break;
                default:
                    ContrulsEnable = false;
                    this.scanL.Text = string.Format("Scan node{0}.", backlogic.Scan_status); break;
            }
            base.Refresh();
        }
        bool ContrulsEnable{
            set
            {
                StartBTN.Visible = value;
                StopBTN.Visible = !(value);
                AutoSetAddressBTN.Enabled = value;
                randomAllAddressBTN.Enabled = value;
                RandomNewNodeBTN.Enabled = value;
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

        private void AutoSetAddressBTN_Click(object sender, EventArgs e)
        {
            backlogic.autoSetAddress();

        }

        private void randomAllAddressBTN_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show(this, "Now you are reseting address for ALL NODE, Are you sure to conntinue? ?", "Reset All Address", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            this.backlogic.resetAllAddress();
            this.backlogic.scanNodes();
        }

        private void RandomNewNodeBTN_Click(object sender, EventArgs e)
        {
            this.backlogic.resetNewNodeAddress();
            this.backlogic.scanNodes();
        }

        private void StartBTN_Click(object sender, EventArgs e)
        {
            this.backlogic.scanNodes();
        }
    }
}
