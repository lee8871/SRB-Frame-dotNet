namespace SRB.NodeType.PS2_Handle
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
            this.StartBTN = new System.Windows.Forms.Button();
            this.sendTimer = new System.Windows.Forms.Timer(this.components);
            this.StopBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.up = new System.Windows.Forms.Label();
            this.down = new System.Windows.Forms.Label();
            this.right = new System.Windows.Forms.Label();
            this.left = new System.Windows.Forms.Label();
            this.square = new System.Windows.Forms.Label();
            this.circle = new System.Windows.Forms.Label();
            this.cross = new System.Windows.Forms.Label();
            this.trag = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.l2 = new System.Windows.Forms.Label();
            this.l1 = new System.Windows.Forms.Label();
            this.l3 = new System.Windows.Forms.Label();
            this.r3 = new System.Windows.Forms.Label();
            this.r1 = new System.Windows.Forms.Label();
            this.r2 = new System.Windows.Forms.Label();
            this.select = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Label();
            this.RumbleBT = new System.Windows.Forms.Button();
            this.rumbleNUM = new System.Windows.Forms.NumericUpDown();
            this.RightLAB = new System.Windows.Forms.PictureBox();
            this.LeftLAB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.rumbleNUM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightLAB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftLAB)).BeginInit();
            this.SuspendLayout();
            // 
            // StartBTN
            // 
            this.StartBTN.Location = new System.Drawing.Point(224, 0);
            this.StartBTN.Name = "StartBTN";
            this.StartBTN.Size = new System.Drawing.Size(54, 34);
            this.StartBTN.TabIndex = 9;
            this.StartBTN.Text = "Start";
            this.StartBTN.UseVisualStyleBackColor = true;
            this.StartBTN.Click += new System.EventHandler(this.StartBTN_Click);
            // 
            // sendTimer
            // 
            this.sendTimer.Interval = 50;
            this.sendTimer.Tick += new System.EventHandler(this.sendTimer_Tick);
            // 
            // StopBTN
            // 
            this.StopBTN.Location = new System.Drawing.Point(159, 0);
            this.StopBTN.Name = "StopBTN";
            this.StopBTN.Size = new System.Drawing.Size(60, 34);
            this.StopBTN.TabIndex = 9;
            this.StopBTN.Text = "Stop";
            this.StopBTN.UseVisualStyleBackColor = true;
            this.StopBTN.Click += new System.EventHandler(this.StopBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 12;
            // 
            // up
            // 
            this.up.AutoSize = true;
            this.up.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.up.Location = new System.Drawing.Point(55, 45);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(32, 21);
            this.up.TabIndex = 13;
            this.up.Text = "↑";
            // 
            // down
            // 
            this.down.AutoSize = true;
            this.down.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.down.Location = new System.Drawing.Point(55, 82);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(32, 21);
            this.down.TabIndex = 13;
            this.down.Text = "↓";
            // 
            // right
            // 
            this.right.AutoSize = true;
            this.right.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.right.Location = new System.Drawing.Point(73, 62);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(32, 21);
            this.right.TabIndex = 13;
            this.right.Text = "→";
            // 
            // left
            // 
            this.left.AutoSize = true;
            this.left.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.left.Location = new System.Drawing.Point(37, 64);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(32, 21);
            this.left.TabIndex = 13;
            this.left.Text = "←";
            // 
            // square
            // 
            this.square.AutoSize = true;
            this.square.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.square.Location = new System.Drawing.Point(186, 64);
            this.square.Name = "square";
            this.square.Size = new System.Drawing.Size(32, 21);
            this.square.TabIndex = 15;
            this.square.Text = "□";
            // 
            // circle
            // 
            this.circle.AutoSize = true;
            this.circle.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.circle.Location = new System.Drawing.Point(221, 64);
            this.circle.Name = "circle";
            this.circle.Size = new System.Drawing.Size(32, 21);
            this.circle.TabIndex = 16;
            this.circle.Text = "○";
            // 
            // cross
            // 
            this.cross.AutoSize = true;
            this.cross.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cross.Location = new System.Drawing.Point(204, 82);
            this.cross.Name = "cross";
            this.cross.Size = new System.Drawing.Size(32, 21);
            this.cross.TabIndex = 17;
            this.cross.Text = "×";
            // 
            // trag
            // 
            this.trag.AutoSize = true;
            this.trag.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.trag.Location = new System.Drawing.Point(203, 43);
            this.trag.Name = "trag";
            this.trag.Size = new System.Drawing.Size(32, 21);
            this.trag.TabIndex = 18;
            this.trag.Text = "△";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(169, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 12);
            this.label10.TabIndex = 14;
            // 
            // l2
            // 
            this.l2.AutoSize = true;
            this.l2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.l2.Location = new System.Drawing.Point(11, 43);
            this.l2.Name = "l2";
            this.l2.Size = new System.Drawing.Size(34, 21);
            this.l2.TabIndex = 19;
            this.l2.Text = "L2";
            // 
            // l1
            // 
            this.l1.AutoSize = true;
            this.l1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.l1.Location = new System.Drawing.Point(11, 64);
            this.l1.Name = "l1";
            this.l1.Size = new System.Drawing.Size(34, 21);
            this.l1.TabIndex = 19;
            this.l1.Text = "L1";
            // 
            // l3
            // 
            this.l3.AutoSize = true;
            this.l3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.l3.Location = new System.Drawing.Point(11, 103);
            this.l3.Name = "l3";
            this.l3.Size = new System.Drawing.Size(34, 21);
            this.l3.TabIndex = 19;
            this.l3.Text = "L3";
            // 
            // r3
            // 
            this.r3.AutoSize = true;
            this.r3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r3.Location = new System.Drawing.Point(248, 103);
            this.r3.Name = "r3";
            this.r3.Size = new System.Drawing.Size(34, 21);
            this.r3.TabIndex = 20;
            this.r3.Text = "R3";
            // 
            // r1
            // 
            this.r1.AutoSize = true;
            this.r1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r1.Location = new System.Drawing.Point(248, 64);
            this.r1.Name = "r1";
            this.r1.Size = new System.Drawing.Size(34, 21);
            this.r1.TabIndex = 21;
            this.r1.Text = "R1";
            // 
            // r2
            // 
            this.r2.AutoSize = true;
            this.r2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.r2.Location = new System.Drawing.Point(248, 43);
            this.r2.Name = "r2";
            this.r2.Size = new System.Drawing.Size(34, 21);
            this.r2.TabIndex = 22;
            this.r2.Text = "R2";
            // 
            // select
            // 
            this.select.AutoSize = true;
            this.select.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.select.Location = new System.Drawing.Point(102, 85);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(82, 21);
            this.select.TabIndex = 15;
            this.select.Text = "Select";
            // 
            // Start
            // 
            this.Start.AutoSize = true;
            this.Start.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Start.Location = new System.Drawing.Point(108, 114);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(70, 21);
            this.Start.TabIndex = 15;
            this.Start.Text = "Start";
            // 
            // RumbleBT
            // 
            this.RumbleBT.Location = new System.Drawing.Point(93, 0);
            this.RumbleBT.Name = "RumbleBT";
            this.RumbleBT.Size = new System.Drawing.Size(60, 34);
            this.RumbleBT.TabIndex = 9;
            this.RumbleBT.Text = "Rumble";
            this.RumbleBT.UseVisualStyleBackColor = true;
            this.RumbleBT.Click += new System.EventHandler(this.RumbleBT_Click);
            // 
            // rumbleNUM
            // 
            this.rumbleNUM.Location = new System.Drawing.Point(28, 9);
            this.rumbleNUM.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.rumbleNUM.Name = "rumbleNUM";
            this.rumbleNUM.Size = new System.Drawing.Size(59, 21);
            this.rumbleNUM.TabIndex = 23;
            // 
            // RightLAB
            // 
            this.RightLAB.BackColor = System.Drawing.Color.Transparent;
            this.RightLAB.BackgroundImage = global::PS2_Handle.Properties.Resources.right_arrow_256px_1205488_easyicon_net;
            this.RightLAB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RightLAB.Location = new System.Drawing.Point(207, 103);
            this.RightLAB.Name = "RightLAB";
            this.RightLAB.Size = new System.Drawing.Size(32, 32);
            this.RightLAB.TabIndex = 11;
            this.RightLAB.TabStop = false;
            // 
            // LeftLAB
            // 
            this.LeftLAB.BackColor = System.Drawing.Color.Transparent;
            this.LeftLAB.BackgroundImage = global::PS2_Handle.Properties.Resources.left_arrow_247px_1205454_easyicon1;
            this.LeftLAB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LeftLAB.Location = new System.Drawing.Point(49, 103);
            this.LeftLAB.Name = "LeftLAB";
            this.LeftLAB.Size = new System.Drawing.Size(32, 32);
            this.LeftLAB.TabIndex = 11;
            this.LeftLAB.TabStop = false;
            // 
            // Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.rumbleNUM);
            this.Controls.Add(this.RightLAB);
            this.Controls.Add(this.LeftLAB);
            this.Controls.Add(this.r3);
            this.Controls.Add(this.r1);
            this.Controls.Add(this.r2);
            this.Controls.Add(this.l3);
            this.Controls.Add(this.l1);
            this.Controls.Add(this.l2);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.select);
            this.Controls.Add(this.square);
            this.Controls.Add(this.circle);
            this.Controls.Add(this.cross);
            this.Controls.Add(this.trag);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.left);
            this.Controls.Add(this.right);
            this.Controls.Add(this.down);
            this.Controls.Add(this.up);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RumbleBT);
            this.Controls.Add(this.StopBTN);
            this.Controls.Add(this.StartBTN);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 180);
            this.Name = "Ctrl";
            this.Size = new System.Drawing.Size(300, 180);
            this.Load += new System.EventHandler(this.Ctrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rumbleNUM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RightLAB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LeftLAB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartBTN;
        private System.Windows.Forms.Timer sendTimer;
        private System.Windows.Forms.Button StopBTN;
        private System.Windows.Forms.PictureBox RightLAB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label up;
        private System.Windows.Forms.Label down;
        private System.Windows.Forms.Label right;
        private System.Windows.Forms.Label left;
        private System.Windows.Forms.Label square;
        private System.Windows.Forms.Label circle;
        private System.Windows.Forms.Label cross;
        private System.Windows.Forms.Label trag;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label l2;
        private System.Windows.Forms.Label l1;
        private System.Windows.Forms.Label l3;
        private System.Windows.Forms.Label r3;
        private System.Windows.Forms.Label r1;
        private System.Windows.Forms.Label r2;
        private System.Windows.Forms.Label select;
        private System.Windows.Forms.Label Start;
        private System.Windows.Forms.Button RumbleBT;
        private System.Windows.Forms.NumericUpDown rumbleNUM;
        private System.Windows.Forms.PictureBox LeftLAB;
    }
}
