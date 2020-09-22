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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chart1 = new SRB_Chart.Chart();
            this.TestSequenceBTN = new System.Windows.Forms.Button();
            this.getSpeedTableBTN = new System.Windows.Forms.Button();
            this.RunTestBTN = new System.Windows.Forms.Button();
            this.ChartRefrashTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chart1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TestSequenceBTN);
            this.splitContainer1.Panel2.Controls.Add(this.getSpeedTableBTN);
            this.splitContainer1.Panel2.Controls.Add(this.RunTestBTN);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(1242, 705);
            this.splitContainer1.SplitterDistance = 1137;
            this.splitContainer1.TabIndex = 0;
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chart1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chart1.Forcu_on_plot = null;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(1134, 673);
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
            // TestSequenceBTN
            // 
            this.TestSequenceBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TestSequenceBTN.Location = new System.Drawing.Point(11, 597);
            this.TestSequenceBTN.Name = "TestSequenceBTN";
            this.TestSequenceBTN.Size = new System.Drawing.Size(78, 47);
            this.TestSequenceBTN.TabIndex = 4;
            this.TestSequenceBTN.Text = "执行序列";
            this.TestSequenceBTN.UseVisualStyleBackColor = true;
            this.TestSequenceBTN.Click += new System.EventHandler(this.TestSequenceBTN_Click);
            // 
            // getSpeedTableBTN
            // 
            this.getSpeedTableBTN.Location = new System.Drawing.Point(11, 12);
            this.getSpeedTableBTN.Name = "getSpeedTableBTN";
            this.getSpeedTableBTN.Size = new System.Drawing.Size(78, 47);
            this.getSpeedTableBTN.TabIndex = 3;
            this.getSpeedTableBTN.Text = "get Speed Table";
            this.getSpeedTableBTN.UseVisualStyleBackColor = true;
            this.getSpeedTableBTN.Click += new System.EventHandler(this.getSpeedTableBTN_Click);
            // 
            // RunTestBTN
            // 
            this.RunTestBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RunTestBTN.Location = new System.Drawing.Point(11, 650);
            this.RunTestBTN.Name = "RunTestBTN";
            this.RunTestBTN.Size = new System.Drawing.Size(78, 47);
            this.RunTestBTN.TabIndex = 0;
            this.RunTestBTN.Text = "Run";
            this.RunTestBTN.UseVisualStyleBackColor = true;
            this.RunTestBTN.Click += new System.EventHandler(this.RunTestBTN_Click);
            // 
            // ChartRefrashTimer
            // 
            this.ChartRefrashTimer.Enabled = true;
            this.ChartRefrashTimer.Tick += new System.EventHandler(this.ChartRefrashTimer_Tick);
            // 
            // debugFORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 705);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "debugFORM";
            this.Text = "SpeedMotorF_debug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.debugFORM_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button RunTestBTN;
        private System.Windows.Forms.Button getSpeedTableBTN;
        private System.Windows.Forms.Button TestSequenceBTN;
        private SRB_Chart.Chart chart1;
        private System.Windows.Forms.Timer ChartRefrashTimer;
    }
}