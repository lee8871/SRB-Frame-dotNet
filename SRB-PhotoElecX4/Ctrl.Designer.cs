namespace SRB.NodeType.PhotoElecX4
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
            this.ADCtable = new System.Windows.Forms.DataGridView();
            this.DebugBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ADCtable)).BeginInit();
            this.SuspendLayout();
            // 
            // ADCtable
            // 
            this.ADCtable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ADCtable.Location = new System.Drawing.Point(6, 74);
            this.ADCtable.Name = "ADCtable";
            this.ADCtable.RowTemplate.Height = 23;
            this.ADCtable.Size = new System.Drawing.Size(291, 226);
            this.ADCtable.TabIndex = 28;
            // 
            // DebugBTN
            // 
            this.DebugBTN.BackgroundImage = global::SRB.NodeType.PhotoElecX4.Properties.Resources.info;
            this.DebugBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DebugBTN.Location = new System.Drawing.Point(257, 44);
            this.DebugBTN.Name = "DebugBTN";
            this.DebugBTN.Size = new System.Drawing.Size(24, 24);
            this.DebugBTN.TabIndex = 32;
            this.DebugBTN.UseVisualStyleBackColor = true;
            this.DebugBTN.Click += new System.EventHandler(this.DebugBTN_Click);
            // 
            // Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DebugBTN);
            this.Controls.Add(this.ADCtable);
            this.MinimumSize = new System.Drawing.Size(300, 0);
            this.Name = "Ctrl";
            this.Size = new System.Drawing.Size(300, 300);
            this.Controls.SetChildIndex(this.HelpBTN, 0);
            this.Controls.SetChildIndex(this.RunStopBTN, 0);
            this.Controls.SetChildIndex(this.ADCtable, 0);
            this.Controls.SetChildIndex(this.DebugBTN, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ADCtable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ADCtable;
        private System.Windows.Forms.Button DebugBTN;
    }
}
