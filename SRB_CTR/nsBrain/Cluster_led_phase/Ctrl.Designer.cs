namespace SRB_CTR.nsBrain.Cluster_led_phase
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.fadeNUM = new System.Windows.Forms.NumericUpDown();
            this.CycleNUM = new System.Windows.Forms.NumericUpDown();
            this.cycleL = new System.Windows.Forms.Label();
            this.writeBTN = new System.Windows.Forms.Button();
            this.fadeL = new System.Windows.Forms.Label();
            this.readBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fadeNUM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CycleNUM)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fade(sec):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cycle(sec):";
            // 
            // fadeNUM
            // 
            this.fadeNUM.DecimalPlaces = 2;
            this.fadeNUM.Location = new System.Drawing.Point(115, 3);
            this.fadeNUM.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.fadeNUM.Name = "fadeNUM";
            this.fadeNUM.Size = new System.Drawing.Size(51, 21);
            this.fadeNUM.TabIndex = 2;
            // 
            // CycleNUM
            // 
            this.CycleNUM.DecimalPlaces = 2;
            this.CycleNUM.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.CycleNUM.Location = new System.Drawing.Point(115, 21);
            this.CycleNUM.Name = "CycleNUM";
            this.CycleNUM.Size = new System.Drawing.Size(51, 21);
            this.CycleNUM.TabIndex = 2;
            // 
            // cycleL
            // 
            this.cycleL.AutoSize = true;
            this.cycleL.Location = new System.Drawing.Point(75, 23);
            this.cycleL.Name = "cycleL";
            this.cycleL.Size = new System.Drawing.Size(29, 12);
            this.cycleL.TabIndex = 3;
            this.cycleL.Text = "2.00";
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
            // fadeL
            // 
            this.fadeL.AutoSize = true;
            this.fadeL.Location = new System.Drawing.Point(75, 5);
            this.fadeL.Name = "fadeL";
            this.fadeL.Size = new System.Drawing.Size(23, 12);
            this.fadeL.TabIndex = 3;
            this.fadeL.Text = "255";
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
            // Crtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.readBTN);
            this.Controls.Add(this.writeBTN);
            this.Controls.Add(this.cycleL);
            this.Controls.Add(this.fadeL);
            this.Controls.Add(this.CycleNUM);
            this.Controls.Add(this.fadeNUM);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 20);
            this.Name = "Crtl";
            this.Size = new System.Drawing.Size(300, 45);
            ((System.ComponentModel.ISupportInitialize)(this.fadeNUM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CycleNUM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown fadeNUM;
        private System.Windows.Forms.NumericUpDown CycleNUM;
        private System.Windows.Forms.Label cycleL;
        private System.Windows.Forms.Label fadeL;
        private System.Windows.Forms.Button writeBTN;
        private System.Windows.Forms.Button readBTN;
    }
}
