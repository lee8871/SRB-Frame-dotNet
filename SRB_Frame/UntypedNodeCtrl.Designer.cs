namespace SRB.Frame
{
    partial class UntypedNodeCtrl
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
            this.nameL = new System.Windows.Forms.Label();
            this.sendRTB = new System.Windows.Forms.RichTextBox();
            this.recvRTB = new System.Windows.Forms.RichTextBox();
            this.AccessBTN = new System.Windows.Forms.Button();
            this.TitleTap = new System.Windows.Forms.ToolTip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.accessTimerOnBTN = new System.Windows.Forms.Button();
            this.AccessT = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // nameL
            // 
            this.nameL.AutoSize = true;
            this.nameL.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameL.ForeColor = System.Drawing.Color.OrangeRed;
            this.nameL.Location = new System.Drawing.Point(3, 0);
            this.nameL.Name = "nameL";
            this.nameL.Size = new System.Drawing.Size(142, 21);
            this.nameL.TabIndex = 0;
            this.nameL.Text = "untypednode";
            this.nameL.Click += new System.EventHandler(this.nameL_Click);
            // 
            // sendRTB
            // 
            this.sendRTB.Location = new System.Drawing.Point(7, 44);
            this.sendRTB.Name = "sendRTB";
            this.sendRTB.Size = new System.Drawing.Size(284, 48);
            this.sendRTB.TabIndex = 1;
            this.sendRTB.Text = "";
            // 
            // recvRTB
            // 
            this.recvRTB.Location = new System.Drawing.Point(7, 134);
            this.recvRTB.Name = "recvRTB";
            this.recvRTB.Size = new System.Drawing.Size(284, 48);
            this.recvRTB.TabIndex = 2;
            this.recvRTB.Text = "";
            // 
            // AccessBTN
            // 
            this.AccessBTN.Location = new System.Drawing.Point(7, 99);
            this.AccessBTN.Name = "AccessBTN";
            this.AccessBTN.Size = new System.Drawing.Size(116, 29);
            this.AccessBTN.TabIndex = 3;
            this.AccessBTN.Text = "Access once";
            this.AccessBTN.UseVisualStyleBackColor = true;
            this.AccessBTN.Click += new System.EventHandler(this.AccessOnce);
            // 
            // TitleTap
            // 
            this.TitleTap.ToolTipTitle = "This node has an unkonw type. May be data errored. You can scan again. Or node er" +
    "ror. ";
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "This node has an unkonw type. May be data errored. You can scan again. Or node er" +
    "ror. ";
            // 
            // accessTimerOnBTN
            // 
            this.accessTimerOnBTN.Location = new System.Drawing.Point(129, 99);
            this.accessTimerOnBTN.Name = "accessTimerOnBTN";
            this.accessTimerOnBTN.Size = new System.Drawing.Size(116, 29);
            this.accessTimerOnBTN.TabIndex = 3;
            this.accessTimerOnBTN.Text = "Access per 100ms";
            this.accessTimerOnBTN.UseVisualStyleBackColor = true;
            this.accessTimerOnBTN.Click += new System.EventHandler(this.accessTimerOnBTN_Click);
            // 
            // AccessT
            // 
            this.AccessT.Tick += new System.EventHandler(this.AccessOnce);
            // 
            // UntypedNodeCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.accessTimerOnBTN);
            this.Controls.Add(this.AccessBTN);
            this.Controls.Add(this.recvRTB);
            this.Controls.Add(this.sendRTB);
            this.Controls.Add(this.nameL);
            this.MaximumSize = new System.Drawing.Size(300, 300);
            this.MinimumSize = new System.Drawing.Size(300, 50);
            this.Name = "UntypedNodeCtrl";
            this.Size = new System.Drawing.Size(300, 185);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameL;
        private System.Windows.Forms.RichTextBox sendRTB;
        private System.Windows.Forms.RichTextBox recvRTB;
        private System.Windows.Forms.Button AccessBTN;
        private System.Windows.Forms.ToolTip TitleTap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button accessTimerOnBTN;
        private System.Windows.Forms.Timer AccessT;
    }
}
