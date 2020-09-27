namespace SRB.NodeType.SpeedMotorF
{
    partial class debugFORM
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.SplitContainer splitContainer1;
            this.pauseBTN = new System.Windows.Forms.Button();
            this.reportRTC = new System.Windows.Forms.RichTextBox();
            this.TestSequenceBTN = new System.Windows.Forms.Button();
            this.ChartRefrashTimer = new System.Windows.Forms.Timer(this.components);
            this.chart1 = new SRB_Chart.Chart();
            this.saveBTN = new System.Windows.Forms.Button();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = System.Drawing.SystemColors.ControlLight;
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            splitContainer1.Panel1.Controls.Add(this.chart1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            splitContainer1.Panel2.Controls.Add(this.saveBTN);
            splitContainer1.Panel2.Controls.Add(this.pauseBTN);
            splitContainer1.Panel2.Controls.Add(this.reportRTC);
            splitContainer1.Panel2.Controls.Add(this.TestSequenceBTN);
            splitContainer1.Panel2MinSize = 100;
            splitContainer1.Size = new System.Drawing.Size(1453, 802);
            splitContainer1.SplitterDistance = 1153;
            splitContainer1.TabIndex = 0;
            // 
            // pauseBTN
            // 
            this.pauseBTN.Location = new System.Drawing.Point(3, 284);
            this.pauseBTN.Name = "pauseBTN";
            this.pauseBTN.Size = new System.Drawing.Size(78, 47);
            this.pauseBTN.TabIndex = 6;
            this.pauseBTN.Text = "暂停显示";
            this.pauseBTN.UseVisualStyleBackColor = true;
            this.pauseBTN.Click += new System.EventHandler(this.pauseBTN_Click);
            // 
            // reportRTC
            // 
            this.reportRTC.Dock = System.Windows.Forms.DockStyle.Top;
            this.reportRTC.Location = new System.Drawing.Point(0, 0);
            this.reportRTC.MinimumSize = new System.Drawing.Size(296, 225);
            this.reportRTC.Name = "reportRTC";
            this.reportRTC.Size = new System.Drawing.Size(296, 225);
            this.reportRTC.TabIndex = 5;
            this.reportRTC.Text = "";
            // 
            // TestSequenceBTN
            // 
            this.TestSequenceBTN.Location = new System.Drawing.Point(3, 231);
            this.TestSequenceBTN.Name = "TestSequenceBTN";
            this.TestSequenceBTN.Size = new System.Drawing.Size(78, 47);
            this.TestSequenceBTN.TabIndex = 4;
            this.TestSequenceBTN.Text = "执行序列";
            this.TestSequenceBTN.UseVisualStyleBackColor = true;
            this.TestSequenceBTN.Click += new System.EventHandler(this.TestSequenceBTN_Click);
            // 
            // ChartRefrashTimer
            // 
            this.ChartRefrashTimer.Enabled = true;
            this.ChartRefrashTimer.Tick += new System.EventHandler(this.ChartRefrashTimer_Tick);
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chart1.Forcu_on_plot = null;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(1150, 673);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.X_grid_size = 100D;
            this.chart1.X_location = 520D;
            this.chart1.X_zoom = 0.5D;
            this.chart1.Y_grid_size = 4000D;
            this.chart1.Y_max = 16834D;
            this.chart1.Y_min = -16834D;
            this.chart1.Y_zoom = 0.02D;
            // 
            // saveBTN
            // 
            this.saveBTN.Location = new System.Drawing.Point(3, 337);
            this.saveBTN.Name = "saveBTN";
            this.saveBTN.Size = new System.Drawing.Size(78, 34);
            this.saveBTN.TabIndex = 7;
            this.saveBTN.Text = "保存";
            this.saveBTN.UseVisualStyleBackColor = true;
            this.saveBTN.Click += new System.EventHandler(this.saveBTN_Click);
            // 
            // debugFORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1453, 802);
            this.Controls.Add(splitContainer1);
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "debugFORM";
            this.Text = "SpeedMotorF_debug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.debugFORM_FormClosing);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button TestSequenceBTN;
        private SRB_Chart.Chart chart1;
        private System.Windows.Forms.Timer ChartRefrashTimer;
        private System.Windows.Forms.RichTextBox reportRTC;
        private System.Windows.Forms.Button pauseBTN;
        private System.Windows.Forms.Button saveBTN;
    }
}