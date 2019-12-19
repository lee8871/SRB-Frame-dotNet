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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IClusterControl));
            this.readBTN = new System.Windows.Forms.Button();
            this.writeBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // readBTN
            // 
            this.readBTN.BackgroundImage = global::SRB_Frame.Properties.Resources._1175882;
            resources.ApplyResources(this.readBTN, "readBTN");
            this.readBTN.Name = "readBTN";
            this.readBTN.UseVisualStyleBackColor = true;
            this.readBTN.Click += new System.EventHandler(this.OnReadClick);
            // 
            // writeBTN
            // 
            this.writeBTN.BackgroundImage = global::SRB_Frame.Properties.Resources._1175763;
            resources.ApplyResources(this.writeBTN, "writeBTN");
            this.writeBTN.Name = "writeBTN";
            this.writeBTN.UseVisualStyleBackColor = true;
            this.writeBTN.Click += new System.EventHandler(this.OnWriteClick);
            // 
            // IClusterControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.readBTN);
            this.Controls.Add(this.writeBTN);
            this.Name = "IClusterControl";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button readBTN;
        private System.Windows.Forms.Button writeBTN;
    }
}
