using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SRB.Frame;

namespace SRB_CTR
{
    public partial class mainForm : Form
    {
        SRB_oneline_master backlogic;
        Control config_ctrl;
        Size nodeSize = new Size(70, 48);
        public mainForm(SRB_oneline_master pa)
        {
            InitializeComponent();
            nodesTable.BackColor = support.Color_BackGround;
            frameCounterFLP.BackColor = support.Color_BackGround;
            this.BackColor = support.Color_HighLight;

            frameCounterFLP.Refresh();
            backlogic = pa;
            setPortState();
            brainRunStateUpdate();
            stopAddrShowBTN.Visible = false;
            // cycleSet(this, null);
            backlogic.eNode_register += new SRB_oneline_master.dNodeChange(addNode);
            backlogic.eNode_unregister += new SRB_oneline_master.dNodeChange(removeNode);
            backlogic.eNode_change += new SRB_oneline_master.dNodeChange(changeNode);
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            VersionLAB.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() +"  @  "+
                System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString();

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            backlogic.Dispose();
            base.OnClosing(e);
        }
        #region node Table sync
        private delegate void dElegateNode(BaseNode n);
        public void addNode(BaseNode n)
        {
            if (this.InvokeRequired)
            {
                dElegateNode d = new dElegateNode(addNode);
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
                nodeButtonSet(b);
                this.nodesTable.Controls.Add(b);
            }
        }
        public void changeNode(BaseNode n)
        {
            if (this.InvokeRequired)
            {
                dElegateNode d = new dElegateNode(changeNode);
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
                nodeButtonSet(b);
            }
        }

        public void nodeStringSet(BaseNode n)
        {
            if (this.InvokeRequired)
            {
                dElegateNode d = new dElegateNode(changeNode);
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
                if (n.Addr >= 100)
                {
                    b.ForeColor = support.Color_red;
                }
                else
                {
                    b.ForeColor = support.Color_dank;
                }
                this.NodeTipTT.SetToolTip(b, n.ToolTip());
            }
        }


        private void nodeButtonSet(Button b)
        {
            BaseNode n = b.Tag as BaseNode;
            b.BackColor = Color.GhostWhite;
            b.Size = nodeSize;
            b.Click += new EventHandler(nodeButton_Click);
            nodeStringSet(n);

        }

        private string getNodeString(BaseNode n)
        {
            return string.Format("A:{0}\n{1}", n.Addr, n.Name);
        }

        public void removeNode(BaseNode n)
        {
            if (this.InvokeRequired)
            {
                dElegateNode d = new dElegateNode(removeNode);
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
            Button b = (Button)sender;
            BaseNode n = (BaseNode)(b.Tag);
            Node_form nf = n.getForm();
            nf.ShowAt((System.Windows.Forms.Control)sender);
            changeNode(n);
        }


        #endregion

        #region brain config
        //void cycleSet(object sender, EventArgs e)
        //{
        //    int cycle;
        //    if (false == int.TryParse(cycleTB.Text, out cycle))
        //    {
        //        cycleTB.Text = cycle.ToString();
        //    }
        //    else
        //    {
        //        this.frame.Cycle_in_ms = cycle;
        //    }
        //}

        void stopBTN_Click(object sender, EventArgs e)
        {
            backlogic.stopCalculation();
            while(backlogic.Is_calculation_running);
            brainRunStateUpdate();
        }

        void runBTN_Click(object sender, EventArgs e)
        {
            if(backlogic.isHighSpeedSupporting() == false)
            {
                DialogResult dr; 
                dr = MessageBox.Show(this, "Now you are using UART which is too slow to run brain. Are you sure to conntinue?","Brain start", MessageBoxButtons.OKCancel);
                if(dr == DialogResult.Cancel)
                {
                    return;
                }
            }
            backlogic.runCalculation(); brainRunStateUpdate();
        }
        void brainRunStateUpdate()
        {
            if (backlogic.Is_calculation_running)
            {
                stopBTN.Visible =
                !(SRB_config.Enabled 
                = ScanNodeBTN.Enabled
                = runBTN.Visible 
                = nodesTable.Enabled 
                = false);
                if (config_ctrl != null)config_ctrl.Enabled = false;
                if (scanNodeCtrl != null) scanNodeCtrl.Enabled = false;
                backlogic.endScan();
                foreach(BaseNode n in backlogic.Nodes)
                {
                    if (n != null)
                    {
                        n.closeNodeForm();
                    }
                }

            }
            else
            {
                stopBTN.Visible =
                !(SRB_config.Enabled
                = ScanNodeBTN.Enabled
                = runBTN.Visible
                = nodesTable.Enabled = true);
                if (config_ctrl != null) config_ctrl.Enabled = true;
                if (scanNodeCtrl != null) scanNodeCtrl.Enabled = true;
            }
        }



        #endregion




        public double us_calculation = 0, us_communation = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            setPortState();
            if(scanNodeCtrl!=null)
            {
                if(scanNodeCtrl.Visible)
                {
                    scanNodeCtrl.Refresh();
                }
            }

            addrShowStep();
        }


