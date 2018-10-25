namespace SRB_CTR.SRB_Frame
{
    partial class SRB_master_uart_uc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SRB_master_uart_uc));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CloseRecordDataBTN = new System.Windows.Forms.ToolStripButton();
            this.OpenRecordDataBTN = new System.Windows.Forms.ToolStripButton();
            this.CloseConnectBTN = new System.Windows.Forms.ToolStripButton();
            this.comSelectCB = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(23, 23);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseRecordDataBTN,
            this.OpenRecordDataBTN,
            this.CloseConnectBTN,
            this.comSelectCB});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(300, 30);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // CloseRecordDataBTN
            // 
            this.CloseRecordDataBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CloseRecordDataBTN.Image = ((System.Drawing.Image)(resources.GetObject("CloseRecordDataBTN.Image")));
            this.CloseRecordDataBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseRecordDataBTN.Name = "CloseRecordDataBTN";
            this.CloseRecordDataBTN.Size = new System.Drawing.Size(27, 27);
            this.CloseRecordDataBTN.Text = "正在记录串口数据，点击关闭";
            this.CloseRecordDataBTN.Visible = false;
            this.CloseRecordDataBTN.Click += new System.EventHandler(this.CloseRecordDataBTN_Click);
            // 
            // OpenRecordDataBTN
            // 
            this.OpenRecordDataBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenRecordDataBTN.Image = ((System.Drawing.Image)(resources.GetObject("OpenRecordDataBTN.Image")));
            this.OpenRecordDataBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenRecordDataBTN.Name = "OpenRecordDataBTN";
            this.OpenRecordDataBTN.Size = new System.Drawing.Size(27, 27);
            this.OpenRecordDataBTN.Text = "开始串口记录串口数据";
            this.OpenRecordDataBTN.Click += new System.EventHandler(this.OpenRecordDataBTN_Click);
            // 
            // CloseConnectBTN
            // 
            this.CloseConnectBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CloseConnectBTN.Image = ((System.Drawing.Image)(resources.GetObject("CloseConnectBTN.Image")));
            this.CloseConnectBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseConnectBTN.Name = "CloseConnectBTN";
            this.CloseConnectBTN.Size = new System.Drawing.Size(27, 27);
            this.CloseConnectBTN.Text = "断开串口";
            this.CloseConnectBTN.Click += new System.EventHandler(this.CloseConnectBTN_Click);
            // 
            // comSelectCB
            // 
            this.comSelectCB.MaxDropDownItems = 16;
            this.comSelectCB.Name = "comSelectCB";
            this.comSelectCB.Size = new System.Drawing.Size(75, 30);
            // 
            // SRB_master_uart_uc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.MaximumSize = new System.Drawing.Size(300, 32);
            this.MinimumSize = new System.Drawing.Size(300, 32);
            this.Name = "SRB_master_uart_uc";
            this.Size = new System.Drawing.Size(300, 32);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox comSelectCB;
        private System.Windows.Forms.ToolStripButton CloseRecordDataBTN;
        private System.Windows.Forms.ToolStripButton OpenRecordDataBTN;
        private System.Windows.Forms.ToolStripButton CloseConnectBTN;


    }
}