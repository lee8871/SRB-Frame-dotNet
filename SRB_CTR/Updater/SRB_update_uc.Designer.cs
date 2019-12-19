namespace SRB_CTR
{
    partial class UpdateAll_uc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateAll_uc));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.FileBTN = new System.Windows.Forms.ToolStripButton();
            this.gotoUpdateAllBTN = new System.Windows.Forms.ToolStripButton();
            this.GotoUpdateByPowerOnBTN = new System.Windows.Forms.ToolStripButton();
            this.BurnBTN = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(23, 23);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileBTN,
            this.gotoUpdateAllBTN,
            this.GotoUpdateByPowerOnBTN,
            this.BurnBTN});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(300, 30);
            this.toolStrip1.TabIndex = 0;
            // 
            // FileBTN
            // 
            this.FileBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FileBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FileBTN.Image = global::SRB_CTR.Properties.Resources.File;
            this.FileBTN.Name = "FileBTN";
            this.FileBTN.Size = new System.Drawing.Size(27, 27);
            this.FileBTN.Text = "LoadFiles";
            // 
            // gotoUpdateAllBTN
            // 
            this.gotoUpdateAllBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.gotoUpdateAllBTN.Image = ((System.Drawing.Image)(resources.GetObject("gotoUpdateAllBTN.Image")));
            this.gotoUpdateAllBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gotoUpdateAllBTN.Name = "gotoUpdateAllBTN";
            this.gotoUpdateAllBTN.Size = new System.Drawing.Size(27, 27);
            this.gotoUpdateAllBTN.Text = "Update Mode All";
            this.gotoUpdateAllBTN.Click += new System.EventHandler(this.gotoUpdateMode_Click);
            // 
            // GotoUpdateByPowerOnBTN
            // 
            this.GotoUpdateByPowerOnBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GotoUpdateByPowerOnBTN.Image = ((System.Drawing.Image)(resources.GetObject("GotoUpdateByPowerOnBTN.Image")));
            this.GotoUpdateByPowerOnBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GotoUpdateByPowerOnBTN.Name = "GotoUpdateByPowerOnBTN";
            this.GotoUpdateByPowerOnBTN.Size = new System.Drawing.Size(27, 27);
            this.GotoUpdateByPowerOnBTN.Text = "Update Mode All By Power On";
            this.GotoUpdateByPowerOnBTN.Click += new System.EventHandler(this.GotoUpdateByPowerOnBTN_Click);
            // 
            // BurnBTN
            // 
            this.BurnBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BurnBTN.Image = ((System.Drawing.Image)(resources.GetObject("BurnBTN.Image")));
            this.BurnBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BurnBTN.Name = "BurnBTN";
            this.BurnBTN.Size = new System.Drawing.Size(27, 27);
            this.BurnBTN.Text = "Auto Burn All";
            this.BurnBTN.Click += new System.EventHandler(this.BurnBTN_Click);
            // 
            // UpdateAll_uc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Name = "UpdateAll_uc";
            this.Size = new System.Drawing.Size(300, 120);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton FileBTN;
        private System.Windows.Forms.ToolStripButton GotoUpdateByPowerOnBTN;
        private System.Windows.Forms.ToolStripButton gotoUpdateAllBTN;
        private System.Windows.Forms.ToolStripButton BurnBTN;
    }
}