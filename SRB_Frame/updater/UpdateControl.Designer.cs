namespace SRB.Frame
{
    partial class UpdateControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateControl));
            this.MainOF = new System.Windows.Forms.OpenFileDialog();
            this.ResetBTN = new System.Windows.Forms.Button();
            this.openBTN = new System.Windows.Forms.Button();
            this.BurnBTN = new System.Windows.Forms.Button();
            this.UpdateFileTB = new System.Windows.Forms.TextBox();
            this.UpdateInformationgRTB = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // MainOF
            // 
            resources.ApplyResources(this.MainOF, "MainOF");
            this.MainOF.FileOk += new System.ComponentModel.CancelEventHandler(this.MainOF_FileOk);
            // 
            // ResetBTN
            // 
            this.ResetBTN.BackgroundImage = global::SRB_Frame.Properties.Resources.HighLight;
            resources.ApplyResources(this.ResetBTN, "ResetBTN");
            this.ResetBTN.Name = "ResetBTN";
            this.ResetBTN.UseVisualStyleBackColor = true;
            this.ResetBTN.Click += new System.EventHandler(this.ResetBTN_Click);
            // 
            // openBTN
            // 
            this.openBTN.BackgroundImage = global::SRB_Frame.Properties.Resources.Open;
            resources.ApplyResources(this.openBTN, "openBTN");
            this.openBTN.Name = "openBTN";
            this.openBTN.UseVisualStyleBackColor = true;
            this.openBTN.Click += new System.EventHandler(this.openBTN_Click);
            // 
            // BurnBTN
            // 
            this.BurnBTN.BackgroundImage = global::SRB_Frame.Properties.Resources.burn;
            resources.ApplyResources(this.BurnBTN, "BurnBTN");
            this.BurnBTN.Name = "BurnBTN";
            this.BurnBTN.UseVisualStyleBackColor = true;
            this.BurnBTN.Click += new System.EventHandler(this.BurnBTN_Click);
            // 
            // UpdateFileTB
            // 
            resources.ApplyResources(this.UpdateFileTB, "UpdateFileTB");
            this.UpdateFileTB.Name = "UpdateFileTB";
            this.UpdateFileTB.ReadOnly = true;
            // 
            // UpdateInformationgRTB
            // 
            resources.ApplyResources(this.UpdateInformationgRTB, "UpdateInformationgRTB");
            this.UpdateInformationgRTB.Name = "UpdateInformationgRTB";
            this.UpdateInformationgRTB.ReadOnly = true;
            // 
            // UpdateControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.UpdateInformationgRTB);
            this.Controls.Add(this.UpdateFileTB);
            this.Controls.Add(this.ResetBTN);
            this.Controls.Add(this.openBTN);
            this.Controls.Add(this.BurnBTN);
            this.Name = "UpdateControl";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button openBTN;
        private System.Windows.Forms.Button BurnBTN;
        private System.Windows.Forms.OpenFileDialog MainOF;
        private System.Windows.Forms.Button ResetBTN;
        private System.Windows.Forms.TextBox UpdateFileTB;
        private System.Windows.Forms.RichTextBox UpdateInformationgRTB;
    }
}
