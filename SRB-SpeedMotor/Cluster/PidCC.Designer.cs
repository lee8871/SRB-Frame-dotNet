namespace SRB.NodeType.SpeedMotor
{ 
    partial class PidCC
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
            this.kpNUM = new System.Windows.Forms.NumericUpDown();
            this.kiNUM = new System.Windows.Forms.NumericUpDown();
            this.kdNUM = new System.Windows.Forms.NumericUpDown();
            this.k0NUM = new System.Windows.Forms.NumericUpDown();
            this.k1NUM = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.kpNUM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiNUM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdNUM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.k0NUM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.k1NUM)).BeginInit();
            this.SuspendLayout();
            // 
            // kpNUM
            // 
            this.kpNUM.DecimalPlaces = 5;
            this.kpNUM.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.kpNUM.Location = new System.Drawing.Point(32, 17);
            this.kpNUM.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.kpNUM.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            -2147483648});
            this.kpNUM.Name = "kpNUM";
            this.kpNUM.Size = new System.Drawing.Size(85, 21);
            this.kpNUM.TabIndex = 7;
            this.kpNUM.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // kiNUM
            // 
            this.kiNUM.DecimalPlaces = 5;
            this.kiNUM.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.kiNUM.Location = new System.Drawing.Point(32, 42);
            this.kiNUM.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.kiNUM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.kiNUM.Name = "kiNUM";
            this.kiNUM.Size = new System.Drawing.Size(85, 21);
            this.kiNUM.TabIndex = 8;
            // 
            // kdNUM
            // 
            this.kdNUM.DecimalPlaces = 5;
            this.kdNUM.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.kdNUM.Location = new System.Drawing.Point(32, 67);
            this.kdNUM.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.kdNUM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.kdNUM.Name = "kdNUM";
            this.kdNUM.Size = new System.Drawing.Size(85, 21);
            this.kdNUM.TabIndex = 9;
            // 
            // k0NUM
            // 
            this.k0NUM.Location = new System.Drawing.Point(152, 42);
            this.k0NUM.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.k0NUM.Name = "k0NUM";
            this.k0NUM.Size = new System.Drawing.Size(85, 21);
            this.k0NUM.TabIndex = 10;
            // 
            // k1NUM
            // 
            this.k1NUM.Location = new System.Drawing.Point(152, 67);
            this.k1NUM.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.k1NUM.Name = "k1NUM";
            this.k1NUM.Size = new System.Drawing.Size(85, 21);
            this.k1NUM.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "kp";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "ki";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "kd";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(129, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "k0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(129, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "k1";
            // 
            // PidCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.k1NUM);
            this.Controls.Add(this.k0NUM);
            this.Controls.Add(this.kdNUM);
            this.Controls.Add(this.kiNUM);
            this.Controls.Add(this.kpNUM);
            this.Name = "PidCC";
            this.Size = new System.Drawing.Size(300, 91);
            this.Controls.SetChildIndex(this.kpNUM, 0);
            this.Controls.SetChildIndex(this.kiNUM, 0);
            this.Controls.SetChildIndex(this.kdNUM, 0);
            this.Controls.SetChildIndex(this.k0NUM, 0);
            this.Controls.SetChildIndex(this.k1NUM, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            ((System.ComponentModel.ISupportInitialize)(this.kpNUM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kiNUM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kdNUM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.k0NUM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.k1NUM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown kpNUM;
        private System.Windows.Forms.NumericUpDown kiNUM;
        private System.Windows.Forms.NumericUpDown kdNUM;
        private System.Windows.Forms.NumericUpDown k0NUM;
        private System.Windows.Forms.NumericUpDown k1NUM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
