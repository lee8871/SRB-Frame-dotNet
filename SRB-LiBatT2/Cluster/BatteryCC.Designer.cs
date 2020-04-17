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
            this.label1 = new System.Windows.Forms.Label();
            this.LEDCB = new System.Windows.Forms.CheckBox();
            this.ChargeEnableCB = new System.Windows.Forms.CheckBox();
            this.MuteCB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.InnResNUM = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.CurrentNUM = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.CapacityNUM = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.LowVotNUM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InnResNUM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentNUM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CapacityNUM)).BeginInit();
            this.SuspendLayout();
            // 
            // LowVotNUM
            // 
            this.LowVotNUM.DecimalPlaces = 2;
            this.LowVotNUM.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.LowVotNUM.Location = new System.Drawing.Point(171, 68);
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
            this.LowVotNUM.Name = "LowVotNUM";
            this.LowVotNUM.Size = new System.Drawing.Size(53, 21);
            this.LowVotNUM.TabIndex = 7;
            this.LowVotNUM.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.LowVotNUM.ValueChanged += new System.EventHandler(this.lowVotNUM_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "Low Voltage Alram (V)";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // LEDCB
            // 
            this.LEDCB.AutoSize = true;
            this.LEDCB.Location = new System.Drawing.Point(17, 51);
            this.LEDCB.Name = "LEDCB";
            this.LEDCB.Size = new System.Drawing.Size(144, 16);
            this.LEDCB.TabIndex = 9;
            this.LEDCB.Text = "Power On Enabled LED";
            this.LEDCB.UseVisualStyleBackColor = true;
            // 
            // ChargeEnableCB
            // 
            this.ChargeEnableCB.AutoSize = true;
            this.ChargeEnableCB.Location = new System.Drawing.Point(17, 8);
            this.ChargeEnableCB.Name = "ChargeEnableCB";
            this.ChargeEnableCB.Size = new System.Drawing.Size(156, 16);
            this.ChargeEnableCB.TabIndex = 10;
            this.ChargeEnableCB.Text = "Power On Enable Charge";
            this.ChargeEnableCB.UseVisualStyleBackColor = true;
            // 
            // MuteCB
            // 
            this.MuteCB.AutoSize = true;
            this.MuteCB.Location = new System.Drawing.Point(17, 29);
            this.MuteCB.Name = "MuteCB";
            this.MuteCB.Size = new System.Drawing.Size(102, 16);
            this.MuteCB.TabIndex = 11;
            this.MuteCB.Text = "Power On Mute";
            this.MuteCB.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "Internal resistance (mΩ)";
            // 
            // InnResNUM
            // 
            this.InnResNUM.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.InnResNUM.Location = new System.Drawing.Point(171, 133);
            this.InnResNUM.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.InnResNUM.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.InnResNUM.Name = "InnResNUM";
            this.InnResNUM.Size = new System.Drawing.Size(53, 21);
            this.InnResNUM.TabIndex = 12;
            this.InnResNUM.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "Max Charge Current (A)";
            // 
            // CurrentNUM
            // 
            this.CurrentNUM.DecimalPlaces = 2;
            this.CurrentNUM.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.CurrentNUM.Location = new System.Drawing.Point(171, 89);
            this.CurrentNUM.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.CurrentNUM.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.CurrentNUM.Name = "CurrentNUM";
            this.CurrentNUM.Size = new System.Drawing.Size(53, 21);
            this.CurrentNUM.TabIndex = 14;
            this.CurrentNUM.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "Capacity (mAh)";
            // 
            // CapacityNUM
            // 
            this.CapacityNUM.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.CapacityNUM.Location = new System.Drawing.Point(171, 110);
            this.CapacityNUM.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.CapacityNUM.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.CapacityNUM.Name = "CapacityNUM";
            this.CapacityNUM.Size = new System.Drawing.Size(53, 21);
            this.CapacityNUM.TabIndex = 16;
            this.CapacityNUM.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // BatteryCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CapacityNUM);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CurrentNUM);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.InnResNUM);
            this.Controls.Add(this.MuteCB);
            this.Controls.Add(this.ChargeEnableCB);
            this.Controls.Add(this.LEDCB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LowVotNUM);
            this.Name = "BatteryCC";
            this.Size = new System.Drawing.Size(300, 157);
            this.Load += new System.EventHandler(this.BatteryCC_Load);
            this.Controls.SetChildIndex(this.LowVotNUM, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.LEDCB, 0);
            this.Controls.SetChildIndex(this.ChargeEnableCB, 0);
            this.Controls.SetChildIndex(this.MuteCB, 0);
            this.Controls.SetChildIndex(this.InnResNUM, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.CurrentNUM, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.CapacityNUM, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            ((System.ComponentModel.ISupportInitialize)(this.LowVotNUM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InnResNUM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentNUM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CapacityNUM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown LowVotNUM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox LEDCB;
        private System.Windows.Forms.CheckBox ChargeEnableCB;
        private System.Windows.Forms.CheckBox MuteCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown InnResNUM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown CurrentNUM;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown CapacityNUM;
    }
}
