namespace SRB_CTR.nsFrame{
    partial class AccessDisplayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccessDisplayer));
            this.mainDGV = new System.Windows.Forms.DataGridView();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.send = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SC = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.fastLeftBTN = new System.Windows.Forms.ToolStripButton();
            this.leftBTN = new System.Windows.Forms.ToolStripButton();
            this.pageTB = new System.Windows.Forms.ToolStripTextBox();
            this.rightBTN = new System.Windows.Forms.ToolStripButton();
            this.fastRightBTN = new System.Windows.Forms.ToolStripButton();
            this.pageNum = new System.Windows.Forms.ToolStripLabel();
            this.mainRTC = new System.Windows.Forms.RichTextBox();
            this.main_dgv_updateT = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mainDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SC)).BeginInit();
            this.SC.Panel1.SuspendLayout();
            this.SC.Panel2.SuspendLayout();
            this.SC.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainDGV
            // 
            this.mainDGV.AllowUserToAddRows = false;
            this.mainDGV.AllowUserToDeleteRows = false;
            this.mainDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mainDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.mainDGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.mainDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.time,
            this.Status,
            this.address,
            this.send,
            this.recv});
            this.mainDGV.Location = new System.Drawing.Point(0, 28);
            this.mainDGV.Name = "mainDGV";
            this.mainDGV.ReadOnly = true;
            this.mainDGV.RowTemplate.Height = 23;
            this.mainDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainDGV.Size = new System.Drawing.Size(913, 367);
            this.mainDGV.TabIndex = 1;
            // 
            // time
            // 
            this.time.HeaderText = "time";
            this.time.Name = "time";
            this.time.ReadOnly = true;
            this.time.Width = 54;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 66;
            // 
            // address
            // 
            this.address.HeaderText = "address";
            this.address.Name = "address";
            this.address.ReadOnly = true;
            this.address.Width = 72;
            // 
            // send
            // 
            this.send.HeaderText = "send";
            this.send.Name = "send";
            this.send.ReadOnly = true;
            this.send.Width = 54;
            // 
            // recv
            // 
            this.recv.HeaderText = "recv";
            this.recv.Name = "recv";
            this.recv.ReadOnly = true;
            this.recv.Width = 54;
            // 
            // SC
            // 
            this.SC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SC.Location = new System.Drawing.Point(0, 0);
            this.SC.Name = "SC";
            this.SC.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SC.Panel1
            // 
            this.SC.Panel1.Controls.Add(this.toolStrip1);
            this.SC.Panel1.Controls.Add(this.mainDGV);
            // 
            // SC.Panel2
            // 
            this.SC.Panel2.Controls.Add(this.mainRTC);
            this.SC.Size = new System.Drawing.Size(913, 561);
            this.SC.SplitterDistance = 395;
            this.SC.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fastLeftBTN,
            this.leftBTN,
            this.pageTB,
            this.rightBTN,
            this.fastRightBTN,
            this.pageNum});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(913, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // fastLeftBTN
            // 
            this.fastLeftBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fastLeftBTN.Image = ((System.Drawing.Image)(resources.GetObject("fastLeftBTN.Image")));
            this.fastLeftBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fastLeftBTN.Name = "fastLeftBTN";
            this.fastLeftBTN.Size = new System.Drawing.Size(23, 22);
            this.fastLeftBTN.Text = "toolStripButton2";
            this.fastLeftBTN.Click += new System.EventHandler(this.fastLeftBTN_Click);
            // 
            // leftBTN
            // 
            this.leftBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.leftBTN.Image = ((System.Drawing.Image)(resources.GetObject("leftBTN.Image")));
            this.leftBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.leftBTN.Name = "leftBTN";
            this.leftBTN.Size = new System.Drawing.Size(23, 22);
            this.leftBTN.Text = "toolStripButton3";
            this.leftBTN.Click += new System.EventHandler(this.leftBTN_Click);
            // 
            // pageTB
            // 
            this.pageTB.Name = "pageTB";
            this.pageTB.Size = new System.Drawing.Size(28, 25);
            // 
            // rightBTN
            // 
            this.rightBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rightBTN.Image = ((System.Drawing.Image)(resources.GetObject("rightBTN.Image")));
            this.rightBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rightBTN.Name = "rightBTN";
            this.rightBTN.Size = new System.Drawing.Size(23, 22);
            this.rightBTN.Text = "toolStripButton1";
            this.rightBTN.Click += new System.EventHandler(this.rightBTN_Click);
            // 
            // fastRightBTN
            // 
            this.fastRightBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fastRightBTN.Image = ((System.Drawing.Image)(resources.GetObject("fastRightBTN.Image")));
            this.fastRightBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fastRightBTN.Name = "fastRightBTN";
            this.fastRightBTN.Size = new System.Drawing.Size(23, 22);
            this.fastRightBTN.Text = "toolStripButton4";
            this.fastRightBTN.Click += new System.EventHandler(this.fastRightBTN_Click);
            // 
            // pageNum
            // 
            this.pageNum.Name = "pageNum";
            this.pageNum.Size = new System.Drawing.Size(15, 22);
            this.pageNum.Text = "0";
            // 
            // mainRTC
            // 
            this.mainRTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainRTC.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainRTC.Location = new System.Drawing.Point(0, 0);
            this.mainRTC.Name = "mainRTC";
            this.mainRTC.ShowSelectionMargin = true;
            this.mainRTC.Size = new System.Drawing.Size(913, 162);
            this.mainRTC.TabIndex = 0;
            this.mainRTC.Text = "";
            // 
            // main_dgv_updateT
            // 
            this.main_dgv_updateT.Enabled = true;
            this.main_dgv_updateT.Tick += new System.EventHandler(this.main_dgv_updateT_Tick);
            // 
            // AccessDisplayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 561);
            this.Controls.Add(this.SC);
            this.Name = "AccessDisplayer";
            ((System.ComponentModel.ISupportInitialize)(this.mainDGV)).EndInit();
            this.SC.Panel1.ResumeLayout(false);
            this.SC.Panel1.PerformLayout();
            this.SC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SC)).EndInit();
            this.SC.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView mainDGV;
        private System.Windows.Forms.SplitContainer SC;
        private System.Windows.Forms.RichTextBox mainRTC;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.DataGridViewTextBoxColumn send;
        private System.Windows.Forms.DataGridViewTextBoxColumn recv;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton fastLeftBTN;
        private System.Windows.Forms.ToolStripButton leftBTN;
        private System.Windows.Forms.ToolStripTextBox pageTB;
        private System.Windows.Forms.ToolStripButton rightBTN;
        private System.Windows.Forms.ToolStripButton fastRightBTN;
        private System.Windows.Forms.Timer main_dgv_updateT;
        private System.Windows.Forms.ToolStripLabel pageNum;
    }
}