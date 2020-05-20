using System.Windows.Forms;

namespace SRB_CTR
{
    internal partial class UpdateAll_uc : UserControl
    {
        private SrbOnelineMaster backlogic;
        Timer timer;
        public UpdateAll_uc(SrbOnelineMaster backlogic)
        {
            InitializeComponent();
            this.backlogic = backlogic;
            timer = new Timer();
            timer.Interval = 5000;
            timer.Tick += GotoUpdateByPowerOn_stop;
        }


        private void gotoUpdateMode_Click(object sender, System.EventArgs e)
        {
            backlogic.Update_all.gotoUpdateModeAll();

        }

        private void GotoUpdateByPowerOnBTN_Click(object sender, System.EventArgs e)
        {
            if (timer.Enabled)
            {
                GotoUpdateByPowerOn_stop(sender, e);
            }
            else {
                backlogic.Update_all.gotoUpdateAllFromPowerOn_start();
                timer.Start();
            }
        }
        private void GotoUpdateByPowerOn_stop(object sender, System.EventArgs e)
        {
            timer.Stop();
            backlogic.Update_all.gotoUpdateAllFromPowerOn_stop();
        }

        private void scanUpdateBTN_Click(object sender, System.EventArgs e)
        {

            this.backlogic.address_bc.scanUpdateNodes();
        }

        private void FileBTN_Click(object sender, System.EventArgs e)
        {
            backlogic.Update_all.loadFiles("./update");
            infoRTC.Clear();
            infoRTC.AppendText("lode file from './update'.\n" +
                 backlogic.Update_all.Sup_loader.ToString()); 

        }

        private void BurnBTN_Click(object sender, System.EventArgs e)
        {
            backlogic.Update_all.burnAll(appendInfo);
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
                else { 
                    infoRTC.AppendText(st);
                }
            }
        }

        private void recoedBTN_Click(object sender, System.EventArgs e)
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

        private void closeBTN_Click(object sender, System.EventArgs e)
        {
            this.Hide();
        }
    }

}
