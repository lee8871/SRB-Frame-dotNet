namespace SRB.Frame
{
    partial class INodeControl
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
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.MappingSelectCB = new System.Windows.Forms.ComboBox();
            this.sendFreqNUM = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.RunStopBTN = new System.Windows.Forms.Button();
            this.sendTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.sendFreqNUM)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolTips
            // 
            this.ToolTips.IsBalloon = true;
            // 
            // MappingSelectCB
            // 
            this.MappingSelectCB.FormattingEnabled = true;
            this.MappingSelectCB.Items.AddRange(new object[] {
            "Mapping 0",
            "Mapping 1",
            "Mapping 2",
            "Mapping 3"});
            this.MappingSelectCB.Location = new System.Drawing.Point(171, 0);
            this.MappingSelectCB.Name = "MappingSelectCB";
            this.MappingSelectCB.Size = new System.Drawing.Size(80, 20);
            this.MappingSelectCB.TabIndex = 25;
            this.MappingSelectCB.Text = "Mapping 1";
            // 
            // sendFreqNUM
            // 
            this.sendFreqNUM.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.sendFreqNUM.Location = new System.Drawing.Point(103, 0);
            this.sendFreqNUM.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.sendFreqNUM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sendFreqNUM.Name = "sendFreqNUM";
            this.sendFreqNUM.Size = new System.Drawing.Size(48, 21);
            this.sendFreqNUM.TabIndex = 24;
            this.sendFreqNUM.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.sendFreqNUM.ValueChanged += new System.EventHandler(this.sendFreqNUM_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "AccessSpeed(Hz): ";
            // 
            // RunStopBTN
            // 
            this.RunStopBTN.BackgroundImage = global::SRB_Frame.Properties.Resources._1175842;
            this.RunStopBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RunStopBTN.Location = new System.Drawing.Point(257, -1);
            this.RunStopBTN.Name = "RunStopBTN";
            this.RunStopBTN.Size = new System.Drawing.Size(40, 39);
            this.RunStopBTN.TabIndex = 15;
            this.RunStopBTN.UseVisualStyleBackColor = true;
            this.RunStopBTN.Click += new System.EventHandler(this.RunStopBTN_Click);
            // 
            // sendTimer
            // 
            this.sendTimer.Interval = 50;
            this.sendTimer.Tick += new System.EventHandler(this.sendTimer_Tick);
            // 
            // INodeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.MappingSelectCB);
            this.Controls.Add(this.sendFreqNUM);
            this.Controls.Add(this.RunStopBTN);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 20);
            this.Name = "INodeControl";
            this.Size = new System.Drawing.Size(300, 41);
            ((System.ComponentModel.ISupportInitialize)(this.sendFreqNUM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        protected System.Windows.Forms.Button RunStopBTN;
        private System.Windows.Forms.ComboBox MappingSelectCB;
        private System.Windows.Forms.NumericUpDown sendFreqNUM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer sendTimer;
        public System.Windows.Forms.ToolTip ToolTips;
    }
}
