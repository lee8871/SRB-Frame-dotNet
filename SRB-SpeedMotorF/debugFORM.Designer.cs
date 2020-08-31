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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.TestSequenceBTN = new System.Windows.Forms.Button();
            this.getSpeedTableBTN = new System.Windows.Forms.Button();
            this.RunTestBTN = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(484, 261);
            this.splitContainer1.SplitterDistance = 379;
            this.splitContainer1.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(379, 261);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // TestSequenceBTN
            // 
            this.TestSequenceBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TestSequenceBTN.Location = new System.Drawing.Point(11, 153);
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
            this.RunTestBTN.Location = new System.Drawing.Point(11, 206);
            this.RunTestBTN.Name = "RunTestBTN";
            this.RunTestBTN.Size = new System.Drawing.Size(78, 47);
            this.RunTestBTN.TabIndex = 0;
            this.RunTestBTN.Text = "Run";
            this.RunTestBTN.UseVisualStyleBackColor = true;
            this.RunTestBTN.Click += new System.EventHandler(this.RunTestBTN_Click);
            // 
            // debugFORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 261);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "debugFORM";
            this.Text = "SpeedMotorF_debug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.debugFORM_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button RunTestBTN;
        private System.Windows.Forms.Button getSpeedTableBTN;
        private System.Windows.Forms.Button TestSequenceBTN;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}