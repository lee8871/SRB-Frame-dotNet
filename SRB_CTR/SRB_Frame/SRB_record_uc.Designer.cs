namespace SRB_CTR.SRB_Frame
{
    partial class SRB_record_uc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SRB_record_uc));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.recordBTN = new System.Windows.Forms.ToolStripButton();
            this.fileName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(23, 23);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.recordBTN,
            this.fileName});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(300, 30);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(50, 27);
            this.toolStripLabel1.Text = "Record";
            // 
            // recordBTN
            // 
            this.recordBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.recordBTN.Image = ((System.Drawing.Image)(resources.GetObject("recordBTN.Image")));
            this.recordBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.recordBTN.Name = "recordBTN";
            this.recordBTN.Size = new System.Drawing.Size(27, 27);
            this.recordBTN.Text = "正在记录串口数据，点击关闭";
            // 
            // fileName
            // 
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(100, 30);
            // 
            // SRB_record_uc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.MaximumSize = new System.Drawing.Size(300, 32);
            this.MinimumSize = new System.Drawing.Size(300, 32);
            this.Name = "SRB_record_uc";
            this.Size = new System.Drawing.Size(300, 32);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton recordBTN;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox fileName;
    }
}