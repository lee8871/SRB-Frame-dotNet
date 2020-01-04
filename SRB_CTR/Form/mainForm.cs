using SRB.Frame;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SRB_CTR
{
    public partial class mainForm : Form
    {
        private SrbOnelineMaster backlogic;
        private Control config_ctrl;
        private Size nodeSize = new Size(70, 48);
        public mainForm(SrbOnelineMaster pa)
        {
            InitializeComponent();
            nodesTable.BackColor = support.Color_BackGround;
            frameCounterFLP.BackColor = support.Color_BackGround;
            this.BackColor = support.Color_HighLight;

            frameCounterFLP.Refresh();
            backlogic = pa;
            setPortState();
            brainRunStateUpdate();
            // cycleSet(this, null);
            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            VersionLAB.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "  @  " +
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
                n.eChangeDescription += new dNodeUpdateEvent(nodeStringSet);
                n.eDispossing += new dNodeUpdateEvent(removeNode);
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
        public void nodeStringSet(BaseNode n)
        {
            if (this.InvokeRequired)
            {
                dElegateNode d = new dElegateNode(nodeStringSet);
                this.Invoke(d, new object[] { n });
            }
            else
            {
                if (n.Tag == null)
                {
                    throw new Exception("Node dose not have a tag");
                }
                Button b = (Button)n.Tag;
                if (n.Is_in_update)
                {
                    b.Text = string.Format("A:{0}\n{1}", n.Addr,"Update");
                    b.ForeColor = support.Color_navy;

                }
                else
                {
                    b.Text = getNodeString(n);
                    if (n.Addr >= 100)
                    {
                        b.ForeColor = support.Color_red;
                    }
                    else
                    {
                        b.ForeColor = support.Color_dank;
                    }
                }
                this.NodeTipTT.SetToolTip(b, n.GetToolTip());
            }
        }



        private string getNodeString(BaseNode n)
        {
            return string.Format("{0}\n{1}", n.Addr, n.Name);
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

        private void nodeButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            BaseNode n = (BaseNode)(b.Tag);
            Node_form nf = n.getForm();
            nf.showAt((System.Windows.Forms.Control)sender);
           // changeNode(n);
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

        private void stopBTN_Click(object sender, EventArgs e)
        {
            backlogic.stopCalculation();
            while (backlogic.Is_calculation_running) ;
            brainRunStateUpdate();
        }

        private void runBTN_Click(object sender, EventArgs e)
        {
            if (backlogic.isHighSpeedSupporting() == false)
            {
                DialogResult dr;
                dr = MessageBox.Show(this, "Now you are using UART which is too slow to run brain. Are you sure to conntinue?", "Brain start", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }
            backlogic.runCalculation(); brainRunStateUpdate();
        }

        private void brainRunStateUpdate()
        {
            if (backlogic.Is_calculation_running)
            {
                stopBTN.Visible =
                !(SRB_config.Enabled
                = ScanNodeBTN.Enabled
                = runBTN.Visible
                = nodesTable.Enabled
                = false);
                if (config_ctrl != null) config_ctrl.Enabled = false;
                if (scanNodeCtrl != null) scanNodeCtrl.Enabled = false;
                backlogic.endScan();
                foreach (BaseNode n in backlogic.Bus)
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
            ShowRecordBTN_changeImage();
            setPortState();
            if (scanNodeCtrl != null)
            {
                if (scanNodeCtrl.Visible)
                {
                    scanNodeCtrl.Refresh();
                }
            }
            addrShowStep();
        }




        private bool last_port_status = false;
        private void setPortState()
        {
            bool port_status;
            port_status = backlogic.Bus.Is_opened; 
            if (port_status != last_port_status)
            {
                if (backlogic.Bus.Is_opened)
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

        int showRecordBtnFlag = 0;

        Image[] showRecordBtn_Images =
            { Properties.Resources.record0,
            Properties.Resources.record1,
            Properties.Resources.record2,
            Properties.Resources.record3,
            Properties.Resources.record4 };
        private void ShowRecordBTN_Click(object sender, EventArgs e)
        {
            if (showRecordBtnFlag == 0)
            {
                showRecordBtnFlag = 1;
                ShowRecordBTN.Image = showRecordBtn_Images[showRecordBtnFlag];
                backlogic.beginRecord();
            }
            else
            {
                showRecordBtnFlag = 0;
                ShowRecordBTN.Image = showRecordBtn_Images[showRecordBtnFlag];
                backlogic.endRecord();
            }
        }
        private void ShowRecordBTN_changeImage()
        {
            if (showRecordBtnFlag != 0)
            {
                showRecordBtnFlag++;
                if(showRecordBtnFlag == 5)
                {
                    showRecordBtnFlag = 1;
                }
                ShowRecordBTN.Image = showRecordBtn_Images[showRecordBtnFlag];
            }

        }


        private scanNodeState scanNodeCtrl;
        private int addrShowBTN_flag = 0;
        private int addr_show_status = 0;
        Image[] AddrShowBTN_Images =
            { Properties.Resources.AddrLed0,
            Properties.Resources.AddrLed1,
            Properties.Resources.AddrLed2,};
        private void AddrShowBTN_Click(object sender, EventArgs e)
        {

            if (addrShowBTN_flag == 0)
            {
                addrShowBTN_flag = 1;
                AddrShowBTN.Image = AddrShowBTN_Images[addrShowBTN_flag];
                backlogic.endRecord();
            }
            else
            {
                addrShowBTN_flag = 0;
                AddrShowBTN.Image = AddrShowBTN_Images[addrShowBTN_flag];
                backlogic.beginRecord();
                backlogic.ledAddrAll(SRB.Frame.BaseNode.AddressCluster.LedAddrType.Close);
            }

        }

        private void addrShowStep()
        {
            if (addrShowBTN_flag!=0)
            {
                addr_show_status++;
                addr_show_status %= 3;
                switch (addr_show_status)
                {
                    case 0:
                        backlogic.ledAddrAll(SRB.Frame.BaseNode.AddressCluster.LedAddrType.High);
                        break;
                    case 1:
                        backlogic.ledAddrAll(SRB.Frame.BaseNode.AddressCluster.LedAddrType.Low);
                        break;
                    case 2:
                        backlogic.ledAddrAll(SRB.Frame.BaseNode.AddressCluster.LedAddrType.Close);
                        break;
                }
                addrShowBTN_flag++;
                if (addrShowBTN_flag == 3)
                {
                    addrShowBTN_flag = 1;
                }
                AddrShowBTN.Image = AddrShowBTN_Images[addrShowBTN_flag];

            }
        }



        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/lee8871/SRB");
        }

        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
        private void srbStoped()
        {
            this.SRB_config.Image = Properties.Resources.disconnect;
        }
        private void srbRunning()
        {
            this.SRB_config.Image = Properties.Resources.connect;
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
        private void uARTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (config_ctrl != backlogic.uartControlDisplay())
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
        private UpdateAll_uc update_all_ctrl;
        private void updateAll_Click(object sender, EventArgs e)
        {
            if (update_all_ctrl == null)
            {
                update_all_ctrl = new UpdateAll_uc(this.backlogic);
                frameCounterFLP.Controls.Add(update_all_ctrl);
                update_all_ctrl.Show();
            }
            else
            {
                if (update_all_ctrl.Visible)
                {
                    update_all_ctrl.Hide();
                }
                else
                {
                    update_all_ctrl.Show();
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
