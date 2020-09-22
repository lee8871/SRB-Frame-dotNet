namespace SRB_Chart
{
    partial class UserControl1
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.chart1 = new SRB_Chart.Chart();
            this.SuspendLayout();
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar1.LargeChange = 1;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 306);
            this.hScrollBar1.Maximum = 2;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(568, 16);
            this.hScrollBar1.TabIndex = 1;
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.chart1.Dock = System.Windows.Forms.DockStyle.Top;
            this.chart1.Forcu_on_plot = null;
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(568, 225);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.X_grid_size = 100D;
            this.chart1.X_location = 520D;
            this.chart1.X_zoom = 0.5D;
            this.chart1.Y_grid_size = 100D;
            this.chart1.Y_max = 330D;
            this.chart1.Y_min = -120D;
            this.chart1.Y_zoom = 0.5D;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.chart1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(568, 322);
            this.ResumeLayout(false);

        }

        #endregion

        private Chart chart1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
    }
}
