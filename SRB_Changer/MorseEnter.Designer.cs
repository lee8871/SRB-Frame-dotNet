namespace SRB.NodeType.Charger
{
    partial class MorseEnter
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.MorseCharTB = new System.Windows.Forms.TextBox();
            this.MorseTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MorseCharTB
            // 
            this.MorseCharTB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MorseCharTB.Location = new System.Drawing.Point(0, -2);
            this.MorseCharTB.MaxLength = 1;
            this.MorseCharTB.Name = "MorseCharTB";
            this.MorseCharTB.Size = new System.Drawing.Size(23, 26);
            this.MorseCharTB.TabIndex = 5;
            this.MorseCharTB.Text = "F";
            this.MorseCharTB.TextChanged += new System.EventHandler(this.MorseCharTB_TextChanged);
            // 
            // MorseTB
            // 
            this.MorseTB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MorseTB.Location = new System.Drawing.Point(29, -2);
            this.MorseTB.MaxLength = 7;
            this.MorseTB.Name = "MorseTB";
            this.MorseTB.Size = new System.Drawing.Size(70, 26);
            this.MorseTB.TabIndex = 6;
            this.MorseTB.Text = "..-.";
            this.MorseTB.TextChanged += new System.EventHandler(this.MorseTB_TextChanged);
            // 
            // MorseEnter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MorseCharTB);
            this.Controls.Add(this.MorseTB);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MorseEnter";
            this.Size = new System.Drawing.Size(99, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MorseCharTB;
        private System.Windows.Forms.TextBox MorseTB;
    }
}
