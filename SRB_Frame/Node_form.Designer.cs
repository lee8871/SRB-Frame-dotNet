namespace SRB.Frame
{
    partial class Node_form
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
            this.clusters = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // clusters
            // 
            this.clusters.AutoSize = true;
            this.clusters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.clusters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clusters.Location = new System.Drawing.Point(0, 0);
            this.clusters.MaximumSize = new System.Drawing.Size(306, 6550);
            this.clusters.MinimumSize = new System.Drawing.Size(306, 91);
            this.clusters.Name = "clusters";
            this.clusters.Size = new System.Drawing.Size(306, 91);
            this.clusters.TabIndex = 0;
            // 
            // Node_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(306, 91);
            this.Controls.Add(this.clusters);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Node_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "</NodeName>";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel clusters;

    }
}