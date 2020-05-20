namespace SRB.Frame
{
    partial class SyncCC
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
            this.TimeLAB = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.calibrationLAB = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TimeLAB
            // 
            this.TimeLAB.AutoSize = true;
            this.TimeLAB.Location = new System.Drawing.Point(12, 7);
            this.TimeLAB.Name = "TimeLAB";
            this.TimeLAB.Size = new System.Drawing.Size(185, 12);
            this.TimeLAB.TabIndex = 0;
            this.TimeLAB.Text = "Sync 212 Clock is: XX.xxxx:xxx";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // calibrationLAB
            // 
            this.calibrationLAB.AutoSize = true;
            this.calibrationLAB.Location = new System.Drawing.Point(12, 24);
            this.calibrationLAB.Name = "calibrationLAB";
            this.calibrationLAB.Size = new System.Drawing.Size(107, 12);
            this.calibrationLAB.TabIndex = 7;
            this.calibrationLAB.Text = "calibration = {0}";
            // 
            // SyncCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.calibrationLAB);
            this.Controls.Add(this.TimeLAB);
            this.Enable_write = false;
            this.Name = "SyncCC";
            this.Controls.SetChildIndex(this.TimeLAB, 0);
            this.Controls.SetChildIndex(this.calibrationLAB, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label TimeLAB;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label calibrationLAB;
    }
}
