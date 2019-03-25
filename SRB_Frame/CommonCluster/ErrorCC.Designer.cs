namespace SRB.Frame.Cluster
{
    partial class ErrorCC
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
            this.pageLineL = new System.Windows.Forms.Label();
            this.errorTextL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pageLineL
            // 
            this.pageLineL.AutoSize = true;
            this.pageLineL.Location = new System.Drawing.Point(3, 1);
            this.pageLineL.Name = "pageLineL";
            this.pageLineL.Size = new System.Drawing.Size(101, 12);
            this.pageLineL.TabIndex = 3;
            this.pageLineL.Text = "File:{0} Line{1}";
            // 
            // errorTextL
            // 
            this.errorTextL.AutoSize = true;
            this.errorTextL.Location = new System.Drawing.Point(3, 19);
            this.errorTextL.Name = "errorTextL";
            this.errorTextL.Size = new System.Drawing.Size(41, 12);
            this.errorTextL.TabIndex = 3;
            this.errorTextL.Text = "Error ";
            // 
            // ErrorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.errorTextL);
            this.Controls.Add(this.pageLineL);
            this.Enable_write = false;
            this.Name = "ErrorControl";
            this.Controls.SetChildIndex(this.pageLineL, 0);
            this.Controls.SetChildIndex(this.errorTextL, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label pageLineL;
        private System.Windows.Forms.Label errorTextL;
    }
}
