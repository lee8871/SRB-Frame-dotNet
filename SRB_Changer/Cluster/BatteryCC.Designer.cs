namespace SRB.NodeType.Charger
{
    partial class BatteryCC
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
            this.LowVotNUM = new System.Windows.Forms.NumericUpDown();
            this.HighVotNUM = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.LEDCB = new System.Windows.Forms.CheckBox();
            this.ChargeEnableCB = new System.Windows.Forms.CheckBox();
            this.MuteCB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.LowVotNUM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HighVotNUM)).BeginInit();
            this.SuspendLayout();
            // 
            // lowVotNUM
            // 
            this.LowVotNUM.DecimalPlaces = 2;
            this.LowVotNUM.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.LowVotNUM.Location = new System.Drawing.Point(17, 22);
            this.LowVotNUM.Maximum = new decimal(new int[] {
            84,
            0,
            0,
            65536});
            this.LowVotNUM.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.LowVotNUM.Name = "lowVotNUM";
            this.LowVotNUM.Size = new System.Drawing.Size(53, 21);
            this.LowVotNUM.TabIndex = 7;
            this.LowVotNUM.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.LowVotNUM.ValueChanged += new System.EventHandler(this.lowVotNUM_ValueChanged);
            // 
            // HighVotNUM
            // 
            this.HighVotNUM.DecimalPlaces = 2;
            this.HighVotNUM.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.HighVotNUM.Location = new System.Drawing.Point(126, 22);
            this.HighVotNUM.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.HighVotNUM.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.HighVotNUM.Name = "HighVotNUM";
            this.HighVotNUM.Size = new System.Drawing.Size(47, 21);
            this.HighVotNUM.TabIndex = 7;
            this.HighVotNUM.Value = new decimal(new int[] {
            84,
            0,
            0,
            65536});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Battery Voltage Range:";
            // 
            // LEDCB
            // 
            this.LEDCB.AutoSize = true;
            this.LEDCB.Location = new System.Drawing.Point(17, 92);
            this.LEDCB.Name = "LEDCB";
            this.LEDCB.Size = new System.Drawing.Size(144, 16);
            this.LEDCB.TabIndex = 9;
            this.LEDCB.Text = "Power On Enabled LED";
            this.LEDCB.UseVisualStyleBackColor = true;
            // 
            // ChargeEnableCB
            // 
            this.ChargeEnableCB.AutoSize = true;
            this.ChargeEnableCB.Location = new System.Drawing.Point(17, 49);
            this.ChargeEnableCB.Name = "ChargeEnableCB";
            this.ChargeEnableCB.Size = new System.Drawing.Size(156, 16);
            this.ChargeEnableCB.TabIndex = 10;
            this.ChargeEnableCB.Text = "Power On Enable Charge";
            this.ChargeEnableCB.UseVisualStyleBackColor = true;
            // 
            // MuteCB
            // 
            this.MuteCB.AutoSize = true;
            this.MuteCB.Location = new System.Drawing.Point(17, 70);
            this.MuteCB.Name = "MuteCB";
            this.MuteCB.Size = new System.Drawing.Size(102, 16);
            this.MuteCB.TabIndex = 11;
            this.MuteCB.Text = "Power On Mute";
            this.MuteCB.UseVisualStyleBackColor = true;
            // 
            // BatteryCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.MuteCB);
            this.Controls.Add(this.ChargeEnableCB);
            this.Controls.Add(this.LEDCB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HighVotNUM);
            this.Controls.Add(this.LowVotNUM);
            this.Name = "BatteryCC";
            this.Size = new System.Drawing.Size(300, 111);
            this.Load += new System.EventHandler(this.BatteryCC_Load);
            this.Controls.SetChildIndex(this.LowVotNUM, 0);
            this.Controls.SetChildIndex(this.HighVotNUM, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.LEDCB, 0);
            this.Controls.SetChildIndex(this.ChargeEnableCB, 0);
            this.Controls.SetChildIndex(this.MuteCB, 0);
            ((System.ComponentModel.ISupportInitialize)(this.LowVotNUM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HighVotNUM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown LowVotNUM;
        private System.Windows.Forms.NumericUpDown HighVotNUM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox LEDCB;
        private System.Windows.Forms.CheckBox ChargeEnableCB;
        private System.Windows.Forms.CheckBox MuteCB;
    }
}
