
namespace SRB.NodeType.PhotoElecX4
{
    partial class TestForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mainChart = new SRB_Chart.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.ADCtable = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ADCtable)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.mainChart);
            this.splitContainer1.Panel1MinSize = 300;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.ADCtable);
            this.splitContainer1.Panel2MinSize = 300;
            this.splitContainer1.Size = new System.Drawing.Size(1111, 561);
            this.splitContainer1.SplitterDistance = 807;
            this.splitContainer1.TabIndex = 1;
            // 
            // mainChart
            // 
            this.mainChart.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.mainChart.Dock = System.Windows.Forms.DockStyle.Top;
            this.mainChart.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mainChart.Forcu_on_plot = null;
            this.mainChart.Location = new System.Drawing.Point(0, 0);
            this.mainChart.Name = "mainChart";
            this.mainChart.Size = new System.Drawing.Size(807, 512);
            this.mainChart.TabIndex = 1;
            this.mainChart.Text = "chart1";
            this.mainChart.X_grid_size = 1D;
            this.mainChart.X_location = 0D;
            this.mainChart.X_zoom = 100D;
            this.mainChart.Y_grid_size = 200D;
            this.mainChart.Y_max = 1024D;
            this.mainChart.Y_min = 0D;
            this.mainChart.Y_zoom = 0.5D;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 466);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ADCtable
            // 
            this.ADCtable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ADCtable.Dock = System.Windows.Forms.DockStyle.Top;
            this.ADCtable.Location = new System.Drawing.Point(0, 0);
            this.ADCtable.Name = "ADCtable";
            this.ADCtable.RowTemplate.Height = 23;
            this.ADCtable.Size = new System.Drawing.Size(300, 460);
            this.ADCtable.TabIndex = 28;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 561);
            this.Controls.Add(this.splitContainer1);
            this.MaximumSize = new System.Drawing.Size(60000, 600);
            this.MinimumSize = new System.Drawing.Size(600, 600);
            this.Name = "TestForm";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ADCtable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button1;
        private SRB_Chart.Chart mainChart;
        private System.Windows.Forms.DataGridView ADCtable;
    }
}