namespace SRB_CTR.SRB_Frame.Cluster_info
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
            this.versionL = new System.Windows.Forms.Label();
            this.typeL = new System.Windows.Forms.Label();
            this.factorySettingBTN = new System.Windows.Forms.Button();
            this.ResetNodeBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
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
            // versionL
            // 
            this.versionL.AutoSize = true;
            this.versionL.Location = new System.Drawing.Point(3, 19);
            this.versionL.Name = "versionL";
            this.versionL.Size = new System.Drawing.Size(53, 12);
            this.versionL.TabIndex = 3;
            this.versionL.Text = "Version:";
            // 
            // typeL
            // 
            this.typeL.AutoSize = true;
            this.typeL.Location = new System.Drawing.Point(3, 1);
            this.typeL.Name = "typeL";
            this.typeL.Size = new System.Drawing.Size(41, 12);
            this.typeL.TabIndex = 3;
            this.typeL.Text = "Type: ";
            // 
            // factorySettingBTN
            // 
            this.factorySettingBTN.BackColor = System.Drawing.Color.Crimson;
            this.factorySettingBTN.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.factorySettingBTN.ForeColor = System.Drawing.Color.Crimson;
            this.factorySettingBTN.Location = new System.Drawing.Point(193, 34);
            this.factorySettingBTN.Name = "factorySettingBTN";
            this.factorySettingBTN.Size = new System.Drawing.Size(23, 23);
            this.factorySettingBTN.TabIndex = 5;
            this.factorySettingBTN.UseVisualStyleBackColor = false;
            this.factorySettingBTN.Click += new System.EventHandler(this.factorySettingBTN_Click);
            // 
            // ResetNodeBTN
            // 
            this.ResetNodeBTN.Location = new System.Drawing.Point(5, 34);
            this.ResetNodeBTN.Name = "ResetNodeBTN";
            this.ResetNodeBTN.Size = new System.Drawing.Size(75, 23);
            this.ResetNodeBTN.TabIndex = 5;
            this.ResetNodeBTN.Text = "Reset";
            this.ResetNodeBTN.UseVisualStyleBackColor = true;
            this.ResetNodeBTN.Click += new System.EventHandler(this.ResetNodeBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Factory Setting:";
            // 
            // Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ResetNodeBTN);
            this.Controls.Add(this.factorySettingBTN);
            this.Controls.Add(this.readBTN);
            this.Controls.Add(this.versionL);
            this.Controls.Add(this.typeL);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 20);
            this.Name = "Ctrl";
            this.Size = new System.Drawing.Size(300, 60);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button readBTN;
        private System.Windows.Forms.Label versionL;
        private System.Windows.Forms.Label typeL;
        private System.Windows.Forms.Button factorySettingBTN;
        private System.Windows.Forms.Button ResetNodeBTN;
        private System.Windows.Forms.Label label1;
    }
}
