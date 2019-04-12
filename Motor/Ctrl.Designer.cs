namespace SRB.NodeType.Du_motor
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
            this.handleBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // handleBTN
            // 
            this.handleBTN.Location = new System.Drawing.Point(6, 26);
            this.handleBTN.Name = "handleBTN";
            this.handleBTN.Size = new System.Drawing.Size(145, 94);
            this.handleBTN.TabIndex = 9;
            this.handleBTN.Text = "双电机控制器";
            this.handleBTN.UseVisualStyleBackColor = true;
            this.handleBTN.Click += new System.EventHandler(this.handleBTN_Click);
            // 
            // Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.handleBTN);
            this.MinimumSize = new System.Drawing.Size(300, 0);
            this.Name = "Ctrl";
            this.Size = new System.Drawing.Size(300, 123);
            this.Controls.SetChildIndex(this.handleBTN, 0);
            this.Controls.SetChildIndex(this.RunStopBTN, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button handleBTN;

    }
}
