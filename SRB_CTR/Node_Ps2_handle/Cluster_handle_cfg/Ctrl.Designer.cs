﻿namespace SRB_CTR.nsBrain.Node_PS2_handle.Cluster_handle_cfg
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
            this.writeBTN = new System.Windows.Forms.Button();
            this.readBTN = new System.Windows.Forms.Button();
            this.AnalogCBOX = new System.Windows.Forms.CheckBox();
            this.RumbleCBOX = new System.Windows.Forms.CheckBox();
            this.PeriodNum = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PeriodNum)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(137, 12);
            label1.TabIndex = 0;
            label1.Text = "Set Handle Read Period";
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
            // AnalogCBOX
            // 
            this.AnalogCBOX.AutoSize = true;
            this.AnalogCBOX.Checked = true;
            this.AnalogCBOX.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.AnalogCBOX.Location = new System.Drawing.Point(15, 42);
            this.AnalogCBOX.Name = "AnalogCBOX";
            this.AnalogCBOX.Size = new System.Drawing.Size(126, 16);
            this.AnalogCBOX.TabIndex = 6;
            this.AnalogCBOX.Text = "Read Analog Value";
            this.AnalogCBOX.UseVisualStyleBackColor = true;
            // 
            // RumbleCBOX
            // 
            this.RumbleCBOX.AutoSize = true;
            this.RumbleCBOX.Checked = true;
            this.RumbleCBOX.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.RumbleCBOX.Location = new System.Drawing.Point(15, 64);
            this.RumbleCBOX.Name = "RumbleCBOX";
            this.RumbleCBOX.Size = new System.Drawing.Size(90, 16);
            this.RumbleCBOX.TabIndex = 7;
            this.RumbleCBOX.Text = "Open Rumble";
            this.RumbleCBOX.UseVisualStyleBackColor = true;
            // 
            // PeriodNum
            // 
            this.PeriodNum.Location = new System.Drawing.Point(15, 16);
            this.PeriodNum.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.PeriodNum.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.PeriodNum.Name = "PeriodNum";
            this.PeriodNum.Size = new System.Drawing.Size(67, 21);
            this.PeriodNum.TabIndex = 8;
            this.PeriodNum.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.PeriodNum);
            this.Controls.Add(this.RumbleCBOX);
            this.Controls.Add(this.AnalogCBOX);
            this.Controls.Add(this.readBTN);
            this.Controls.Add(this.writeBTN);
            this.Controls.Add(label1);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 20);
            this.Name = "Ctrl";
            this.Size = new System.Drawing.Size(300, 83);
            ((System.ComponentModel.ISupportInitialize)(this.PeriodNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button writeBTN;
        private System.Windows.Forms.Button readBTN;
        private System.Windows.Forms.CheckBox AnalogCBOX;
        private System.Windows.Forms.CheckBox RumbleCBOX;
        private System.Windows.Forms.NumericUpDown PeriodNum;
    }
}