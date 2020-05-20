using System;
using System.Windows.Forms;

namespace SRB_CTR
{
    internal partial class SyncUC : UserControl
    {
        private SRB.Frame.BaseNode.SyncCluster.Broadcast sync_bc;
        public SyncUC(SRB.Frame.BaseNode.SyncCluster.Broadcast sync_bc=null)
        {
            this.sync_bc = sync_bc;
            InitializeComponent();
            this.scanPB.ForeColor = SRB.Frame.support.Color_BackGround;
            this.scanPB.BackColor = SRB.Frame.support.Color_red;
        }
        public override void Refresh()
        {
            base.Refresh();
        }

        private void closeBTN_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void calibrationBTN_Click(object sender, EventArgs e)
        {
            if (sync_bc.Is_calibrat_running == false)
            {
                sync_bc.calibrat(appendInfo);
            }
            else
            {
                sync_bc.calibratAbandon();
            }

        }
        public void appendInfo(string st)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(() => { appendInfo(st); }));
            }
            else
            {
                if (st == null)
                {
                    infoRTC.Clear();
                }
                else
                {
                    infoRTC.AppendText(st);
                }
            }
        }


        private void recoedBTN_Click(object sender, EventArgs e)
        {
            if (this.infoRTC.Visible)
            {
                infoRTC.Hide();
            }
            else
            {
                infoRTC.Show();
            }

        }
        Form sync_debug_F;
        private void DebugFormBTN_Click(object sender, EventArgs e)
        {
            if (sync_debug_F == null)
            {
                sync_debug_F = new SyncBroadcastC(sync_bc);
            }
            if(sync_debug_F.Visible == false)
            {
                sync_debug_F.Show();
            }
        }
    }
}
