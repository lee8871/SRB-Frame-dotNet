namespace SRB.NodeType.PS2_Handle
{
    partial class ConfigCC
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.OnlineNUM = new System.Windows.Forms.NumericUpDown();
            this.loseNUM = new System.Windows.Forms.NumericUpDown();
            this.StrengthNUM = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OnlineNUM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loseNUM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StrengthNUM)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(90, 46);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(143, 12);
            label1.TabIndex = 0;
            label1.Text = "Online Rumble Time (ms)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(90, 68);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(131, 12);
            label2.TabIndex = 0;
            label2.Text = "Lose Rumble Time (ms)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(90, 90);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(149, 12);
            label3.TabIndex = 0;
            label3.Text = "Power On Rumble Strength";
            // 
            // OnlineNUM
            // 
            this.OnlineNUM.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.OnlineNUM.Location = new System.Drawing.Point(17, 44);
            this.OnlineNUM.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this.OnlineNUM.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.OnlineNUM.Name = "OnlineNUM";
            this.OnlineNUM.Size = new System.Drawing.Size(67, 21);
            this.OnlineNUM.TabIndex = 8;
            this.OnlineNUM.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // loseNUM
            // 
            this.loseNUM.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.loseNUM.Location = new System.Drawing.Point(17, 66);
            this.loseNUM.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this.loseNUM.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.loseNUM.Name = "loseNUM";
            this.loseNUM.Size = new System.Drawing.Size(67, 21);
            this.loseNUM.TabIndex = 8;
            this.loseNUM.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // StrengthNUM
            // 
            this.StrengthNUM.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.StrengthNUM.Location = new System.Drawing.Point(17, 88);
            this.StrengthNUM.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.StrengthNUM.Name = "StrengthNUM";
            this.StrengthNUM.Size = new System.Drawing.Size(67, 21);
            this.StrengthNUM.TabIndex = 8;
            this.StrengthNUM.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // ConfigCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.StrengthNUM);
            this.Controls.Add(this.loseNUM);
            this.Controls.Add(this.OnlineNUM);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Name = "ConfigCC";
            this.Size = new System.Drawing.Size(300, 112);
            this.Controls.SetChildIndex(label1, 0);
            this.Controls.SetChildIndex(label2, 0);
            this.Controls.SetChildIndex(label3, 0);
            this.Controls.SetChildIndex(this.OnlineNUM, 0);
            this.Controls.SetChildIndex(this.loseNUM, 0);
            this.Controls.SetChildIndex(this.StrengthNUM, 0);
            ((System.ComponentModel.ISupportInitialize)(this.OnlineNUM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loseNUM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StrengthNUM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NumericUpDown OnlineNUM;
        private System.Windows.Forms.NumericUpDown loseNUM;
        private System.Windows.Forms.NumericUpDown StrengthNUM;
    }
}
