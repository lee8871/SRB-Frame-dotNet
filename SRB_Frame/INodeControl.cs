using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SRB.Frame
{

    public partial class INodeControl : UserControl
    {
        private Node node;
        public Node Node => node;
        public bool is_running => sendTimer.Enabled;
        public INodeControl(Node n)
        {
            node = n;
            InitializeComponent();
            this.Disposed += INodeControl_Disposed;
            RetryTIMER_Tick(this, null);
            delegate_BankChangeByAccess = new dVoid_delegate_void(onRefreshData);
            node.eBankChangeByAccess += Node_eBankChangeByAccess;
        }

        private void INodeControl_Disposed(object sender, EventArgs e)
        {
            sendTimer.Stop();
            OnAccessStop();
            node.eBankChangeByAccess -= Node_eBankChangeByAccess;
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
        private delegate void dVoid_delegate_void();
        private dVoid_delegate_void delegate_BankChangeByAccess;
        private long refresh_tick = 0;

        int test_temp_0 = 0;
        protected virtual void onRefreshData()
        {
            test_temp_0++;
        }
        private void Node_eBankChangeByAccess(object sender, EventArgs e)
        {
            long tick = Stopwatch.GetTimestamp();
            if ((tick - refresh_tick).tickToMs() > 100)
            {
                this.BeginInvoke(delegate_BankChangeByAccess);
                refresh_tick = tick;
            }
        }

        private void sendFreqNUM_ValueChanged(object sender, EventArgs e)
        {
            this.sendTimer.Interval = (int)(1000.0 / (double)(sendFreqNUM.Value));
        }
        private void HelpBTN_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(node.Help_net_work);
        }

        private void RetryTIMER_Tick(object sender, EventArgs e)
        {
            if (this.node != null) { 
                this.RetryLAB.Text = string.Format("Access:{0} Retry:{1} Lose:{2}",
                   node.Access_counter, node.Access_retry_counter, node.Access_fail_counter);
            }
        }




    }
}
