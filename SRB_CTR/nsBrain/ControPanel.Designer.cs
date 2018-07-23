namespace SRB_CTR.nsBrain
{
    partial class ControPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControPanel));
            this.handleBTN = new System.Windows.Forms.Button();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.lockBTN = new System.Windows.Forms.ToolStripButton();
            this.unlockBTN = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // handleBTN
            // 
            this.handleBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.handleBTN.Location = new System.Drawing.Point(12, 149);
            this.handleBTN.Name = "handleBTN";
            this.handleBTN.Size = new System.Drawing.Size(100, 100);
            this.handleBTN.TabIndex = 0;
            this.handleBTN.UseVisualStyleBackColor = true;
            // 
            // mainTimer
            // 
            this.mainTimer.Enabled = true;
            this.mainTimer.Interval = 20;
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lockBTN,
            this.unlockBTN});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(284, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // lockBTN
            // 
            this.lockBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.lockBTN.Image = ((System.Drawing.Image)(resources.GetObject("lockBTN.Image")));
            this.lockBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lockBTN.Name = "lockBTN";
            this.lockBTN.Size = new System.Drawing.Size(24, 24);
            this.lockBTN.Text = "toolStripButton1";
            this.lockBTN.Click += new System.EventHandler(this.lockBTN_Click);
            // 
            // unlockBTN
            // 
            this.unlockBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.unlockBTN.Image = ((System.Drawing.Image)(resources.GetObject("unlockBTN.Image")));
            this.unlockBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.unlockBTN.Name = "unlockBTN";
            this.unlockBTN.Size = new System.Drawing.Size(24, 24);
            this.unlockBTN.Text = "toolStripButton2";
            this.unlockBTN.Click += new System.EventHandler(this.unlockBTN_Click);
            // 
            // ControPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.handleBTN);
            this.Name = "ControPanel";
            this.Text = "ControPanel";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button handleBTN;
        private System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton lockBTN;
        private System.Windows.Forms.ToolStripButton unlockBTN;
    }
}