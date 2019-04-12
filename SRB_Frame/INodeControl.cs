using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB.Frame
{
    public partial class INodeControl : UserControl
    {
        BaseNode node;
        public bool is_running { get => sendTimer.Enabled; }
        public INodeControl(BaseNode n)
        {
            node = n;
            InitializeComponent();
        }
        public INodeControl()
        {
            InitializeComponent();
        }

        private void RunStopBTN_Click(object sender, EventArgs e)
        {
            if (sendTimer.Enabled)
            {
                this.RunStopBTN.BackgroundImage = global::SRB_Frame.Properties.Resources._1175842;
                sendTimer.Stop() ;
                OnAccessStop();
            }
            else
            {
                this.RunStopBTN.BackgroundImage = global::SRB_Frame.Properties.Resources._1175836;
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

            node.singleAccess(this.MappingSelectCB.SelectedIndex);
        }



        private void sendFreqNUM_ValueChanged(object sender, EventArgs e)
        {
            this.sendTimer.Interval = (int)(1000.0 / (double)(sendFreqNUM.Value));
        }
    }
}
