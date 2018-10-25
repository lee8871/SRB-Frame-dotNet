namespace SRB_CTR.SRB_Frame.Cluster_error
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
            this.readBTN = new System.Windows.Forms.Button();
            this.pageLineL = new System.Windows.Forms.Label();
            this.errorTextL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // readBTN
            // 
            this.readBTN.BackgroundImage = global::SRB_CTR.Properties.Resources._1175682;
            this.readBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.readBTN.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.readBTN.Location = new System.Drawing.Point(257, 0);
            this.readBTN.Name = "readBTN";
            this.readBTN.Size = new System.Drawing.Size(40, 40);
            this.readBTN.TabIndex = 4;
            this.readBTN.UseVisualStyleBackColor = true;
            this.readBTN.Click += new System.EventHandler(this.read);
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
            // UntypedNodeCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.readBTN);
            this.Controls.Add(this.errorTextL);
            this.Controls.Add(this.pageLineL);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 20);
            this.Name = "UntypedNodeCtrl";
            this.Size = new System.Drawing.Size(300, 43);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button readBTN;
        private System.Windows.Forms.Label pageLineL;
        private System.Windows.Forms.Label errorTextL;
    }
}
