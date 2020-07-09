namespace SRB_CTR
{
    partial class SyncUC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncUC));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.syncBTN = new System.Windows.Forms.ToolStripButton();
            this.syncClearBTN = new System.Windows.Forms.ToolStripButton();
            this.calibrationBTN = new System.Windows.Forms.ToolStripButton();
            this.statusLAB = new System.Windows.Forms.ToolStripLabel();
            this.closeBTN = new System.Windows.Forms.ToolStripButton();
            this.recoedBTN = new System.Windows.Forms.ToolStripButton();
            this.Test_toolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.syncDiffTestBTN = new System.Windows.Forms.ToolStripMenuItem();
            this.infoRTC = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(23, 23);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.syncBTN,
            this.syncClearBTN,
            this.calibrationBTN,
            this.statusLAB,
            this.closeBTN,
            this.recoedBTN,
            this.Test_toolStripDropDownButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(378, 30);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // syncBTN
            // 
            this.syncBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.syncBTN.Image = ((System.Drawing.Image)(resources.GetObject("syncBTN.Image")));
            this.syncBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.syncBTN.Name = "syncBTN";
            this.syncBTN.Size = new System.Drawing.Size(27, 27);
            this.syncBTN.Text = "Scan All Nodes";
            this.syncBTN.Click += new System.EventHandler(this.syncBTN_Click);
            // 
            // syncClearBTN
            // 
            this.syncClearBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.syncClearBTN.Image = ((System.Drawing.Image)(resources.GetObject("syncClearBTN.Image")));
            this.syncClearBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.syncClearBTN.Name = "syncClearBTN";
            this.syncClearBTN.Size = new System.Drawing.Size(27, 27);
            this.syncClearBTN.Text = "clear_sync";
            this.syncClearBTN.Click += new System.EventHandler(this.syncClearBTN_Click);
            // 
            // calibrationBTN
            // 
            this.calibrationBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.calibrationBTN.Image = ((System.Drawing.Image)(resources.GetObject("calibrationBTN.Image")));
            this.calibrationBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.calibrationBTN.Name = "calibrationBTN";
            this.calibrationBTN.Size = new System.Drawing.Size(27, 27);
            this.calibrationBTN.Text = "Celibrate All Node";
            this.calibrationBTN.Click += new System.EventHandler(this.calibrationBTN_Click);
            // 
            // statusLAB
            // 
            this.statusLAB.Name = "statusLAB";
            this.statusLAB.Size = new System.Drawing.Size(74, 27);
            this.statusLAB.Text = "Sync_Statuc";
            // 
            // closeBTN
            // 
            this.closeBTN.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.closeBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.closeBTN.Image = ((System.Drawing.Image)(resources.GetObject("closeBTN.Image")));
            this.closeBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeBTN.Name = "closeBTN";
            this.closeBTN.Size = new System.Drawing.Size(27, 27);
            this.closeBTN.Text = "Close";
            this.closeBTN.Click += new System.EventHandler(this.closeBTN_Click);
            // 
            // recoedBTN
            // 
            this.recoedBTN.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.recoedBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recoedBTN.Image = ((System.Drawing.Image)(resources.GetObject("recoedBTN.Image")));
            this.recoedBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recoedBTN.Name = "recoedBTN";
            this.recoedBTN.Size = new System.Drawing.Size(27, 27);
            this.recoedBTN.Text = "Info";
            this.recoedBTN.Click += new System.EventHandler(this.recoedBTN_Click);
            // 
            // Test_toolStripDropDownButton
            // 
            this.Test_toolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Test_toolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.syncDiffTestBTN});
            this.Test_toolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("Test_toolStripDropDownButton.Image")));
            this.Test_toolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Test_toolStripDropDownButton.Name = "Test_toolStripDropDownButton";
            this.Test_toolStripDropDownButton.Size = new System.Drawing.Size(36, 27);
            this.Test_toolStripDropDownButton.Text = "test";
            // 
            // syncDiffTestBTN
            // 
            this.syncDiffTestBTN.Name = "syncDiffTestBTN";
            this.syncDiffTestBTN.Size = new System.Drawing.Size(180, 22);
            this.syncDiffTestBTN.Text = "Sync Diff Test";
            this.syncDiffTestBTN.Click += new System.EventHandler(this.syncDiffTestBTN_Click);
            // 
            // infoRTC
            // 
            this.infoRTC.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.infoRTC.Location = new System.Drawing.Point(0, 30);
            this.infoRTC.MinimumSize = new System.Drawing.Size(378, 170);
            this.infoRTC.Name = "infoRTC";
            this.infoRTC.Size = new System.Drawing.Size(378, 170);
            this.infoRTC.TabIndex = 1;
            this.infoRTC.Text = "";
            // 
            // SyncUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.infoRTC);
            this.Controls.Add(this.toolStrip1);
            this.MaximumSize = new System.Drawing.Size(378, 200);
            this.MinimumSize = new System.Drawing.Size(378, 32);
            this.Name = "SyncUC";
            this.Size = new System.Drawing.Size(378, 200);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton syncClearBTN;
        private System.Windows.Forms.ToolStripButton calibrationBTN;
        private System.Windows.Forms.ToolStripButton syncBTN;
        private System.Windows.Forms.ToolStripButton closeBTN;
        private System.Windows.Forms.ToolStripButton recoedBTN;
        private System.Windows.Forms.RichTextBox infoRTC;
        private System.Windows.Forms.ToolStripLabel statusLAB;
        private System.Windows.Forms.ToolStripDropDownButton Test_toolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem syncDiffTestBTN;
    }
}
