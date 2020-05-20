namespace SRB_CTR
{
    partial class SyncBroadcastC
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SyncBroadcastC));
            this.do_syncBTN = new System.Windows.Forms.Button();
            this.infoRTC = new System.Windows.Forms.RichTextBox();
            this.saveBTN = new System.Windows.Forms.Button();
            this.ReadBTN = new System.Windows.Forms.Button();
            this.timerAuto = new System.Windows.Forms.Timer(this.components);
            this.timer_interval_NUM = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.timer_interval_NUM)).BeginInit();
            this.SuspendLayout();
            // 
            // do_syncBTN
            // 
            this.do_syncBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.do_syncBTN.Location = new System.Drawing.Point(384, 433);
            this.do_syncBTN.Name = "do_syncBTN";
            this.do_syncBTN.Size = new System.Drawing.Size(71, 85);
            this.do_syncBTN.TabIndex = 0;
            this.do_syncBTN.Text = "Do Sync";
            this.do_syncBTN.UseVisualStyleBackColor = true;
            this.do_syncBTN.Click += new System.EventHandler(this.do_syncBTN_Click);
            this.do_syncBTN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.do_syncBTN_MouseUp);
            // 
            // infoRTC
            // 
            this.infoRTC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.infoRTC.Location = new System.Drawing.Point(3, 3);
            this.infoRTC.Name = "infoRTC";
            this.infoRTC.Size = new System.Drawing.Size(375, 515);
            this.infoRTC.TabIndex = 1;
            this.infoRTC.Text = "";
            // 
            // saveBTN
            // 
            this.saveBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBTN.BackColor = System.Drawing.Color.Gold;
            this.saveBTN.Location = new System.Drawing.Point(384, 298);
            this.saveBTN.Name = "saveBTN";
            this.saveBTN.Size = new System.Drawing.Size(71, 59);
            this.saveBTN.TabIndex = 2;
            this.saveBTN.Text = "Save";
            this.saveBTN.UseVisualStyleBackColor = false;
            this.saveBTN.Click += new System.EventHandler(this.saveBTN_Click);
            // 
            // ReadBTN
            // 
            this.ReadBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ReadBTN.Location = new System.Drawing.Point(384, 390);
            this.ReadBTN.Name = "ReadBTN";
            this.ReadBTN.Size = new System.Drawing.Size(71, 37);
            this.ReadBTN.TabIndex = 3;
            this.ReadBTN.Text = "Read";
            this.ReadBTN.UseVisualStyleBackColor = true;
            this.ReadBTN.Click += new System.EventHandler(this.readBTN_Click);
            this.ReadBTN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ReadBTN_MouseUp);
            // 
            // timerAuto
            // 
            this.timerAuto.Enabled = true;
            this.timerAuto.Interval = 500;
            this.timerAuto.Tick += new System.EventHandler(this.timer500_Tick);
            // 
            // timer_interval_NUM
            // 
            this.timer_interval_NUM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.timer_interval_NUM.Location = new System.Drawing.Point(384, 363);
            this.timer_interval_NUM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.timer_interval_NUM.Name = "timer_interval_NUM";
            this.timer_interval_NUM.Size = new System.Drawing.Size(71, 21);
            this.timer_interval_NUM.TabIndex = 7;
            this.timer_interval_NUM.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.timer_interval_NUM.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // SyncBroadcastC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 522);
            this.Controls.Add(this.timer_interval_NUM);
            this.Controls.Add(this.ReadBTN);
            this.Controls.Add(this.saveBTN);
            this.Controls.Add(this.do_syncBTN);
            this.Controls.Add(this.infoRTC);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SyncBroadcastC";
            this.Text = "Sync Debug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SyncBroadcastC_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.timer_interval_NUM)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button do_syncBTN;
        private System.Windows.Forms.RichTextBox infoRTC;
        private System.Windows.Forms.Button saveBTN;
        private System.Windows.Forms.Button ReadBTN;
        private System.Windows.Forms.Timer timerAuto;
        private System.Windows.Forms.NumericUpDown timer_interval_NUM;
    }
}
