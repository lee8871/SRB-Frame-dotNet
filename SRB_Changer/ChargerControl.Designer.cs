﻿namespace SRB.NodeType.Charger
{
    partial class ChangerControl
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
            this.statusLAB = new System.Windows.Forms.Label();
            this.MorseTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.MorseCharTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.RunStopBTN = new System.Windows.Forms.Button();
            this.BatteryPowerLedBTN = new System.Windows.Forms.Button();
            this.PlayBTN = new System.Windows.Forms.Button();
            this.ChangeEnableBTN = new System.Windows.Forms.Button();
            this.MuteBTN = new System.Windows.Forms.Button();
            this.CapacityLAB = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sendTimer
            // 
            this.sendTimer.Interval = 50;
            this.sendTimer.Tick += new System.EventHandler(this.sendTimer_Tick);
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
            this.label1.Text = "Battery Voltage:";
            // 
            // BatteryValueLAB
            // 
            this.BatteryValueLAB.AutoSize = true;
            this.BatteryValueLAB.Location = new System.Drawing.Point(53, 28);
            this.BatteryValueLAB.Name = "BatteryValueLAB";
            this.BatteryValueLAB.Size = new System.Drawing.Size(41, 12);
            this.BatteryValueLAB.TabIndex = 2;
            this.BatteryValueLAB.Text = "0.000V";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 40);
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
            this.ChargeTimerLAB.Size = new System.Drawing.Size(17, 12);
            this.ChargeTimerLAB.TabIndex = 5;
            this.ChargeTimerLAB.Text = "0S";
            // 
            // statusLAB
            // 
            this.statusLAB.AutoSize = true;
            this.statusLAB.Location = new System.Drawing.Point(113, 52);
            this.statusLAB.Name = "statusLAB";
            this.statusLAB.Size = new System.Drawing.Size(53, 12);
            this.statusLAB.TabIndex = 3;
            this.statusLAB.Text = "Charging";
            // 
            // MorseTB
            // 
            this.MorseTB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MorseTB.Location = new System.Drawing.Point(66, 105);
            this.MorseTB.MaxLength = 7;
            this.MorseTB.Name = "MorseTB";
            this.MorseTB.Size = new System.Drawing.Size(87, 26);
            this.MorseTB.TabIndex = 4;
            this.MorseTB.Text = "..-.";
            this.MorseTB.TextChanged += new System.EventHandler(this.MorseTB_TextChanged);
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
            // MorseCharTB
            // 
            this.MorseCharTB.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MorseCharTB.Location = new System.Drawing.Point(37, 105);
            this.MorseCharTB.MaxLength = 1;
            this.MorseCharTB.Name = "MorseCharTB";
            this.MorseCharTB.Size = new System.Drawing.Size(23, 26);
            this.MorseCharTB.TabIndex = 4;
            this.MorseCharTB.Text = "F";
            this.MorseCharTB.TextChanged += new System.EventHandler(this.MorseCharTB_TextChanged);
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
            // ToolTips
            // 
            this.ToolTips.IsBalloon = true;
            // 
            // RunStopBTN
            // 
            this.RunStopBTN.BackgroundImage = global::SRB_Changer.Properties.Resources._1175842;
            this.RunStopBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RunStopBTN.Location = new System.Drawing.Point(252, 52);
            this.RunStopBTN.Name = "RunStopBTN";
            this.RunStopBTN.Size = new System.Drawing.Size(40, 40);
            this.RunStopBTN.TabIndex = 15;
            this.RunStopBTN.UseVisualStyleBackColor = true;
            this.RunStopBTN.Click += new System.EventHandler(this.RunStopBTN_Click);
            // 
            // BatteryPowerLedBTN
            // 
            this.BatteryPowerLedBTN.BackgroundImage = global::SRB_Changer.Properties.Resources._1175695;
            this.BatteryPowerLedBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BatteryPowerLedBTN.Location = new System.Drawing.Point(193, 106);
            this.BatteryPowerLedBTN.Name = "BatteryPowerLedBTN";
            this.BatteryPowerLedBTN.Size = new System.Drawing.Size(28, 28);
            this.BatteryPowerLedBTN.TabIndex = 8;
            this.BatteryPowerLedBTN.UseVisualStyleBackColor = true;
            this.BatteryPowerLedBTN.Click += new System.EventHandler(this.BatteryPowerLedBTN_Click);
            // 
            // PlayBTN
            // 
            this.PlayBTN.BackgroundImage = global::SRB_Changer.Properties.Resources._1175826;
            this.PlayBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlayBTN.Location = new System.Drawing.Point(159, 106);
            this.PlayBTN.Name = "PlayBTN";
            this.PlayBTN.Size = new System.Drawing.Size(28, 28);
            this.PlayBTN.TabIndex = 8;
            this.PlayBTN.UseVisualStyleBackColor = true;
            this.PlayBTN.Click += new System.EventHandler(this.PlayBTN_Click);
            // 
            // ChangeEnableBTN
            // 
            this.ChangeEnableBTN.BackgroundImage = global::SRB_Changer.Properties.Resources._1175709;
            this.ChangeEnableBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ChangeEnableBTN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ChangeEnableBTN.Location = new System.Drawing.Point(261, 106);
            this.ChangeEnableBTN.Name = "ChangeEnableBTN";
            this.ChangeEnableBTN.Size = new System.Drawing.Size(28, 28);
            this.ChangeEnableBTN.TabIndex = 6;
            this.ChangeEnableBTN.UseVisualStyleBackColor = true;
            this.ChangeEnableBTN.Click += new System.EventHandler(this.ChangeEnableBTN_Click);
            // 
            // MuteBTN
            // 
            this.MuteBTN.BackgroundImage = global::SRB_Changer.Properties.Resources._1175710;
            this.MuteBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MuteBTN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MuteBTN.Location = new System.Drawing.Point(227, 106);
            this.MuteBTN.Name = "MuteBTN";
            this.MuteBTN.Size = new System.Drawing.Size(28, 28);
            this.MuteBTN.TabIndex = 6;
            this.MuteBTN.UseVisualStyleBackColor = true;
            this.MuteBTN.Click += new System.EventHandler(this.MuteBTN_Click);
            // 
            // Capacity
            // 
            this.CapacityLAB.AutoSize = true;
            this.CapacityLAB.Location = new System.Drawing.Point(54, 76);
            this.CapacityLAB.Name = "Capacity";
            this.CapacityLAB.Size = new System.Drawing.Size(17, 12);
            this.CapacityLAB.TabIndex = 17;
            this.CapacityLAB.Text = "0%";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "Capacity";
            // 
            // ChangerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.CapacityLAB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.RunStopBTN);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BatteryPowerLedBTN);
            this.Controls.Add(this.PlayBTN);
            this.Controls.Add(this.MorseCharTB);
            this.Controls.Add(this.MorseTB);
            this.Controls.Add(this.ChangeEnableBTN);
            this.Controls.Add(this.MuteBTN);
            this.Controls.Add(this.ChargeTimerLAB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusLAB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BatteryValueLAB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChangeVottageBar);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 20);
            this.Name = "ChangerControl";
            this.Size = new System.Drawing.Size(300, 137);
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
        private System.Windows.Forms.Label statusLAB;
        private System.Windows.Forms.TextBox MorseTB;
        private System.Windows.Forms.Button PlayBTN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BatteryPowerLedBTN;
        private System.Windows.Forms.TextBox MorseCharTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolTip ToolTips;
        private System.Windows.Forms.Button MuteBTN;
        private System.Windows.Forms.Button RunStopBTN;
        private System.Windows.Forms.Button ChangeEnableBTN;
        private System.Windows.Forms.Label CapacityLAB;
        private System.Windows.Forms.Label label7;
    }
}
