namespace SRB_CTR.nsBrain.Node_dMotor.Cluster_Du_Motor_v02
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
            System.Windows.Forms.Label label1;
            this.motor_a_minNUM = new System.Windows.Forms.NumericUpDown();
            this.writeBTN = new System.Windows.Forms.Button();
            this.readBTN = new System.Windows.Forms.Button();
            this.freqL = new System.Windows.Forms.Label();
            this.FreqCB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.motorMinL = new System.Windows.Forms.Label();
            this.motor_b_minNUM = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.behaviorCB = new System.Windows.Forms.ComboBox();
            this.behaviorL = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.motor_a_minNUM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.motor_b_minNUM)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(179, 12);
            label1.TabIndex = 0;
            label1.Text = "Set frequence of PWM on motor";
            // 
            // motor_a_minNUM
            // 
            this.motor_a_minNUM.DecimalPlaces = 2;
            this.motor_a_minNUM.Location = new System.Drawing.Point(15, 93);
            this.motor_a_minNUM.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.motor_a_minNUM.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.motor_a_minNUM.Name = "motor_a_minNUM";
            this.motor_a_minNUM.Size = new System.Drawing.Size(99, 21);
            this.motor_a_minNUM.TabIndex = 2;
            this.motor_a_minNUM.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.motor_a_minNUM.ValueChanged += new System.EventHandler(this.motor_X_minNUM_ValueChanged);
            // 
            // writeBTN
            // 
            this.writeBTN.BackgroundImage = global::SRB_CTR.Properties.Resources._1175763;
            this.writeBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.writeBTN.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.writeBTN.Location = new System.Drawing.Point(257, 0);
            this.writeBTN.Name = "writeBTN";
            this.writeBTN.Size = new System.Drawing.Size(40, 40);
            this.writeBTN.TabIndex = 4;
            this.writeBTN.UseVisualStyleBackColor = true;
            this.writeBTN.Click += new System.EventHandler(this.write);
            // 
            // readBTN
            // 
            this.readBTN.BackgroundImage = global::SRB_CTR.Properties.Resources._1175682;
            this.readBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.readBTN.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.readBTN.Location = new System.Drawing.Point(211, 0);
            this.readBTN.Name = "readBTN";
            this.readBTN.Size = new System.Drawing.Size(40, 40);
            this.readBTN.TabIndex = 4;
            this.readBTN.UseVisualStyleBackColor = true;
            this.readBTN.Click += new System.EventHandler(this.read);
            // 
            // freqL
            // 
            this.freqL.AutoSize = true;
            this.freqL.Location = new System.Drawing.Point(13, 12);
            this.freqL.Name = "freqL";
            this.freqL.Size = new System.Drawing.Size(119, 12);
            this.freqL.TabIndex = 0;
            this.freqL.Text = "Freq. is {0} <- {1}";
            // 
            // FreqCB
            // 
            this.FreqCB.FormattingEnabled = true;
            this.FreqCB.Items.AddRange(new object[] {
            "1kHz",
            "2kHz",
            "2.5kHz",
            "5kHz",
            "10kHz",
            "20kHz"});
            this.FreqCB.Location = new System.Drawing.Point(15, 43);
            this.FreqCB.Name = "FreqCB";
            this.FreqCB.Size = new System.Drawing.Size(83, 20);
            this.FreqCB.TabIndex = 5;
            this.FreqCB.Text = "10kHz";
            this.FreqCB.SelectedIndexChanged += new System.EventHandler(this.FreqCB_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Set motor\'s min pulse width (in us)";
            // 
            // motorMinL
            // 
            this.motorMinL.AutoSize = true;
            this.motorMinL.Location = new System.Drawing.Point(13, 78);
            this.motorMinL.Name = "motorMinL";
            this.motorMinL.Size = new System.Drawing.Size(233, 12);
            this.motorMinL.TabIndex = 0;
            this.motorMinL.Text = "Port A {0} <- {1}, PortB  {2} <- {3}, ";
            // 
            // motor_b_minNUM
            // 
            this.motor_b_minNUM.DecimalPlaces = 2;
            this.motor_b_minNUM.Location = new System.Drawing.Point(178, 93);
            this.motor_b_minNUM.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.motor_b_minNUM.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.motor_b_minNUM.Name = "motor_b_minNUM";
            this.motor_b_minNUM.Size = new System.Drawing.Size(99, 21);
            this.motor_b_minNUM.TabIndex = 2;
            this.motor_b_minNUM.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.motor_b_minNUM.ValueChanged += new System.EventHandler(this.motor_X_minNUM_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(257, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Set behavior when it can\'t receive commend";
            // 
            // behaviorCB
            // 
            this.behaviorCB.FormattingEnabled = true;
            this.behaviorCB.Items.AddRange(new object[] {
            "Close No Break",
            "Close And Break",
            "Keep Last Cmd"});
            this.behaviorCB.Location = new System.Drawing.Point(15, 149);
            this.behaviorCB.Name = "behaviorCB";
            this.behaviorCB.Size = new System.Drawing.Size(127, 20);
            this.behaviorCB.TabIndex = 5;
            this.behaviorCB.Text = "Close No Break";
            this.behaviorCB.SelectedIndexChanged += new System.EventHandler(this.behaveCB_SelectedIndexChanged);
            // 
            // behaviorL
            // 
            this.behaviorL.AutoEllipsis = true;
            this.behaviorL.AutoSize = true;
            this.behaviorL.Location = new System.Drawing.Point(13, 134);
            this.behaviorL.Name = "behaviorL";
            this.behaviorL.Size = new System.Drawing.Size(65, 12);
            this.behaviorL.TabIndex = 6;
            this.behaviorL.Text = "{0} -> {1}";
            // 
            // Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.behaviorL);
            this.Controls.Add(this.behaviorCB);
            this.Controls.Add(this.FreqCB);
            this.Controls.Add(this.readBTN);
            this.Controls.Add(this.writeBTN);
            this.Controls.Add(this.motor_b_minNUM);
            this.Controls.Add(this.motor_a_minNUM);
            this.Controls.Add(this.motorMinL);
            this.Controls.Add(this.freqL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(label1);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 0);
            this.Name = "Ctrl";
            this.Size = new System.Drawing.Size(300, 172);
            ((System.ComponentModel.ISupportInitialize)(this.motor_a_minNUM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.motor_b_minNUM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown motor_a_minNUM;
        private System.Windows.Forms.Button writeBTN;
        private System.Windows.Forms.Button readBTN;
        private System.Windows.Forms.Label freqL;
        private System.Windows.Forms.ComboBox FreqCB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label motorMinL;
        private System.Windows.Forms.NumericUpDown motor_b_minNUM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox behaviorCB;
        private System.Windows.Forms.Label behaviorL;
    }
}
