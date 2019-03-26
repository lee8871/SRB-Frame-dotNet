namespace SRB.NodeType.Charger
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
            this.components = new System.ComponentModel.Container();
            this.sendTimer = new System.Windows.Forms.Timer(this.components);
            this.ChangeVottageBar = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.BatteryValueLAB = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ChargeTimerLAB = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MuteBTN = new System.Windows.Forms.Button();
            this.MorseTB = new System.Windows.Forms.TextBox();
            this.PlayBTN = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // sendTimer
            // 
            this.sendTimer.Interval = 50;
            // 
            // ChangeVottageBar
            // 
            this.ChangeVottageBar.Location = new System.Drawing.Point(115, 10);
            this.ChangeVottageBar.Maximum = 8400;
            this.ChangeVottageBar.Minimum = 6000;
            this.ChangeVottageBar.Name = "ChangeVottageBar";
            this.ChangeVottageBar.Size = new System.Drawing.Size(177, 10);
            this.ChangeVottageBar.TabIndex = 0;
            this.ChangeVottageBar.Value = 7800;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Battery Vottage:";
            // 
            // BatteryValueLAB
            // 
            this.BatteryValueLAB.AutoSize = true;
            this.BatteryValueLAB.Location = new System.Drawing.Point(53, 28);
            this.BatteryValueLAB.Name = "BatteryValueLAB";
            this.BatteryValueLAB.Size = new System.Drawing.Size(41, 12);
            this.BatteryValueLAB.TabIndex = 2;
            this.BatteryValueLAB.Text = "7.403V";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Charger Status";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Charge Timer:";
            // 
            // ChargeTimerLAB
            // 
            this.ChargeTimerLAB.AutoSize = true;
            this.ChargeTimerLAB.Location = new System.Drawing.Point(54, 52);
            this.ChargeTimerLAB.Name = "ChargeTimerLAB";
            this.ChargeTimerLAB.Size = new System.Drawing.Size(29, 12);
            this.ChargeTimerLAB.TabIndex = 5;
            this.ChargeTimerLAB.Text = "144s";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(202, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "Charging";
            // 
            // MuteBTN
            // 
            this.MuteBTN.Location = new System.Drawing.Point(252, 98);
            this.MuteBTN.Name = "MuteBTN";
            this.MuteBTN.Size = new System.Drawing.Size(40, 40);
            this.MuteBTN.TabIndex = 6;
            this.MuteBTN.Text = "Mute";
            this.MuteBTN.UseVisualStyleBackColor = true;
            // 
            // MorseTB
            // 
            this.MorseTB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MorseTB.Location = new System.Drawing.Point(66, 105);
            this.MorseTB.MaxLength = 7;
            this.MorseTB.Name = "MorseTB";
            this.MorseTB.Size = new System.Drawing.Size(87, 26);
            this.MorseTB.TabIndex = 4;
            this.MorseTB.Text = ".-..-.-";
            // 
            // PlayBTN
            // 
            this.PlayBTN.Location = new System.Drawing.Point(160, 98);
            this.PlayBTN.Name = "PlayBTN";
            this.PlayBTN.Size = new System.Drawing.Size(40, 40);
            this.PlayBTN.TabIndex = 8;
            this.PlayBTN.Text = "Play";
            this.PlayBTN.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "Morse Input:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(206, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(40, 40);
            this.button1.TabIndex = 8;
            this.button1.Text = "Light";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(37, 105);
            this.textBox1.MaxLength = 1;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(23, 26);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "F";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(179, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "7.2|";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(223, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "7.8|";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(268, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "8.4|";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(110, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 14;
            this.label10.Text = "|6.0";
            // 
            // Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PlayBTN);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.MorseTB);
            this.Controls.Add(this.MuteBTN);
            this.Controls.Add(this.ChargeTimerLAB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BatteryValueLAB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChangeVottageBar);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 20);
            this.Name = "Ctrl";
            this.Size = new System.Drawing.Size(300, 141);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer sendTimer;
        private System.Windows.Forms.ProgressBar ChangeVottageBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label BatteryValueLAB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ChargeTimerLAB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button MuteBTN;
        private System.Windows.Forms.TextBox MorseTB;
        private System.Windows.Forms.Button PlayBTN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
