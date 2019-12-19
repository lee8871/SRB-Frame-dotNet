namespace SRB.Frame
{
    partial class MappingCC
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
            this.UpRTC = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StatusLAB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UpRTC
            // 
            this.UpRTC.Location = new System.Drawing.Point(6, 19);
            this.UpRTC.MaxLength = 2048;
            this.UpRTC.Name = "UpRTC";
            this.UpRTC.Size = new System.Drawing.Size(201, 40);
            this.UpRTC.TabIndex = 8;
            this.UpRTC.Text = "";
            this.UpRTC.TextChanged += new System.EventHandler(this.UpRTC_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "Mapping:";
            // 
            // StatusLAB
            // 
            this.StatusLAB.AutoSize = true;
            this.StatusLAB.Location = new System.Drawing.Point(7, 64);
            this.StatusLAB.Name = "StatusLAB";
            this.StatusLAB.Size = new System.Drawing.Size(41, 12);
            this.StatusLAB.TabIndex = 11;
            this.StatusLAB.Text = "Status";
            // 
            // MappingCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.StatusLAB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UpRTC);
            this.Name = "MappingCC";
            this.Size = new System.Drawing.Size(300, 76);
            this.Controls.SetChildIndex(this.UpRTC, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.StatusLAB, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox UpRTC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label StatusLAB;
    }
}
