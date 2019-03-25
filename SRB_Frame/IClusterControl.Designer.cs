namespace SRB.Frame
{
    partial class IClusterControl
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
            this.readBTN = new System.Windows.Forms.Button();
            this.writeBTN = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // readBTN
            // 
            this.readBTN.BackgroundImage = global::SRB_Frame.Properties.Resources._11756821;
            this.readBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.readBTN.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.readBTN.Location = new System.Drawing.Point(257, 0);
            this.readBTN.Name = "readBTN";
            this.readBTN.Size = new System.Drawing.Size(40, 40);
            this.readBTN.TabIndex = 6;
            this.readBTN.UseVisualStyleBackColor = true;
            this.readBTN.Click += new System.EventHandler(this.OnReadClick);
            // 
            // writeBTN
            // 
            this.writeBTN.BackgroundImage = global::SRB_Frame.Properties.Resources._11757631;
            this.writeBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.writeBTN.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.writeBTN.Location = new System.Drawing.Point(214, 0);
            this.writeBTN.Name = "writeBTN";
            this.writeBTN.Size = new System.Drawing.Size(40, 40);
            this.writeBTN.TabIndex = 5;
            this.writeBTN.UseVisualStyleBackColor = true;
            this.writeBTN.Click += new System.EventHandler(this.OnWriteClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // IClusterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.readBTN);
            this.Controls.Add(this.writeBTN);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 20);
            this.Name = "IClusterControl";
            this.Size = new System.Drawing.Size(300, 43);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button readBTN;
        private System.Windows.Forms.Button writeBTN;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
