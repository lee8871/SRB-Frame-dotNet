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
            this.ToolTips = new System.Windows.Forms.ToolTip();
            this.components = new System.ComponentModel.Container();
            this.MappingSelectCB = new System.Windows.Forms.ComboBox();
            this.sendFreqNUM = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.sendTimer = new System.Windows.Forms.Timer(this.components);
            this.HelpBTN = new System.Windows.Forms.Button();
            this.RunStopBTN = new System.Windows.Forms.Button();
            this.retryLAB = new System.Windows.Forms.Label();
            this.Retry_display = new System.Windows.Forms.Timer(this.components);
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
            this.MappingSelectCB.Location = new System.Drawing.Point(147, -1);
            this.MappingSelectCB.Name = "MappingSelectCB";
            this.MappingSelectCB.Size = new System.Drawing.Size(78, 20);
            this.MappingSelectCB.TabIndex = 25;
            this.MappingSelectCB.Text = "Mapping 1";
            // 
            // sendFreqNUM
            // 
            this.sendFreqNUM.Location = new System.Drawing.Point(104, -1);
            this.sendFreqNUM.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.sendFreqNUM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sendFreqNUM.Name = "sendFreqNUM";
            this.sendFreqNUM.Size = new System.Drawing.Size(37, 21);
            this.sendFreqNUM.TabIndex = 24;
            this.sendFreqNUM.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.sendFreqNUM.ValueChanged += new System.EventHandler(this.sendFreqNUM_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "AccessSpeed(Hz): ";
            // 
            // sendTimer
            // 
            this.sendTimer.Interval = 50;
            this.sendTimer.Tick += new System.EventHandler(this.sendTimer_Tick);
            // 
            // HelpBTN
            // 
            this.HelpBTN.BackgroundImage = global::SRB.Frame.Properties.Resources._1175798;
            this.HelpBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.HelpBTN.Location = new System.Drawing.Point(231, -2);
            this.HelpBTN.Name = "HelpBTN";
            this.HelpBTN.Size = new System.Drawing.Size(24, 24);
            this.HelpBTN.TabIndex = 27;
            this.HelpBTN.UseVisualStyleBackColor = true;
            this.HelpBTN.Click += new System.EventHandler(this.HelpBTN_Click);
            // 
            // RunStopBTN
            // 
            this.RunStopBTN.BackgroundImage = global::SRB.Frame.Properties.Resources._1175842;
            this.RunStopBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RunStopBTN.Location = new System.Drawing.Point(257, -2);
            this.RunStopBTN.Name = "RunStopBTN";
            this.RunStopBTN.Size = new System.Drawing.Size(40, 39);
            this.RunStopBTN.TabIndex = 15;
            this.RunStopBTN.UseVisualStyleBackColor = true;
            this.RunStopBTN.Click += new System.EventHandler(this.RunStopBTN_Click);
            // 
            // retryLAB
            // 
            this.retryLAB.AutoSize = true;
            this.retryLAB.Location = new System.Drawing.Point(4, 23);
            this.retryLAB.Name = "retryLAB";
            this.retryLAB.Size = new System.Drawing.Size(179, 12);
            this.retryLAB.TabIndex = 28;
            this.retryLAB.Text = "Access:{0} Retry:{1} Lose:{2}";
            // 
            // Retry_display
            // 
            this.Retry_display.Enabled = true;
            this.Retry_display.Interval = 2000;
            this.Retry_display.Tick += new System.EventHandler(this.Retry_display_Tick);
            // 
            // INodeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.retryLAB);
            this.Controls.Add(this.HelpBTN);
            this.Controls.Add(this.MappingSelectCB);
            this.Controls.Add(this.sendFreqNUM);
            this.Controls.Add(this.RunStopBTN);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 20);
            this.Name = "INodeControl";
            this.Size = new System.Drawing.Size(300, 40);
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
        protected System.Windows.Forms.Button HelpBTN;
        private System.Windows.Forms.Label retryLAB;
        private System.Windows.Forms.Timer Retry_display;
    }
}
