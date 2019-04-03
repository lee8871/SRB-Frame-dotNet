namespace SRB.Frame.Cluster.ud
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
            this.label2 = new System.Windows.Forms.Label();
            this.DownRTC = new System.Windows.Forms.RichTextBox();
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
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "Up Mapping:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "Down Mapping:";
            // 
            // DownRTC
            // 
            this.DownRTC.Location = new System.Drawing.Point(6, 77);
            this.DownRTC.MaxLength = 2048;
            this.DownRTC.Name = "DownRTC";
            this.DownRTC.Size = new System.Drawing.Size(201, 40);
            this.DownRTC.TabIndex = 9;
            this.DownRTC.Text = "";
            // 
            // MappingCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DownRTC);
            this.Controls.Add(this.UpRTC);
            this.Name = "MappingCC";
            this.Size = new System.Drawing.Size(300, 120);
            this.Controls.SetChildIndex(this.UpRTC, 0);
            this.Controls.SetChildIndex(this.DownRTC, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox UpRTC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox DownRTC;
    }
}
