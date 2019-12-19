namespace SRB_CTR.nsBrain.Node_Test001
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
            this.ColorLed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ColorLed
            // 
            this.ColorLed.AutoSize = true;
            this.ColorLed.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ColorLed.Location = new System.Drawing.Point(-6, 0);
            this.ColorLed.Name = "ColorLed";
            this.ColorLed.Size = new System.Drawing.Size(47, 33);
            this.ColorLed.TabIndex = 0;
            this.ColorLed.Text = "●";
            // 
            // UntypedNodeCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.ColorLed);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.Name = "UntypedNodeCtrl";
            this.Size = new System.Drawing.Size(44, 33);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ColorLed;
    }
}
