namespace SRB.NodeType.SpeedMotorF
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
            this.handleBTN = new System.Windows.Forms.Button();
            this.BrakeBTN = new System.Windows.Forms.Button();
            this.StopBTN = new System.Windows.Forms.Button();
            this.SpeedLAB = new System.Windows.Forms.Label();
            this.OdometerLAB = new System.Windows.Forms.Label();
            this.DebugBTN = new System.Windows.Forms.Button();
            this.Handle_tick = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // handleBTN
            // 
            this.handleBTN.Location = new System.Drawing.Point(6, 39);
            this.handleBTN.Name = "handleBTN";
            this.handleBTN.Size = new System.Drawing.Size(124, 74);
            this.handleBTN.TabIndex = 9;
            this.handleBTN.Text = "电机操纵杆";
            this.ToolTips.SetToolTip(this.handleBTN, "左键左右拖动设置电机的速度值。\r\n右键点击切换灵敏度。");
            this.handleBTN.UseVisualStyleBackColor = true;
            this.handleBTN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.handleBTN_MouseDown);
            // 
            // BrakeBTN
            // 
            this.BrakeBTN.Location = new System.Drawing.Point(136, 39);
            this.BrakeBTN.Name = "BrakeBTN";
            this.BrakeBTN.Size = new System.Drawing.Size(60, 42);
            this.BrakeBTN.TabIndex = 27;
            this.BrakeBTN.Text = "Brake";
            this.BrakeBTN.UseVisualStyleBackColor = true;
            // 
            // StopBTN
            // 
            this.StopBTN.Location = new System.Drawing.Point(202, 37);
            this.StopBTN.Name = "StopBTN";
            this.StopBTN.Size = new System.Drawing.Size(60, 44);
            this.StopBTN.TabIndex = 28;
            this.StopBTN.Text = "Stop";
            this.StopBTN.UseVisualStyleBackColor = true;
            this.StopBTN.Click += new System.EventHandler(this.StopBTN_Click);
            // 
            // SpeedLAB
            // 
            this.SpeedLAB.AutoSize = true;
            this.SpeedLAB.Location = new System.Drawing.Point(137, 84);
            this.SpeedLAB.Name = "SpeedLAB";
            this.SpeedLAB.Size = new System.Drawing.Size(41, 12);
            this.SpeedLAB.TabIndex = 29;
            this.SpeedLAB.Text = "Speed:";
            // 
            // OdometerLAB
            // 
            this.OdometerLAB.AutoSize = true;
            this.OdometerLAB.Location = new System.Drawing.Point(137, 96);
            this.OdometerLAB.Name = "OdometerLAB";
            this.OdometerLAB.Size = new System.Drawing.Size(59, 12);
            this.OdometerLAB.TabIndex = 30;
            this.OdometerLAB.Text = "Odometer:";
            // 
            // DebugBTN
            // 
            this.DebugBTN.BackgroundImage = global::SRB.NodeType.SpeedMotorF.Properties.Resources.info;
            this.DebugBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DebugBTN.Location = new System.Drawing.Point(268, 44);
            this.DebugBTN.Name = "DebugBTN";
            this.DebugBTN.Size = new System.Drawing.Size(24, 24);
            this.DebugBTN.TabIndex = 31;
            this.DebugBTN.UseVisualStyleBackColor = true;
            this.DebugBTN.Click += new System.EventHandler(this.DebugBTN_Click);
            // 
            // Handle_tick
            // 
            this.Handle_tick.Interval = 25;
            this.Handle_tick.Tick += new System.EventHandler(this.Handle_tick_Tick);
            // 
            // Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DebugBTN);
            this.Controls.Add(this.OdometerLAB);
            this.Controls.Add(this.SpeedLAB);
            this.Controls.Add(this.StopBTN);
            this.Controls.Add(this.BrakeBTN);
            this.Controls.Add(this.handleBTN);
            this.MinimumSize = new System.Drawing.Size(300, 0);
            this.Name = "Ctrl";
            this.Size = new System.Drawing.Size(300, 116);
            this.Controls.SetChildIndex(this.handleBTN, 0);
            this.Controls.SetChildIndex(this.BrakeBTN, 0);
            this.Controls.SetChildIndex(this.StopBTN, 0);
            this.Controls.SetChildIndex(this.HelpBTN, 0);
            this.Controls.SetChildIndex(this.RunStopBTN, 0);
            this.Controls.SetChildIndex(this.SpeedLAB, 0);
            this.Controls.SetChildIndex(this.OdometerLAB, 0);
            this.Controls.SetChildIndex(this.DebugBTN, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button handleBTN;
        private System.Windows.Forms.Button BrakeBTN;
        private System.Windows.Forms.Button StopBTN;
        private System.Windows.Forms.Label SpeedLAB;
        private System.Windows.Forms.Label OdometerLAB;
        private System.Windows.Forms.Button DebugBTN;
        private System.Windows.Forms.Timer Handle_tick;
    }
}
