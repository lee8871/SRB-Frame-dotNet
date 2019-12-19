namespace SRB_CTR
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.VersionLAB = new System.Windows.Forms.ToolStripStatusLabel();
            this.uiTIMER = new System.Windows.Forms.Timer(this.components);
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.NodeTipTT = new System.Windows.Forms.ToolTip(this.components);
            this.left_UpDownSC = new System.Windows.Forms.SplitContainer();
            this.frameCounterFLP = new System.Windows.Forms.FlowLayoutPanel();
            this.nodesTable = new System.Windows.Forms.FlowLayoutPanel();
            this.nodeScanTS = new System.Windows.Forms.ToolStrip();
            this.SRB_config = new System.Windows.Forms.ToolStripSplitButton();
            this.uSBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uARTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowRecordBTN_s = new System.Windows.Forms.ToolStripButton();
            this.ShowRecordBTN_r = new System.Windows.Forms.ToolStripButton();
            this.ScanNodeBTN = new System.Windows.Forms.ToolStripButton();
            this.runBTN = new System.Windows.Forms.ToolStripButton();
            this.stopBTN = new System.Windows.Forms.ToolStripButton();
            this.stopAddrShowBTN = new System.Windows.Forms.ToolStripButton();
            this.beginAddrShowBTN = new System.Windows.Forms.ToolStripButton();
            this.updateAllBTN = new System.Windows.Forms.ToolStripButton();
            this.mainTSC = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.left_UpDownSC)).BeginInit();
            this.left_UpDownSC.Panel1.SuspendLayout();
            this.left_UpDownSC.Panel2.SuspendLayout();
            this.left_UpDownSC.SuspendLayout();
            this.nodeScanTS.SuspendLayout();
            this.mainTSC.ContentPanel.SuspendLayout();
            this.mainTSC.TopToolStripPanel.SuspendLayout();
            this.mainTSC.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VersionLAB});
            this.statusStrip1.Location = new System.Drawing.Point(0, 579);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(384, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // VersionLAB
            // 
            this.VersionLAB.Name = "VersionLAB";
            this.VersionLAB.Size = new System.Drawing.Size(53, 17);
            this.VersionLAB.Text = "V0.0.1.0";
            // 
            // uiTIMER
            // 
            this.uiTIMER.Enabled = true;
            this.uiTIMER.Interval = 1000;
            this.uiTIMER.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 175);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(150, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightToolStripPanel.Location = new System.Drawing.Point(150, 25);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 150);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 25);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 150);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(384, 554);
            // 
            // left_UpDownSC
            // 
            this.left_UpDownSC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.left_UpDownSC.Location = new System.Drawing.Point(0, 0);
            this.left_UpDownSC.Name = "left_UpDownSC";
            this.left_UpDownSC.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // left_UpDownSC.Panel1
            // 
            this.left_UpDownSC.Panel1.Controls.Add(this.frameCounterFLP);
            // 
            // left_UpDownSC.Panel2
            // 
            this.left_UpDownSC.Panel2.Controls.Add(this.nodesTable);
            this.left_UpDownSC.Size = new System.Drawing.Size(384, 549);
            this.left_UpDownSC.SplitterDistance = 98;
            this.left_UpDownSC.TabIndex = 0;
            // 
            // frameCounterFLP
            // 
            this.frameCounterFLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameCounterFLP.Location = new System.Drawing.Point(0, 0);
            this.frameCounterFLP.Name = "frameCounterFLP";
            this.frameCounterFLP.Size = new System.Drawing.Size(384, 98);
            this.frameCounterFLP.TabIndex = 0;
            // 
            // nodesTable
            // 
            this.nodesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodesTable.Location = new System.Drawing.Point(0, 0);
            this.nodesTable.Name = "nodesTable";
            this.nodesTable.Size = new System.Drawing.Size(384, 447);
            this.nodesTable.TabIndex = 0;
            // 
            // nodeScanTS
            // 
            this.nodeScanTS.BackColor = System.Drawing.SystemColors.Control;
            this.nodeScanTS.Dock = System.Windows.Forms.DockStyle.None;
            this.nodeScanTS.ImageScalingSize = new System.Drawing.Size(23, 23);
            this.nodeScanTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SRB_config,
            this.ShowRecordBTN_s,
            this.ShowRecordBTN_r,
            this.ScanNodeBTN,
            this.runBTN,
            this.stopBTN,
            this.stopAddrShowBTN,
            this.beginAddrShowBTN,
            this.updateAllBTN});
            this.nodeScanTS.Location = new System.Drawing.Point(3, 0);
            this.nodeScanTS.Name = "nodeScanTS";
            this.nodeScanTS.Size = new System.Drawing.Size(298, 30);
            this.nodeScanTS.TabIndex = 4;
            // 
            // SRB_config
            // 
            this.SRB_config.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SRB_config.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uSBToolStripMenuItem,
            this.uARTToolStripMenuItem});
            this.SRB_config.Image = global::SRB_CTR.Properties.Resources.Disconnect;
            this.SRB_config.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SRB_config.Name = "SRB_config";
            this.SRB_config.Size = new System.Drawing.Size(39, 27);
            this.SRB_config.Text = "端口配置";
            this.SRB_config.ToolTipText = "Select SRB Port";
            this.SRB_config.ButtonClick += new System.EventHandler(this.SRB_config_ButtonClick);
            // 
            // uSBToolStripMenuItem
            // 
            this.uSBToolStripMenuItem.Name = "uSBToolStripMenuItem";
            this.uSBToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.uSBToolStripMenuItem.Text = "USB";
            this.uSBToolStripMenuItem.Click += new System.EventHandler(this.uSBToolStripMenuItem_Click);
            // 
            // uARTToolStripMenuItem
            // 
            this.uARTToolStripMenuItem.Name = "uARTToolStripMenuItem";
            this.uARTToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.uARTToolStripMenuItem.Text = "UART";
            this.uARTToolStripMenuItem.Click += new System.EventHandler(this.uARTToolStripMenuItem_Click);
            // 
            // ShowRecordBTN_s
            // 
            this.ShowRecordBTN_s.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ShowRecordBTN_s.Image = ((System.Drawing.Image)(resources.GetObject("ShowRecordBTN_s.Image")));
            this.ShowRecordBTN_s.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShowRecordBTN_s.Name = "ShowRecordBTN_s";
            this.ShowRecordBTN_s.Size = new System.Drawing.Size(27, 27);
            this.ShowRecordBTN_s.Text = "Start Access Record";
            this.ShowRecordBTN_s.Click += new System.EventHandler(this.ShowRecordBTN_s_Click);
            // 
            // ShowRecordBTN_r
            // 
            this.ShowRecordBTN_r.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ShowRecordBTN_r.Image = ((System.Drawing.Image)(resources.GetObject("ShowRecordBTN_r.Image")));
            this.ShowRecordBTN_r.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShowRecordBTN_r.Name = "ShowRecordBTN_r";
            this.ShowRecordBTN_r.Size = new System.Drawing.Size(27, 27);
            this.ShowRecordBTN_r.Text = " Stop Access Record ";
            this.ShowRecordBTN_r.Visible = false;
            this.ShowRecordBTN_r.Click += new System.EventHandler(this.ShowRecordBTN_r_Click);
            // 
            // ScanNodeBTN
            // 
            this.ScanNodeBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ScanNodeBTN.Image = ((System.Drawing.Image)(resources.GetObject("ScanNodeBTN.Image")));
            this.ScanNodeBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ScanNodeBTN.Name = "ScanNodeBTN";
            this.ScanNodeBTN.Size = new System.Drawing.Size(27, 27);
            this.ScanNodeBTN.Text = "Search Node";
            this.ScanNodeBTN.Click += new System.EventHandler(this.ScanNodeBTN_Click);
            // 
            // runBTN
            // 
            this.runBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.runBTN.Image = ((System.Drawing.Image)(resources.GetObject("runBTN.Image")));
            this.runBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runBTN.Name = "runBTN";
            this.runBTN.Size = new System.Drawing.Size(27, 27);
            this.runBTN.Text = "Run";
            this.runBTN.Click += new System.EventHandler(this.runBTN_Click);
            // 
            // stopBTN
            // 
            this.stopBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopBTN.Image = ((System.Drawing.Image)(resources.GetObject("stopBTN.Image")));
            this.stopBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopBTN.Name = "stopBTN";
            this.stopBTN.Size = new System.Drawing.Size(27, 27);
            this.stopBTN.Text = "Stop";
            this.stopBTN.Visible = false;
            this.stopBTN.Click += new System.EventHandler(this.stopBTN_Click);
            // 
            // stopAddrShowBTN
            // 
            this.stopAddrShowBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopAddrShowBTN.Image = ((System.Drawing.Image)(resources.GetObject("stopAddrShowBTN.Image")));
            this.stopAddrShowBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopAddrShowBTN.Name = "stopAddrShowBTN";
            this.stopAddrShowBTN.Size = new System.Drawing.Size(27, 27);
            this.stopAddrShowBTN.Text = "Stop Addr Show";
            this.stopAddrShowBTN.Visible = false;
            this.stopAddrShowBTN.Click += new System.EventHandler(this.stopAddrShowBTN_Click);
            // 
            // beginAddrShowBTN
            // 
            this.beginAddrShowBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.beginAddrShowBTN.Image = ((System.Drawing.Image)(resources.GetObject("beginAddrShowBTN.Image")));
            this.beginAddrShowBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.beginAddrShowBTN.Name = "beginAddrShowBTN";
            this.beginAddrShowBTN.Size = new System.Drawing.Size(27, 27);
            this.beginAddrShowBTN.Text = "Begin Addr show";
            this.beginAddrShowBTN.Click += new System.EventHandler(this.beginAddrShowBTN_Click);
            // 
            // updateAllBTN
            // 
            this.updateAllBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.updateAllBTN.Image = ((System.Drawing.Image)(resources.GetObject("updateAllBTN.Image")));
            this.updateAllBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateAllBTN.Name = "updateAllBTN";
            this.updateAllBTN.Size = new System.Drawing.Size(27, 27);
            this.updateAllBTN.Text = "Update All";
            this.updateAllBTN.Click += new System.EventHandler(this.updateAll_Click);
            // 
            // mainTSC
            // 
            this.mainTSC.BottomToolStripPanelVisible = false;
            // 
            // mainTSC.ContentPanel
            // 
            this.mainTSC.ContentPanel.Controls.Add(this.left_UpDownSC);
            this.mainTSC.ContentPanel.Size = new System.Drawing.Size(384, 549);
            this.mainTSC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTSC.LeftToolStripPanelVisible = false;
            this.mainTSC.Location = new System.Drawing.Point(0, 0);
            this.mainTSC.Name = "mainTSC";
            this.mainTSC.RightToolStripPanelVisible = false;
            this.mainTSC.Size = new System.Drawing.Size(384, 579);
            this.mainTSC.TabIndex = 5;
            this.mainTSC.Text = "toolStripContainer1";
            // 
            // mainTSC.TopToolStripPanel
            // 
            this.mainTSC.TopToolStripPanel.Controls.Add(this.nodeScanTS);
            this.mainTSC.TopToolStripPanel.Click += new System.EventHandler(this.mainTSC_TopToolStripPanel_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 601);
            this.Controls.Add(this.mainTSC);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 640);
            this.Name = "mainForm";
            this.Text = "Simple Robot Bus";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.left_UpDownSC.Panel1.ResumeLayout(false);
            this.left_UpDownSC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.left_UpDownSC)).EndInit();
            this.left_UpDownSC.ResumeLayout(false);
            this.nodeScanTS.ResumeLayout(false);
            this.nodeScanTS.PerformLayout();
            this.mainTSC.ContentPanel.ResumeLayout(false);
            this.mainTSC.TopToolStripPanel.ResumeLayout(false);
            this.mainTSC.TopToolStripPanel.PerformLayout();
            this.mainTSC.ResumeLayout(false);
            this.mainTSC.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer uiTIMER;
        private System.Windows.Forms.ToolStripPanel topTSP;
        private System.Windows.Forms.ToolTip NodeTipTT;
        private System.Windows.Forms.ToolStripStatusLabel VersionLAB;
        private System.Windows.Forms.SplitContainer left_UpDownSC;
        private System.Windows.Forms.FlowLayoutPanel frameCounterFLP;
        private System.Windows.Forms.FlowLayoutPanel nodesTable;
        private System.Windows.Forms.ToolStripContainer mainTSC;
        private System.Windows.Forms.ToolStrip nodeScanTS;
        private System.Windows.Forms.ToolStripSplitButton SRB_config;
        private System.Windows.Forms.ToolStripMenuItem uSBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uARTToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ShowRecordBTN_s;
        private System.Windows.Forms.ToolStripButton ShowRecordBTN_r;
        private System.Windows.Forms.ToolStripButton ScanNodeBTN;
        private System.Windows.Forms.ToolStripButton runBTN;
        private System.Windows.Forms.ToolStripButton stopBTN;
        private System.Windows.Forms.ToolStripButton stopAddrShowBTN;
        private System.Windows.Forms.ToolStripButton beginAddrShowBTN;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripButton updateAllBTN;
    }
}