        private void addrShowStep()
        {
            if (is_addr_show_on)
            {
                if (addr_show_sno == 6000)
                {
                    backlogic.ledAddrAll(SRB.Frame.Cluster.AddressCluster.LedAddrType.Close);
                    stopAddrShowBTN_Click(this, null);
                }
                switch (addr_show_sno % 3)
                {
                    case 0:
                        backlogic.ledAddrAll(SRB.Frame.Cluster.AddressCluster.LedAddrType.High);
                        break;
                    case 1:
                        backlogic.ledAddrAll(SRB.Frame.Cluster.AddressCluster.LedAddrType.Low);
                        break;
                    case 2:
                        backlogic.ledAddrAll(SRB.Frame.Cluster.AddressCluster.LedAddrType.Close);
                        break;
                }
                addr_show_sno++;
            }
        }
        bool last_port_status =false;
        private void setPortState()
        {
            bool port_status;
            port_status = backlogic.Is_port_opend;
            if (port_status != last_port_status)
            {
                if (backlogic.Is_port_opend)
                {
                    this.srbRunning();
                }
                else
                {
                    this.srbStoped();
                }
                if (config_ctrl != null)
                {
                    if (config_ctrl.Visible)
                    {
                        config_ctrl.Refresh();
                    }
                }
            }
            last_port_status = port_status;
        }




        private void ShowRecordBTN_r_Click(object sender, EventArgs e)
        {
            backlogic.endRecord();
            ShowRecordBTN_r.Visible = false;
            ShowRecordBTN_s.Visible = true;
        }
        private void ShowRecordBTN_s_Click(object sender, EventArgs e)
        {
            backlogic.beginRecord();
            ShowRecordBTN_s.Visible = false;
            ShowRecordBTN_r.Visible = true;
        }

        scanNodeState scanNodeCtrl;
        private bool is_addr_show_on = false;
        private int addr_show_sno = 0;
        private void stopAddrShowBTN_Click(object sender, EventArgs e)
        {
            is_addr_show_on = false;
            this.stopAddrShowBTN.Visible = false;
            this.beginAddrShowBTN.Visible = true;
            backlogic.ledAddrAll(SRB.Frame.Cluster.AddressCluster.LedAddrType.Close);
        }

        private void beginAddrShowBTN_Click(object sender, EventArgs e)
        {
            is_addr_show_on = true;
            addr_show_sno = 0;
            this.stopAddrShowBTN.Visible = true;
            this.beginAddrShowBTN.Visible = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/lee8871/SRB");
        }

        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
        Image portStoped = global::SRB_CTR.Properties.Resources._1175759;
        Image portRunning = global::SRB_CTR.Properties.Resources._1175746;
        private void srbStoped()
        {
            this.SRB_config.Image = portStoped;
        }
        private void srbRunning()
        {
            this.SRB_config.Image = portRunning;
        }

        private void uSBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (config_ctrl != backlogic.usbControlDisplay())
            {
                frameCounterFLP.Controls.Remove(config_ctrl);
                config_ctrl = backlogic.usbControlDisplay();
                frameCounterFLP.Controls.Add(config_ctrl);
                config_ctrl.Show();
                this.uSBToolStripMenuItem.Checked = true;
                this.uARTToolStripMenuItem.Checked = false;
            }
            else
            {
                if(config_ctrl.Visible)
                {
                    config_ctrl.Hide();
                }
                else
                {
                    config_ctrl.Show();
                }
            }
        }
        private void uARTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(config_ctrl != backlogic.uartControlDisplay())
            {
                frameCounterFLP.Controls.Remove(config_ctrl);
                config_ctrl = backlogic.uartControlDisplay();
                frameCounterFLP.Controls.Add(config_ctrl);
                config_ctrl.Show();
                this.uSBToolStripMenuItem.Checked = false;
                this.uARTToolStripMenuItem.Checked = true;
            }
            else
            {
                if (config_ctrl.Visible)
                {
                    config_ctrl.Hide();
                }
                else
                {
                    config_ctrl.Show();
                }
            }
        }

        private void SRB_config_ButtonClick(object sender, EventArgs e)
        {
            if (config_ctrl != null)
            {
                if (config_ctrl.Visible)
                {
                    config_ctrl.Hide();
                }
                else
                {
                    config_ctrl.Show();
                }
            }

        }


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
                }
            }
        }
    }
}
