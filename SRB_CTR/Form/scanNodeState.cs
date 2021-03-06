﻿using System;
using System.Windows.Forms;

namespace SRB_CTR
{
    internal partial class scanNodeState : UserControl
    {
        private SrbOnelineMaster backlogic;
        public scanNodeState(SrbOnelineMaster frame = null)
        {
            this.backlogic = frame;
            InitializeComponent();
            this.scanPB.ForeColor = SRB.Frame.support.Color_BackGround;
            this.scanPB.BackColor = SRB.Frame.support.Color_red;
        }
        public override void Refresh()
        {
            scanPB.Value = (int)(100 * backlogic.address_bc.Scan_progress);
            switch (backlogic.address_bc.Scan_status)
            {
                case "Scan is not begin":
                case "Scan breaked":
                case "Scan done":
                case "Set address done":
                case "Scan update breaked":
                case "Scan update finish":
                    ContrulsEnable = true;
                    break;
                default:
                    ContrulsEnable = false;
                   // this.scanL.Text = string.Format("Scan node{0}.", backlogic.Scan_status); 
                    break;
            }
            base.Refresh();
        }

        private bool ContrulsEnable
        {
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
            backlogic.address_bc.endScan();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AutoSetAddressBTN_Click(object sender, EventArgs e)
        {
            backlogic.address_bc.autoSetAddress();

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
            this.backlogic.address_bc.scanNodes();
        }

        private void RandomNewNodeBTN_Click(object sender, EventArgs e)
        {
            this.backlogic.resetNewNodeAddress();
            this.backlogic.address_bc.scanNodes();
        }

        private void StartBTN_Click(object sender, EventArgs e)
        {
            this.backlogic.address_bc.scanNodes();
        }
    }
}
