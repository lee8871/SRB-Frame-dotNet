namespace SRB_CTR.nsBrain
{
    partial class NodesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodesForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.SpeedValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.nodesTable = new System.Windows.Forms.FlowLayoutPanel();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.mainTS = new System.Windows.Forms.ToolStrip();
            this.MasterBroadConfigBTN_uc = new System.Windows.Forms.ToolStripButton();
            this.MasterBroadConfigBTN_c = new System.Windows.Forms.ToolStripButton();
            this.ShowAccessBTN = new System.Windows.Forms.ToolStripButton();
            this.mainTSC = new System.Windows.Forms.ToolStripContainer();
            this.brainTS = new System.Windows.Forms.ToolStrip();
            this.brainRunBTN = new System.Windows.Forms.ToolStripButton();
            this.brainStopBTN = new System.Windows.Forms.ToolStripButton();
            this.cycleTB = new System.Windows.Forms.ToolStripTextBox();
            this.statusStrip1.SuspendLayout();
            this.mainTS.SuspendLayout();
            this.mainTSC.ContentPanel.SuspendLayout();
            this.mainTSC.TopToolStripPanel.SuspendLayout();
            this.mainTSC.SuspendLayout();
            this.brainTS.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SpeedValue});
            this.statusStrip1.Location = new System.Drawing.Point(0, 560);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(601, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // SpeedValue
            // 
            this.SpeedValue.Name = "SpeedValue";
            this.SpeedValue.Size = new System.Drawing.Size(170, 17);
            this.SpeedValue.Text = "运算耗时：{0}，发包耗时：{1}";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // nodesTable
            // 
            this.nodesTable.BackColor = System.Drawing.SystemColors.ControlDark;
            this.nodesTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodesTable.Location = new System.Drawing.Point(0, 0);
            this.nodesTable.Name = "nodesTable";
            this.nodesTable.Size = new System.Drawing.Size(601, 530);
            this.nodesTable.TabIndex = 0;
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            this.LeftToolStripPanel.UseWaitCursor = true;
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(551, 557);
            // 
            // mainTS
            // 
            this.mainTS.Dock = System.Windows.Forms.DockStyle.None;
            this.mainTS.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MasterBroadConfigBTN_uc,
            this.MasterBroadConfigBTN_c,
            this.ShowAccessBTN});
            this.mainTS.Location = new System.Drawing.Point(3, 0);
            this.mainTS.Name = "mainTS";
            this.mainTS.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mainTS.Size = new System.Drawing.Size(84, 27);
            this.mainTS.TabIndex = 2;
            this.mainTS.Text = "Bus config";
            // 
            // MasterBroadConfigBTN_uc
            // 
            this.MasterBroadConfigBTN_uc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MasterBroadConfigBTN_uc.Image = ((System.Drawing.Image)(resources.GetObject("MasterBroadConfigBTN_uc.Image")));
            this.MasterBroadConfigBTN_uc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MasterBroadConfigBTN_uc.Name = "MasterBroadConfigBTN_uc";
            this.MasterBroadConfigBTN_uc.Size = new System.Drawing.Size(24, 24);
            this.MasterBroadConfigBTN_uc.Text = "端口配置";
            this.MasterBroadConfigBTN_uc.Click += new System.EventHandler(this.MasterBroadConfigBTN_Click);
            // 
            // MasterBroadConfigBTN_c
            // 
            this.MasterBroadConfigBTN_c.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MasterBroadConfigBTN_c.Image = ((System.Drawing.Image)(resources.GetObject("MasterBroadConfigBTN_c.Image")));
            this.MasterBroadConfigBTN_c.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MasterBroadConfigBTN_c.Name = "MasterBroadConfigBTN_c";
            this.MasterBroadConfigBTN_c.Size = new System.Drawing.Size(24, 24);
            this.MasterBroadConfigBTN_c.Text = "端口配置";
            this.MasterBroadConfigBTN_c.Click += new System.EventHandler(this.MasterBroadConfigBTN_Click);
            // 
            // ShowAccessBTN
            // 
            this.ShowAccessBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ShowAccessBTN.Image = ((System.Drawing.Image)(resources.GetObject("ShowAccessBTN.Image")));
            this.ShowAccessBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShowAccessBTN.Name = "ShowAccessBTN";
            this.ShowAccessBTN.Size = new System.Drawing.Size(24, 24);
            this.ShowAccessBTN.Text = "打开监视窗口";
            this.ShowAccessBTN.Click += new System.EventHandler(this.ShowAccessBTN_Click);
            // 
            // mainTSC
            // 
            this.mainTSC.BottomToolStripPanelVisible = false;
            // 
            // mainTSC.ContentPanel
            // 
            this.mainTSC.ContentPanel.Controls.Add(this.nodesTable);
            this.mainTSC.ContentPanel.Size = new System.Drawing.Size(601, 530);
            this.mainTSC.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainTSC.Location = new System.Drawing.Point(0, 0);
            this.mainTSC.Name = "mainTSC";
            this.mainTSC.Size = new System.Drawing.Size(601, 557);
            this.mainTSC.TabIndex = 0;
            this.mainTSC.TabStop = false;
            // 
            // mainTSC.TopToolStripPanel
            // 
            this.mainTSC.TopToolStripPanel.Controls.Add(this.mainTS);
            this.mainTSC.TopToolStripPanel.Controls.Add(this.brainTS);
            // 
            // brainTS
            // 
            this.brainTS.Dock = System.Windows.Forms.DockStyle.None;
            this.brainTS.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.brainTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.brainRunBTN,
            this.brainStopBTN,
            this.cycleTB});
            this.brainTS.Location = new System.Drawing.Point(87, 0);
            this.brainTS.Name = "brainTS";
            this.brainTS.Size = new System.Drawing.Size(193, 27);
            this.brainTS.TabIndex = 3;
            // 
            // brainRunBTN
            // 
            this.brainRunBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.brainRunBTN.Image = ((System.Drawing.Image)(resources.GetObject("brainRunBTN.Image")));
            this.brainRunBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.brainRunBTN.Name = "brainRunBTN";
            this.brainRunBTN.Size = new System.Drawing.Size(24, 24);
            this.brainRunBTN.Text = "toolStripButton3";
            // 
            // brainStopBTN
            // 
            this.brainStopBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.brainStopBTN.Image = ((System.Drawing.Image)(resources.GetObject("brainStopBTN.Image")));
            this.brainStopBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.brainStopBTN.Name = "brainStopBTN";
            this.brainStopBTN.Size = new System.Drawing.Size(24, 24);
            this.brainStopBTN.Text = "toolStripButton1";
            // 
            // cycleTB
            // 
            this.cycleTB.MaxLength = 6;
            this.cycleTB.Name = "cycleTB";
            this.cycleTB.Size = new System.Drawing.Size(100, 27);
            this.cycleTB.Text = "20";
            // 
            // NodesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 582);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mainTSC);
            this.Name = "NodesForm";
            this.Text = "NodesForm";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mainTS.ResumeLayout(false);
            this.mainTS.PerformLayout();
            this.mainTSC.ContentPanel.ResumeLayout(false);
            this.mainTSC.TopToolStripPanel.ResumeLayout(false);
            this.mainTSC.TopToolStripPanel.PerformLayout();
            this.mainTSC.ResumeLayout(false);
            this.mainTSC.PerformLayout();
            this.brainTS.ResumeLayout(false);
            this.brainTS.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel SpeedValue;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.FlowLayoutPanel nodesTable;
        private System.Windows.Forms.ToolStrip mainTS;
        private System.Windows.Forms.ToolStripButton MasterBroadConfigBTN_uc;
        private System.Windows.Forms.ToolStripButton MasterBroadConfigBTN_c;
        private System.Windows.Forms.ToolStripButton ShowAccessBTN;
        private System.Windows.Forms.ToolStripContainer mainTSC;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.ToolStrip brainTS;
        private System.Windows.Forms.ToolStripButton brainRunBTN;
        private System.Windows.Forms.ToolStripButton brainStopBTN;
        private System.Windows.Forms.ToolStripTextBox cycleTB;
    }
}