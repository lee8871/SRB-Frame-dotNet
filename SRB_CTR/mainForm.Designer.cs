namespace SRB_CTR
{
	partial class mainForm
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

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.comPortControl1 = new lemonReceiver.ToNode.ComPortControl();
            this.SuspendLayout();
            // 
            // comPortControl1
            // 
            this.comPortControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.comPortControl1.Location = new System.Drawing.Point(0, 541);
            this.comPortControl1.MinimumSize = new System.Drawing.Size(40, 20);
            this.comPortControl1.Name = "comPortControl1";
            this.comPortControl1.Size = new System.Drawing.Size(784, 20);
            this.comPortControl1.TabIndex = 0;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.comPortControl1);
            this.Name = "mainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

		}

		#endregion

        private lemonReceiver.ToNode.ComPortControl comPortControl1;





    }
}

