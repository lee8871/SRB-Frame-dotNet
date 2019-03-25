namespace SRB.Frame.Cluster
{
    partial class InformationCC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformationCC));
            this.versionL = new System.Windows.Forms.Label();
            this.typeL = new System.Windows.Forms.Label();
            this.factorySettingBTN = new System.Windows.Forms.Button();
            this.ResetNodeBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // versionL
            // 
            resources.ApplyResources(this.versionL, "versionL");
            this.versionL.Name = "versionL";
            // 
            // typeL
            // 
            resources.ApplyResources(this.typeL, "typeL");
            this.typeL.Name = "typeL";
            // 
            // factorySettingBTN
            // 
            this.factorySettingBTN.BackColor = System.Drawing.Color.Crimson;
            resources.ApplyResources(this.factorySettingBTN, "factorySettingBTN");
            this.factorySettingBTN.ForeColor = System.Drawing.Color.Crimson;
            this.factorySettingBTN.Name = "factorySettingBTN";
            this.factorySettingBTN.UseVisualStyleBackColor = false;
            this.factorySettingBTN.Click += new System.EventHandler(this.factorySettingBTN_Click);
            // 
            // ResetNodeBTN
            // 
            resources.ApplyResources(this.ResetNodeBTN, "ResetNodeBTN");
            this.ResetNodeBTN.Name = "ResetNodeBTN";
            this.ResetNodeBTN.UseVisualStyleBackColor = true;
            this.ResetNodeBTN.Click += new System.EventHandler(this.ResetNodeBTN_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // InformationControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ResetNodeBTN);
            this.Controls.Add(this.factorySettingBTN);
            this.Controls.Add(this.versionL);
            this.Controls.Add(this.typeL);
            this.Enable_write = false;
            this.Name = "InformationControl";
            this.Controls.SetChildIndex(this.typeL, 0);
            this.Controls.SetChildIndex(this.versionL, 0);
            this.Controls.SetChildIndex(this.factorySettingBTN, 0);
            this.Controls.SetChildIndex(this.ResetNodeBTN, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label versionL;
        private System.Windows.Forms.Label typeL;
        private System.Windows.Forms.Button factorySettingBTN;
        private System.Windows.Forms.Button ResetNodeBTN;
        private System.Windows.Forms.Label label1;
    }
}
