using Microsoft.SqlServer.Server;
using System;
using System.Windows.Forms;

namespace SRB.Frame
{

    public partial class INodeControl : UserControl
    {
        private Node node;
        public bool is_running => sendTimer.Enabled;
        public INodeControl(Node n)
        {
            node = n;
            InitializeComponent();
            this.Disposed += INodeControl_Disposed;
        }

        private void INodeControl_Disposed(object sender, EventArgs e)
        {
            sendTimer.Stop();
            OnAccessStop();
        }

        public INodeControl()
        {
            InitializeComponent();
        }

        private void RunStopBTN_Click(object sender, EventArgs e)
        {
            if (sendTimer.Enabled)
            {
                this.RunStopBTN.BackgroundImage = global::SRB.Frame.Properties.Resources._1175842;
                sendTimer.Stop();
                OnAccessStop();
            }
            else
            {
                this.RunStopBTN.BackgroundImage = global::SRB.Frame.Properties.Resources._1175836;
                OnAccessStart();
                sendTimer.Start();
            }

        }
        virtual protected void OnAccessStop()
        {

        }

        virtual protected void OnAccessStart()
        {

        }

        private void sendTimer_Tick(object sender, EventArgs e)
        {
            OnAccess();
        }

        virtual protected void OnAccess()
        {
            node.Datas.addDataAccess(this.MappingSelectCB.SelectedIndex,true);
        }



        private void sendFreqNUM_ValueChanged(object sender, EventArgs e)
        {
            this.sendTimer.Interval = (int)(1000.0 / (double)(sendFreqNUM.Value));
        }
        private void HelpBTN_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(node.Help_net_work);
        }



        private void Retry_display_Tick(object sender, EventArgs e)
        {
            this.retryLAB.Text = string.Format("Access:{0} Retry:{1} Lose:{2}",
                        node.Access_counter, node.Access_retry_counter, node.Access_fail_counter);
        }
    }
}
