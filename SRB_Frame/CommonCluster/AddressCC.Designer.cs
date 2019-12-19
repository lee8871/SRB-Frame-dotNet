namespace SRB.Frame
{
    partial class AddressCC
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
            this.label2 = new System.Windows.Forms.Label();
            this.NodeNameL = new System.Windows.Forms.Label();
            this.NodeNameTB = new System.Windows.Forms.TextBox();
            this.AddrL = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.AddrNUM = new System.Windows.Forms.NumericUpDown();
            this.highBTN = new System.Windows.Forms.Button();
            this.lowBTN = new System.Windows.Forms.Button();
            this.closeBTN = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AddrNUM)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(9, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Address:";
            // 
            // NodeNameL
            // 
            this.NodeNameL.AutoSize = true;
            this.NodeNameL.Location = new System.Drawing.Point(9, 49);
            this.NodeNameL.Name = "NodeNameL";
            this.NodeNameL.Size = new System.Drawing.Size(95, 12);
            this.NodeNameL.TabIndex = 0;
            this.NodeNameL.Text = "some node XXXXX";
            // 
            // NodeNameTB
            // 
            this.NodeNameTB.Location = new System.Drawing.Point(146, 46);
            this.NodeNameTB.MaxLength = 30;
            this.NodeNameTB.Name = "NodeNameTB";
            this.NodeNameTB.Size = new System.Drawing.Size(150, 21);
            this.NodeNameTB.TabIndex = 1;
            this.NodeNameTB.Text = "some_NODE_12345";
            // 
            // AddrL
            // 
            this.AddrL.AutoSize = true;
            this.AddrL.Location = new System.Drawing.Point(76, 28);
            this.AddrL.Name = "AddrL";
            this.AddrL.Size = new System.Drawing.Size(17, 12);
            this.AddrL.TabIndex = 0;
            this.AddrL.Text = "00";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // AddrNUM
            // 
            this.AddrNUM.Location = new System.Drawing.Point(147, 26);
            this.AddrNUM.Name = "AddrNUM";
            this.AddrNUM.Size = new System.Drawing.Size(50, 21);
            this.AddrNUM.TabIndex = 7;
            // 
            // highBTN
            // 
            this.highBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.highBTN.Location = new System.Drawing.Point(96, 0);
            this.highBTN.Name = "highBTN";
            this.highBTN.Size = new System.Drawing.Size(22, 22);
            this.highBTN.TabIndex = 8;
            this.highBTN.Text = "H";
            this.highBTN.UseVisualStyleBackColor = false;
            this.highBTN.Click += new System.EventHandler(this.highBTN_Click);
            // 
            // lowBTN
            // 
            this.lowBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lowBTN.Location = new System.Drawing.Point(121, 0);
            this.lowBTN.Name = "lowBTN";
            this.lowBTN.Size = new System.Drawing.Size(22, 22);
            this.lowBTN.TabIndex = 8;
            this.lowBTN.Text = "L";
            this.lowBTN.UseVisualStyleBackColor = false;
            this.lowBTN.Click += new System.EventHandler(this.lowBTN_Click);
            // 
            // closeBTN
            // 
            this.closeBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBTN.Location = new System.Drawing.Point(146, 0);
            this.closeBTN.Name = "closeBTN";
            this.closeBTN.Size = new System.Drawing.Size(22, 22);
            this.closeBTN.TabIndex = 8;
            this.closeBTN.Text = "C";
            this.closeBTN.UseVisualStyleBackColor = false;
            this.closeBTN.Click += new System.EventHandler(this.closeBTN_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(9, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Addr Color:";
            // 
            // AddressCC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.closeBTN);
            this.Controls.Add(this.lowBTN);
            this.Controls.Add(this.highBTN);
            this.Controls.Add(this.AddrNUM);
            this.Controls.Add(this.NodeNameL);
            this.Controls.Add(this.AddrL);
            this.Controls.Add(this.NodeNameTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "AddressCC";
            this.Size = new System.Drawing.Size(300, 70);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.NodeNameTB, 0);
            this.Controls.SetChildIndex(this.AddrL, 0);
            this.Controls.SetChildIndex(this.NodeNameL, 0);
            this.Controls.SetChildIndex(this.AddrNUM, 0);
            this.Controls.SetChildIndex(this.highBTN, 0);
            this.Controls.SetChildIndex(this.lowBTN, 0);
            this.Controls.SetChildIndex(this.closeBTN, 0);
            ((System.ComponentModel.ISupportInitialize)(this.AddrNUM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label NodeNameL;
        private System.Windows.Forms.TextBox NodeNameTB;
        private System.Windows.Forms.Label AddrL;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.NumericUpDown AddrNUM;
        private System.Windows.Forms.Button highBTN;
        private System.Windows.Forms.Button lowBTN;
        private System.Windows.Forms.Button closeBTN;
        private System.Windows.Forms.Label label3;
    }
}
