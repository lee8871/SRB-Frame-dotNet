using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_CTR.nsFrame
{
    partial class FrameForm : Form
    {
        frame backlogic;

        Control uartConfigCtrl;
        public FrameForm(frame pa )
        {
            InitializeComponent();
            backlogic = pa;
            setPortState();
            brainRunStateUpdate();
            cycleTB.LostFocus += new EventHandler(cycleSet);
            brainRunBTN.Click+=new EventHandler(brainRunBTN_Click);
            brainStopBTN.Click+=new EventHandler(brainStopBTN_Click);
            cycleSet(this, null);
            backlogic.eNode_register += new frame.eNodeChange(addNode);
            backlogic.eNode_unregister += new frame.eNodeChange(removeNode);
            backlogic.eNode_change += new frame.eNodeChange(changeNode);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            backlogic.scan_stop = true;

            base.OnClosing(e);
        }
        #region node Table sync
        private delegate void delegateNode(Node n);
        public void addNode(Node n)
        {
            if (this.InvokeRequired)
            {
                delegateNode d = new delegateNode(addNode);
                this.Invoke(d, new object[] { n });
            }
            else
            {
                if (n.Tag != null)
                {
                    throw new Exception("node has Tag");
                }
                Button b = new Button();
                n.Tag = b;
                b.Tag = n;
                b.Text = getNodeString(n);
                b.BackColor = Color.PaleGreen;
                b.Size = new Size(48, 48);
                b.Click += new EventHandler(nodeButton_Click);
                this.nodesTable.Controls.Add(b);
            }
        }
        public void changeString(Node n)
        {
            if (this.InvokeRequired)
            {
                delegateNode d = new delegateNode(changeNode);
                this.Invoke(d, new object[] { n });
            }
            else
            {
                if (n.Tag == null)
                {
                    throw new Exception("Node dose not have a tag");
                }
                Button b = (Button)n.Tag;
                b.Text = getNodeString(n);
            }
            
        }


        public void changeNode(Node n)
        {
            if (this.InvokeRequired)
            {
                delegateNode d = new delegateNode(changeNode);
                this.Invoke(d, new object[] { n });
            }
            else
            {
                if (n.Tag == null)
                {
                    throw new Exception("Node dose not have a tag");
                }
                Button b = (Button)n.Tag;
                b.Tag = n;
                b.Text = getNodeString(n);
                b.BackColor = Color.PaleGreen;
                b.Size = new Size(48, 48);
                b.Click += new EventHandler(nodeButton_Click);
                this.nodesTable.Controls.Add(b);
            }
        }

        private string getNodeString(Node n)
        {
            return string.Format("A:{0}\n{1}", n.Addr, n.Name); 
        }

        public void removeNode(Node n)
        {
            if (this.InvokeRequired)
            {
                delegateNode d = new delegateNode(removeNode);
                this.Invoke(d, new object[] { n });
            }
            else
            {
                if (n.Tag == null)
                {
                    throw new Exception("Node dose not have tag!");
                }
                Button b = (Button)n.Tag;
                this.nodesTable.Controls.Remove(b);
            }
        }
        void nodeButton_Click(object sender, EventArgs e)
        {
            Button b = (Button) sender;
            Node n = (Node)(b.Tag);
            Node_form nf = n.getForm();
            nf.ShowAt((System.Windows.Forms.Control)sender);
        }
        #endregion

        #region brain config
        void cycleSet(object sender, EventArgs e)
        {
            int cycle;
            if (false == int.TryParse(cycleTB.Text, out cycle))
            {
                cycleTB.Text = cycle.ToString();
            }
            else
            {
                this.backlogic.Cycle_in_ms = cycle;
            }
        }
        void  brainStopBTN_Click(object sender, EventArgs e)
        {
            backlogic.stopCalculation(); brainRunStateUpdate();
        }

        void  brainRunBTN_Click(object sender, EventArgs e)
        {
            backlogic.runCalculation(); brainRunStateUpdate();
        }
        void brainRunStateUpdate()
        {
            if (backlogic.Is_calculation_running)
            {
                brainStopBTN.Visible = 
                !(brainRunBTN.Visible = false);
            }
            else
            {
                brainStopBTN.Visible =
                !(brainRunBTN.Visible = true);
            }
        }



        #endregion




        public double us_calculation = 0 , us_communation = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
           setPortState();
        
        }

        private void setPortState() 
        {
            if (backlogic.Is_port_opend)
            {
                this.MasterBroadConfigBTN_c.Visible = true;
                this.MasterBroadConfigBTN_uc.Visible = false;
            }
            else
            {
                this.MasterBroadConfigBTN_c.Visible = false;
                this.MasterBroadConfigBTN_uc.Visible = true;
            }
        }

        private void MasterBroadConfigBTN_Click(object sender, EventArgs e)
        {
            if (uartConfigCtrl == null)
            {
                uartConfigCtrl = backlogic.getuartConfigContol();
                frameCounterFLP.Controls.Add(uartConfigCtrl);
                uartConfigCtrl.Show();
            }
            else
            {
                if (uartConfigCtrl.Visible)
                {
                    uartConfigCtrl.Hide();
                }
                else
                {
                    uartConfigCtrl.Show();
                }
            }

        }



        private void ShowAccessBTN_Click(object sender, EventArgs e)
        {
            backlogic.getAccessDisplayer().Show(this);
        }
        
        scanNodeState scanNodeCtrl;
        private void ScanNodeBTN_Click(object sender, EventArgs e)
        {
            if (scanNodeCtrl == null)
            {
                scanNodeCtrl = new scanNodeState(this.backlogic);
                frameCounterFLP.Controls.Add(scanNodeCtrl);
                scanNodeCtrl.Show();
                this.backlogic.scanNodes();
            }
            else
            {
                if (scanNodeCtrl.Visible)
                {
                    scanNodeCtrl.Hide();
                }
                else
                {
                    scanNodeCtrl.Show();
                    this.backlogic.scanNodes();
                }
            }
        }
    }
}
