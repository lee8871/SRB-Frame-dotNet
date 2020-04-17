namespace SRB.Frame
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformationCC));
            this.versionL = new System.Windows.Forms.Label();
            this.typeL = new System.Windows.Forms.Label();
            this.factorySettingBTN = new System.Windows.Forms.Button();
            this.ResetNodeBTN = new System.Windows.Forms.Button();
            this.Update = new System.Windows.Forms.Button();
            this.TimeStampTT = new System.Windows.Forms.ToolTip(this.components);
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
            this.factorySettingBTN.BackColor = System.Drawing.SystemColors.Control;
            this.factorySettingBTN.BackgroundImage = global::SRB.Frame.Properties.Resources._1175784;
            resources.ApplyResources(this.factorySettingBTN, "factorySettingBTN");
            this.factorySettingBTN.ForeColor = System.Drawing.Color.Crimson;
            this.factorySettingBTN.Name = "factorySettingBTN";
            this.factorySettingBTN.UseVisualStyleBackColor = false;
            this.factorySettingBTN.Click += new System.EventHandler(this.factorySettingBTN_Click);
            // 
            // ResetNodeBTN
            // 
            this.ResetNodeBTN.BackgroundImage = global::SRB.Frame.Properties.Resources._1175854;
            resources.ApplyResources(this.ResetNodeBTN, "ResetNodeBTN");
            this.ResetNodeBTN.Name = "ResetNodeBTN";
            this.ResetNodeBTN.UseVisualStyleBackColor = true;
            this.ResetNodeBTN.Click += new System.EventHandler(this.ResetNodeBTN_Click);
            // 
            // Update
            // 
            this.Update.BackgroundImage = global::SRB.Frame.Properties.Resources.update;
            resources.ApplyResources(this.Update, "Update");
            this.Update.Name = "Update";
            this.Update.UseMnemonic = false;
            this.Update.UseVisualStyleBackColor = true;
            this.Update.Click += new System.EventHandler(this.Update_Click);

            // 
            // InformationCC
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Update);
            this.Controls.Add(this.ResetNodeBTN);
            this.Controls.Add(this.factorySettingBTN);
            this.Controls.Add(this.versionL);
            this.Controls.Add(this.typeL);
            this.Enable_write = false;
            this.Name = "InformationCC";
            this.Controls.SetChildIndex(this.typeL, 0);
            this.Controls.SetChildIndex(this.versionL, 0);
            this.Controls.SetChildIndex(this.factorySettingBTN, 0);
            this.Controls.SetChildIndex(this.ResetNodeBTN, 0);
            this.Controls.SetChildIndex(this.Update, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label versionL;
        private System.Windows.Forms.Label typeL;
        private System.Windows.Forms.Button factorySettingBTN;
        private System.Windows.Forms.Button ResetNodeBTN;
        private System.Windows.Forms.Button Update;
        private System.Windows.Forms.ToolTip TimeStampTT;
    }
}
