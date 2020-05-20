namespace SRB_CTR
{
    partial class recordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(recordForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SaveBTN = new System.Windows.Forms.ToolStripButton();
            this.infoRTC = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(23, 23);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveBTN});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(315, 30);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SaveBTN
            // 
            this.SaveBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveBTN.Image = ((System.Drawing.Image)(resources.GetObject("SaveBTN.Image")));
            this.SaveBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveBTN.Name = "SaveBTN";
            this.SaveBTN.Size = new System.Drawing.Size(27, 27);
            this.SaveBTN.Text = "saveLog";
            // 
            // infoRTC
            // 
            this.infoRTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoRTC.Location = new System.Drawing.Point(0, 30);
            this.infoRTC.Name = "infoRTC";
            this.infoRTC.Size = new System.Drawing.Size(315, 333);
            this.infoRTC.TabIndex = 2;
            this.infoRTC.Text = "";
            // 
            // recordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 363);
            this.Controls.Add(this.infoRTC);
            this.Controls.Add(this.toolStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "recordForm";
            this.ShowIcon = false;
            this.Text = "Record Form";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton SaveBTN;
        private System.Windows.Forms.RichTextBox infoRTC;
    }
}