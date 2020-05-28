using SRB.Frame;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
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
           // this.BackColor = support.Color_HighLight;

            frameCounterFLP.Refresh();
            backlogic = pa;
            setPortState();
            brainRunStateUpdate();
            // cycleSet(this, null);
            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            VersionLAB.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() +
                global::SRB_CTR.Properties.Resources.VersionType;

            frameCounterFLP.SizeChanged += FrameCounterFLP_SizeChanged;
            mainSC.SplitterDistance = frameCounterFLP.Size.Height + 4;


        }

        private void FrameCounterFLP_SizeChanged(object sender, EventArgs e)
        {
            mainSC.SplitterDistance = frameCounterFLP.Size.Height + 4;
        }


        #region node Table sync
        private delegate void dElegateNode(Node n);
        public void addNode(Node n)
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
            Node n = b.Tag as Node;
            b.BackColor = Color.GhostWhite;
            b.Size = nodeSize;
            b.Click += new EventHandler(nodeButton_Click);
            nodeStringSet(n);

        }
        public void nodeStringSet(Node n)
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



        private string getNodeString(Node n)
        {
            return string.Format("{0}\n{1}", n.Addr, n.Name);
        }

        public void removeNode(Node n)
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
            Node n = (Node)(b.Tag);
            NodeForm nf = n.getForm();
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
                = updateAllBTN.Enabled
                = false);
                if (config_ctrl != null) config_ctrl.Enabled = false;
                if (scanNodeCtrl != null) scanNodeCtrl.Enabled = false;
                if (update_all_ctrl != null) update_all_ctrl.Enabled = false;
                backlogic.address_bc.endScan();
                foreach (Node n in backlogic.Bus)
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
                = nodesTable.Enabled
                = updateAllBTN.Enabled 
                = true);
                if (config_ctrl != null) config_ctrl.Enabled = true;
                if (scanNodeCtrl != null) scanNodeCtrl.Enabled = true;
                if (update_all_ctrl != null) update_all_ctrl.Enabled = true;
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
            if (sync_ctrl != null)
            {
                Image temp_image = sync_ctrl.syncBTN_changeImage();
                if (temp_image != null)
                {
                    SyncBTN.Image = temp_image;
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
                backlogic.ledAddrAll(SRB.Frame.Node.AddressCluster.LedAddrType.Close);
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
                        backlogic.ledAddrAll(SRB.Frame.Node.AddressCluster.LedAddrType.High);
                        break;
                    case 1:
                        backlogic.ledAddrAll(SRB.Frame.Node.AddressCluster.LedAddrType.Low);
                        break;
                    case 2:
                        backlogic.ledAddrAll(SRB.Frame.Node.AddressCluster.LedAddrType.Close);
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
        SyncUC sync_ctrl;

        private void SyncBTN_Click(object sender, EventArgs e)
        {

            if (sync_ctrl == null)
            {
                sync_ctrl = new SyncUC(backlogic.sync_bc);
                frameCounterFLP.Controls.Add(sync_ctrl);
                sync_ctrl.Show();
            }
            else
            {
                if (sync_ctrl.Visible)
                {
                    sync_ctrl.Hide();
                }
                else
                {
                    sync_ctrl.Show();
                }
            }
        }
        int closing_click_flag = 0;

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (closing_click_flag == 0) {
                void safeClose()
                {
                    backlogic.Dispose();
                    this.closing_click_flag = 100;
                    this.Invoke(new MethodInvoker(() => { Close(); }));
                }
                Thread stopBackLogic = new Thread(safeClose);
                stopBackLogic.Start();
            }
            closing_click_flag++;
            if (closing_click_flag > 5)
            {
                e.Cancel = false;
            }
        }

        private void ScanNodeBTN_Click(object sender, EventArgs e)
        {
            if (scanNodeCtrl == null)
            {
                scanNodeCtrl = new scanNodeState(this.backlogic);
                frameCounterFLP.Controls.Add(scanNodeCtrl);
                scanNodeCtrl.Show();
                this.backlogic.address_bc.scanNodes();
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
