namespace SRB.NodeType.Charger
{
    partial class InnResCC
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
            this.valueRTC = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // valueRTC
            // 
            this.valueRTC.Location = new System.Drawing.Point(4, 0);
            this.valueRTC.Name = "valueRTC";
            this.valueRTC.Size = new System.Drawing.Size(204, 98);
            this.valueRTC.TabIndex = 7;
            this.valueRTC.Text = "";
            // 
            // InnResCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.valueRTC);
            this.Name = "InnResCC";
            this.Size = new System.Drawing.Size(300, 101);
            this.Load += new System.EventHandler(this.BatteryCC_Load);
            this.Controls.SetChildIndex(this.valueRTC, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox valueRTC;
    }
}
