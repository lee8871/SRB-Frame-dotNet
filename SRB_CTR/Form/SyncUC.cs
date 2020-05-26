using System;
using System.Drawing;
using System.Windows.Forms;

namespace SRB_CTR
{
    internal partial class SyncUC : UserControl
    {
        private SRB.Frame.Node.SyncCluster.Broadcast sync_bc;
        public SyncUC(SRB.Frame.Node.SyncCluster.Broadcast sync_bc=null)
        {
            this.sync_bc = sync_bc;
            InitializeComponent();
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

        private void syncBTN_Click(object sender, EventArgs e)
        {
            if (sync_bc.Is_synchronize_running)
            {
                sync_bc.syncStop() ;
            }
            else
            {
                sync_bc.syncStart();
            }

        }
        int syncBtnFlag;


        Image[] syncBtn_Images =
    { Properties.Resources.clock,
            Properties.Resources.clock1,
            Properties.Resources.clock2,
            Properties.Resources.clock3
        };

        public Image syncBTN_changeImage()
        {
            if (sync_bc.Is_synchronize_running)
            {
                syncBtnFlag++;
                if (syncBtnFlag == 4)
                {
                    syncBtnFlag = 0;
                }
                syncBTN.Image = syncBtn_Images[syncBtnFlag];
                return syncBtn_Images[syncBtnFlag];
            }
            else
            {
                if (syncBtnFlag == 0)
                {
                    return null;
                }
                else
                {
                    syncBtnFlag = 0;
                    syncBTN.Image = syncBtn_Images[syncBtnFlag];
                    return syncBtn_Images[syncBtnFlag];

                }
            }
        }

        private void syncClearBTN_Click(object sender, EventArgs e)
        {
            sync_bc.calibratClean(appendInfo);

        }
    }
}
