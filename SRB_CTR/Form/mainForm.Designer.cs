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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.VersionLAB = new System.Windows.Forms.ToolStripStatusLabel();
            this.uiTIMER = new System.Windows.Forms.Timer(this.components);
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.NodeTipTT = new System.Windows.Forms.ToolTip(this.components);
            this.mainSC = new System.Windows.Forms.SplitContainer();
            this.frameCounterFLP = new System.Windows.Forms.FlowLayoutPanel();
            this.nodesTable = new System.Windows.Forms.FlowLayoutPanel();
            this.nodeScanTS = new System.Windows.Forms.ToolStrip();
            this.SRB_config = new System.Windows.Forms.ToolStripSplitButton();
            this.uSBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uARTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowRecordBTN = new System.Windows.Forms.ToolStripButton();
            this.ScanNodeBTN = new System.Windows.Forms.ToolStripButton();
            this.runBTN = new System.Windows.Forms.ToolStripButton();
            this.stopBTN = new System.Windows.Forms.ToolStripButton();
            this.AddrShowBTN = new System.Windows.Forms.ToolStripButton();
            this.SyncBTN = new System.Windows.Forms.ToolStripButton();
            this.updateAllBTN = new System.Windows.Forms.ToolStripButton();
            this.mainTSC = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSC)).BeginInit();
            this.mainSC.Panel1.SuspendLayout();
            this.mainSC.Panel2.SuspendLayout();
            this.mainSC.SuspendLayout();
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 616);
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
            // mainSC
            // 
            this.mainSC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSC.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mainSC.Location = new System.Drawing.Point(0, 0);
            this.mainSC.Name = "mainSC";
            this.mainSC.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // mainSC.Panel1
            // 
            this.mainSC.Panel1.Controls.Add(this.frameCounterFLP);
            this.mainSC.Panel1MinSize = 0;
            // 
            // mainSC.Panel2
            // 
            this.mainSC.Panel2.Controls.Add(this.nodesTable);
            this.mainSC.Panel2MinSize = 250;
            this.mainSC.Size = new System.Drawing.Size(384, 586);
            this.mainSC.SplitterDistance = 25;
            this.mainSC.SplitterWidth = 8;
            this.mainSC.TabIndex = 0;
            // 
            // frameCounterFLP
            // 
            this.frameCounterFLP.AutoSize = true;
            this.frameCounterFLP.BackColor = System.Drawing.Color.PowderBlue;
            this.frameCounterFLP.Dock = System.Windows.Forms.DockStyle.Top;
            this.frameCounterFLP.Location = new System.Drawing.Point(0, 0);
            this.frameCounterFLP.Name = "frameCounterFLP";
            this.frameCounterFLP.Size = new System.Drawing.Size(384, 0);
            this.frameCounterFLP.TabIndex = 0;
            // 
            // nodesTable
            // 
            this.nodesTable.AutoSize = true;
            this.nodesTable.BackColor = System.Drawing.Color.SkyBlue;
            this.nodesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodesTable.Location = new System.Drawing.Point(0, 0);
            this.nodesTable.Name = "nodesTable";
            this.nodesTable.Size = new System.Drawing.Size(384, 553);
            this.nodesTable.TabIndex = 0;
            // 
            // nodeScanTS
            // 
            this.nodeScanTS.BackColor = System.Drawing.SystemColors.Control;
            this.nodeScanTS.Dock = System.Windows.Forms.DockStyle.None;
            this.nodeScanTS.ImageScalingSize = new System.Drawing.Size(23, 23);
            this.nodeScanTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SRB_config,
            this.ShowRecordBTN,
            this.ScanNodeBTN,
            this.runBTN,
            this.stopBTN,
            this.AddrShowBTN,
            this.SyncBTN,
            this.updateAllBTN});
            this.nodeScanTS.Location = new System.Drawing.Point(3, 0);
            this.nodeScanTS.Name = "nodeScanTS";
            this.nodeScanTS.Size = new System.Drawing.Size(271, 30);
            this.nodeScanTS.TabIndex = 4;
            // 
            // SRB_config
            // 
            this.SRB_config.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SRB_config.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uSBToolStripMenuItem,
            this.uARTToolStripMenuItem});
            this.SRB_config.Image = global::SRB_CTR.Properties.Resources.disconnect;
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
            // ShowRecordBTN
            // 
            this.ShowRecordBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ShowRecordBTN.Image = global::SRB_CTR.Properties.Resources.record0;
            this.ShowRecordBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShowRecordBTN.Name = "ShowRecordBTN";
            this.ShowRecordBTN.Size = new System.Drawing.Size(27, 27);
            this.ShowRecordBTN.Text = "Start Access Record";
            this.ShowRecordBTN.Click += new System.EventHandler(this.ShowRecordBTN_Click);
            // 
            // ScanNodeBTN
            // 
            this.ScanNodeBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ScanNodeBTN.Image = global::SRB_CTR.Properties.Resources.Scan;
            this.ScanNodeBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ScanNodeBTN.Name = "ScanNodeBTN";
            this.ScanNodeBTN.Size = new System.Drawing.Size(27, 27);
            this.ScanNodeBTN.Text = "Search Node";
            this.ScanNodeBTN.Click += new System.EventHandler(this.ScanNodeBTN_Click);
            // 
            // runBTN
            // 
            this.runBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.runBTN.Image = global::SRB_CTR.Properties.Resources.run;
            this.runBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runBTN.Name = "runBTN";
            this.runBTN.Size = new System.Drawing.Size(27, 27);
            this.runBTN.Text = "Run";
            this.runBTN.Click += new System.EventHandler(this.runBTN_Click);
            // 
            // stopBTN
            // 
            this.stopBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stopBTN.Image = global::SRB_CTR.Properties.Resources.Pause;
            this.stopBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopBTN.Name = "stopBTN";
            this.stopBTN.Size = new System.Drawing.Size(27, 27);
            this.stopBTN.Text = "Stop";
            this.stopBTN.Visible = false;
            this.stopBTN.Click += new System.EventHandler(this.stopBTN_Click);
            // 
            // AddrShowBTN
            // 
            this.AddrShowBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddrShowBTN.Image = global::SRB_CTR.Properties.Resources.AddrLed0;
            this.AddrShowBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddrShowBTN.Name = "AddrShowBTN";
            this.AddrShowBTN.Size = new System.Drawing.Size(27, 27);
            this.AddrShowBTN.Text = "Begin Addr show";
            this.AddrShowBTN.Click += new System.EventHandler(this.AddrShowBTN_Click);
            // 
            // SyncBTN
            // 
            this.SyncBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SyncBTN.Image = global::SRB_CTR.Properties.Resources.clock;
            this.SyncBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SyncBTN.Name = "SyncBTN";
            this.SyncBTN.Size = new System.Drawing.Size(27, 27);
            this.SyncBTN.Text = "Sync and celibrat";
            this.SyncBTN.Click += new System.EventHandler(this.SyncBTN_Click);
            // 
            // updateAllBTN
            // 
            this.updateAllBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.updateAllBTN.Image = global::SRB_CTR.Properties.Resources.update;
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
            this.mainTSC.ContentPanel.Controls.Add(this.mainSC);
            this.mainTSC.ContentPanel.Size = new System.Drawing.Size(384, 586);
            this.mainTSC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTSC.LeftToolStripPanelVisible = false;
            this.mainTSC.Location = new System.Drawing.Point(0, 0);
            this.mainTSC.Name = "mainTSC";
            this.mainTSC.RightToolStripPanelVisible = false;
            this.mainTSC.Size = new System.Drawing.Size(384, 616);
            this.mainTSC.TabIndex = 5;
            this.mainTSC.Text = "toolStripContainer1";
            // 
            // mainTSC.TopToolStripPanel
            // 
            this.mainTSC.TopToolStripPanel.Controls.Add(this.nodeScanTS);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 638);
            this.Controls.Add(this.mainTSC);
            this.Controls.Add(this.statusStrip1);
            this.Icon = global::SRB_CTR.Properties.Resources.SRB;
            this.MinimumSize = new System.Drawing.Size(400, 640);
            this.Name = "mainForm";
            this.Text = "Simple Robot Bus";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mainSC.Panel1.ResumeLayout(false);
            this.mainSC.Panel1.PerformLayout();
            this.mainSC.Panel2.ResumeLayout(false);
            this.mainSC.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSC)).EndInit();
            this.mainSC.ResumeLayout(false);
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
        private System.Windows.Forms.ToolTip NodeTipTT;
        private System.Windows.Forms.ToolStripStatusLabel VersionLAB;
        private System.Windows.Forms.SplitContainer mainSC;
        private System.Windows.Forms.FlowLayoutPanel frameCounterFLP;
        private System.Windows.Forms.FlowLayoutPanel nodesTable;
        private System.Windows.Forms.ToolStripContainer mainTSC;
        private System.Windows.Forms.ToolStrip nodeScanTS;
        private System.Windows.Forms.ToolStripSplitButton SRB_config;
        private System.Windows.Forms.ToolStripMenuItem uSBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uARTToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton ShowRecordBTN;
        private System.Windows.Forms.ToolStripButton ScanNodeBTN;
        private System.Windows.Forms.ToolStripButton runBTN;
        private System.Windows.Forms.ToolStripButton stopBTN;
        private System.Windows.Forms.ToolStripButton AddrShowBTN;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStripButton updateAllBTN;
        private System.Windows.Forms.ToolStripButton SyncBTN;
    }
}