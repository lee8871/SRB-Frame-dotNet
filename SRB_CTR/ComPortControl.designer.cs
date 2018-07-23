namespace lemonReceiver.ToNode
{
    partial class ComPortControl
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
            this.CMDSpeed = new System.Windows.Forms.Label();
            this.sendPB = new System.Windows.Forms.ProgressBar();
            this.recvPB = new System.Windows.Forms.ProgressBar();
            this.comSelectCB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // CMDSpeed
            // 
            this.CMDSpeed.AutoSize = true;
            this.CMDSpeed.Location = new System.Drawing.Point(4, 5);
            this.CMDSpeed.Name = "CMDSpeed";
            this.CMDSpeed.Size = new System.Drawing.Size(89, 12);
            this.CMDSpeed.TabIndex = 2;
            this.CMDSpeed.Text = "收发速度:%d/%d";
            // 
            // sendPB
            // 
            this.sendPB.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.sendPB.Location = new System.Drawing.Point(180, 7);
            this.sendPB.Name = "sendPB";
            this.sendPB.Size = new System.Drawing.Size(100, 10);
            this.sendPB.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.sendPB.TabIndex = 3;
            this.sendPB.Value = 20;
            // 
            // recvPB
            // 
            this.recvPB.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.recvPB.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.recvPB.ForeColor = System.Drawing.Color.Red;
            this.recvPB.Location = new System.Drawing.Point(180, 0);
            this.recvPB.Name = "recvPB";
            this.recvPB.Size = new System.Drawing.Size(100, 10);
            this.recvPB.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.recvPB.TabIndex = 3;
            this.recvPB.Value = 20;
            // 
            // comSelectCB
            // 
            this.comSelectCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comSelectCB.FormattingEnabled = true;
            this.comSelectCB.Location = new System.Drawing.Point(286, 0);
            this.comSelectCB.Name = "comSelectCB";
            this.comSelectCB.Size = new System.Drawing.Size(71, 20);
            this.comSelectCB.TabIndex = 0;
            this.comSelectCB.TabStop = false;
            this.comSelectCB.SelectedIndexChanged += new System.EventHandler(this.mainCB_SelectedIndexChanged);
            // 
            // ComPortControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.recvPB);
            this.Controls.Add(this.sendPB);
            this.Controls.Add(this.CMDSpeed);
            this.Controls.Add(this.comSelectCB);
            this.MinimumSize = new System.Drawing.Size(40, 20);
            this.Name = "ComPortControl";
            this.Size = new System.Drawing.Size(360, 20);
            this.Load += new System.EventHandler(this.ComPortControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CMDSpeed;
        private System.Windows.Forms.ProgressBar sendPB;
        private System.Windows.Forms.ProgressBar recvPB;
        private System.Windows.Forms.ComboBox comSelectCB;
    }
}
