namespace SRB.NodeType.Du_motor.Cluster
{
    partial class AdjustCC
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
            System.Windows.Forms.Label label1;
            this.AdjCB = new System.Windows.Forms.ComboBox();
            this.motorATogCBOX = new System.Windows.Forms.CheckBox();
            this.motorBTogCBOX = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(119, 12);
            label1.TabIndex = 0;
            label1.Text = "Set Max speed Value";
            // 
            // AdjCB
            // 
            this.AdjCB.FormattingEnabled = true;
            this.AdjCB.Items.AddRange(new object[] {
            "NoAdjust",
            "255",
            "1000",
            "10000"});
            this.AdjCB.Location = new System.Drawing.Point(15, 15);
            this.AdjCB.Name = "AdjCB";
            this.AdjCB.Size = new System.Drawing.Size(83, 20);
            this.AdjCB.TabIndex = 5;
            this.AdjCB.Text = "<unknow>";
            // 
            // motorATogCBOX
            // 
            this.motorATogCBOX.AutoSize = true;
            this.motorATogCBOX.Checked = true;
            this.motorATogCBOX.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.motorATogCBOX.Location = new System.Drawing.Point(15, 42);
            this.motorATogCBOX.Name = "motorATogCBOX";
            this.motorATogCBOX.Size = new System.Drawing.Size(90, 16);
            this.motorATogCBOX.TabIndex = 6;
            this.motorATogCBOX.Text = "Tog Motor A";
            this.motorATogCBOX.UseVisualStyleBackColor = true;
            // 
            // motorBTogCBOX
            // 
            this.motorBTogCBOX.AutoSize = true;
            this.motorBTogCBOX.Checked = true;
            this.motorBTogCBOX.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.motorBTogCBOX.Location = new System.Drawing.Point(111, 42);
            this.motorBTogCBOX.Name = "motorBTogCBOX";
            this.motorBTogCBOX.Size = new System.Drawing.Size(90, 16);
            this.motorBTogCBOX.TabIndex = 7;
            this.motorBTogCBOX.Text = "Tog Motor B";
            this.motorBTogCBOX.UseVisualStyleBackColor = true;
            // 
            // AdjustCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.motorBTogCBOX);
            this.Controls.Add(this.motorATogCBOX);
            this.Controls.Add(this.AdjCB);
            this.Controls.Add(label1);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 20);
            this.Name = "AdjustCC";
            this.Size = new System.Drawing.Size(300, 61);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox AdjCB;
        private System.Windows.Forms.CheckBox motorATogCBOX;
        private System.Windows.Forms.CheckBox motorBTogCBOX;
    }
}
