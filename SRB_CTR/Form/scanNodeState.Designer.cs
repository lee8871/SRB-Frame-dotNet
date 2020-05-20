namespace SRB_CTR
{
    partial class scanNodeState
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(scanNodeState));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.StartBTN = new System.Windows.Forms.ToolStripButton();
            this.StopBTN = new System.Windows.Forms.ToolStripButton();
            this.scanPB = new System.Windows.Forms.ToolStripProgressBar();
            this.AutoSetAddressBTN = new System.Windows.Forms.ToolStripButton();
            this.RandomNewNodeBTN = new System.Windows.Forms.ToolStripButton();
            this.randomAllAddressBTN = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(23, 23);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartBTN,
            this.StopBTN,
            this.scanPB,
            this.AutoSetAddressBTN,
            this.RandomNewNodeBTN,
            this.randomAllAddressBTN});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(378, 32);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // StartBTN
            // 
            this.StartBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartBTN.Image = ((System.Drawing.Image)(resources.GetObject("StartBTN.Image")));
            this.StartBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StartBTN.Name = "StartBTN";
            this.StartBTN.Size = new System.Drawing.Size(27, 29);
            this.StartBTN.Text = "Scan Nodes";
            this.StartBTN.Click += new System.EventHandler(this.StartBTN_Click);
            // 
            // StopBTN
            // 
            this.StopBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopBTN.Image = ((System.Drawing.Image)(resources.GetObject("StopBTN.Image")));
            this.StopBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopBTN.Name = "StopBTN";
            this.StopBTN.Size = new System.Drawing.Size(27, 29);
            this.StopBTN.Text = "Stop Scaning";
            this.StopBTN.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // scanPB
            // 
            this.scanPB.Name = "scanPB";
            this.scanPB.Size = new System.Drawing.Size(100, 29);
            this.scanPB.ToolTipText = "Scan Progress";
            this.scanPB.Value = 25;
            // 
            // AutoSetAddressBTN
            // 
            this.AutoSetAddressBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AutoSetAddressBTN.Image = ((System.Drawing.Image)(resources.GetObject("AutoSetAddressBTN.Image")));
            this.AutoSetAddressBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AutoSetAddressBTN.Name = "AutoSetAddressBTN";
            this.AutoSetAddressBTN.Size = new System.Drawing.Size(27, 29);
            this.AutoSetAddressBTN.Text = "Auto Set All Address";
            this.AutoSetAddressBTN.Click += new System.EventHandler(this.AutoSetAddressBTN_Click);
            // 
            // RandomNewNodeBTN
            // 
            this.RandomNewNodeBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RandomNewNodeBTN.Image = ((System.Drawing.Image)(resources.GetObject("RandomNewNodeBTN.Image")));
            this.RandomNewNodeBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RandomNewNodeBTN.Name = "RandomNewNodeBTN";
            this.RandomNewNodeBTN.Size = new System.Drawing.Size(27, 29);
            this.RandomNewNodeBTN.Text = "Random New Nodes";
            this.RandomNewNodeBTN.Click += new System.EventHandler(this.RandomNewNodeBTN_Click);
            // 
            // randomAllAddressBTN
            // 
            this.randomAllAddressBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.randomAllAddressBTN.Image = ((System.Drawing.Image)(resources.GetObject("randomAllAddressBTN.Image")));
            this.randomAllAddressBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.randomAllAddressBTN.Name = "randomAllAddressBTN";
            this.randomAllAddressBTN.Size = new System.Drawing.Size(27, 29);
            this.randomAllAddressBTN.Text = "Random All Address";
            this.randomAllAddressBTN.Click += new System.EventHandler(this.randomAllAddressBTN_Click);
            // 
            // scanNodeState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.toolStrip1);
            this.MaximumSize = new System.Drawing.Size(378, 32);
            this.MinimumSize = new System.Drawing.Size(378, 32);
            this.Name = "scanNodeState";
            this.Size = new System.Drawing.Size(378, 32);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton StopBTN;
        private System.Windows.Forms.ToolStripProgressBar scanPB;
        private System.Windows.Forms.ToolStripButton AutoSetAddressBTN;
        private System.Windows.Forms.ToolStripButton randomAllAddressBTN;
        private System.Windows.Forms.ToolStripButton RandomNewNodeBTN;
        private System.Windows.Forms.ToolStripButton StartBTN;
    }
}
