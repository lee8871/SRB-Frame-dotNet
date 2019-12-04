namespace SRB.Frame
{
    partial class UpdateForm
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.infoRTB = new System.Windows.Forms.RichTextBox();
            this.HoldBTN = new System.Windows.Forms.Button();
            this.UpdateBTN = new System.Windows.Forms.Button();
            this.RunBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // infoRTB
            // 
            this.infoRTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoRTB.Location = new System.Drawing.Point(0, 0);
            this.infoRTB.Name = "infoRTB";
            this.infoRTB.Size = new System.Drawing.Size(306, 165);
            this.infoRTB.TabIndex = 0;
            this.infoRTB.Text = "";
            // 
            // HoldBTN
            // 
            this.HoldBTN.Location = new System.Drawing.Point(12, 171);
            this.HoldBTN.Name = "HoldBTN";
            this.HoldBTN.Size = new System.Drawing.Size(65, 58);
            this.HoldBTN.TabIndex = 1;
            this.HoldBTN.Text = "Hold";
            this.HoldBTN.UseVisualStyleBackColor = true;
            this.HoldBTN.Click += new System.EventHandler(this.HoldBTN_Click);
            // 
            // UpdateBTN
            // 
            this.UpdateBTN.Location = new System.Drawing.Point(151, 171);
            this.UpdateBTN.Name = "UpdateBTN";
            this.UpdateBTN.Size = new System.Drawing.Size(70, 58);
            this.UpdateBTN.TabIndex = 2;
            this.UpdateBTN.Text = "Download";
            this.UpdateBTN.UseVisualStyleBackColor = true;
            this.UpdateBTN.Click += new System.EventHandler(this.UpdateBTN_Click);
            // 
            // RunBTN
            // 
            this.RunBTN.Location = new System.Drawing.Point(227, 171);
            this.RunBTN.Name = "RunBTN";
            this.RunBTN.Size = new System.Drawing.Size(67, 58);
            this.RunBTN.TabIndex = 4;
            this.RunBTN.Text = "Run";
            this.RunBTN.UseVisualStyleBackColor = true;
            this.RunBTN.Click += new System.EventHandler(this.RunBTN_Click);
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(306, 241);
            this.Controls.Add(this.RunBTN);
            this.Controls.Add(this.UpdateBTN);
            this.Controls.Add(this.HoldBTN);
            this.Controls.Add(this.infoRTB);
            this.Name = "UpdateForm";
            this.Text = "UpdateNode";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox infoRTB;
        private System.Windows.Forms.Button HoldBTN;
        private System.Windows.Forms.Button UpdateBTN;
        private System.Windows.Forms.Button RunBTN;
    }
}