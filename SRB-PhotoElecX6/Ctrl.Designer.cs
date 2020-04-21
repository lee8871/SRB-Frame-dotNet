namespace SRB.NodeType.PhotoElecX6
{
    partial class Ctrl
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
            this.countBTN = new System.Windows.Forms.Button();
            this.RAWBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // countBTN
            // 
            this.countBTN.Location = new System.Drawing.Point(231, 39);
            this.countBTN.Name = "countBTN";
            this.countBTN.Size = new System.Drawing.Size(66, 31);
            this.countBTN.TabIndex = 29;
            this.countBTN.Text = "0";
            this.countBTN.UseVisualStyleBackColor = true;
            this.countBTN.Click += new System.EventHandler(this.countBTN_Click);
            // 
            // RAWBTN
            // 
            this.RAWBTN.Location = new System.Drawing.Point(147, 39);
            this.RAWBTN.Name = "RAWBTN";
            this.RAWBTN.Size = new System.Drawing.Size(78, 31);
            this.RAWBTN.TabIndex = 30;
            this.RAWBTN.Text = "RAW export";
            this.RAWBTN.UseVisualStyleBackColor = true;
            this.RAWBTN.Click += new System.EventHandler(this.RAWBTN_Click);
            // 
            // Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RAWBTN);
            this.Controls.Add(this.countBTN);
            this.MinimumSize = new System.Drawing.Size(300, 0);
            this.Name = "Ctrl";
            this.Size = new System.Drawing.Size(300, 73);
            this.Controls.SetChildIndex(this.HelpBTN, 0);
            this.Controls.SetChildIndex(this.RunStopBTN, 0);
            this.Controls.SetChildIndex(this.countBTN, 0);
            this.Controls.SetChildIndex(this.RAWBTN, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button countBTN;
        private System.Windows.Forms.Button RAWBTN;
    }
}
