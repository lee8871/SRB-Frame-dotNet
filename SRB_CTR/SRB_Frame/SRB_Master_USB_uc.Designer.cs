﻿namespace SRB_CTR
{
    partial class SRB_Master_USB_Uc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SRB_Master_USB_Uc));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.CloseConnectBTN = new System.Windows.Forms.ToolStripButton();
            this.comSelectCB = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.renameBT = new System.Windows.Forms.ToolStripButton();
            this.LoseRateLAB = new System.Windows.Forms.ToolStripLabel();
            this.EnterNameTB = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(23, 23);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.CloseConnectBTN,
            this.comSelectCB,
            this.toolStripButton1,
            this.renameBT,
            this.LoseRateLAB});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(378, 30);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(32, 27);
            this.toolStripLabel1.Text = "USB";
            // 
            // CloseConnectBTN
            // 
            this.CloseConnectBTN.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CloseConnectBTN.Image = ((System.Drawing.Image)(resources.GetObject("CloseConnectBTN.Image")));
            this.CloseConnectBTN.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseConnectBTN.Name = "CloseConnectBTN";
            this.CloseConnectBTN.Size = new System.Drawing.Size(27, 27);
            this.CloseConnectBTN.Text = "断开串口";
            this.CloseConnectBTN.Click += new System.EventHandler(this.CloseConnectBTN_Click);
            // 
            // comSelectCB
            // 
            this.comSelectCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comSelectCB.MaxDropDownItems = 16;
            this.comSelectCB.Name = "comSelectCB";
            this.comSelectCB.Size = new System.Drawing.Size(125, 30);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(27, 27);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // renameBT
            // 
            this.renameBT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.renameBT.Image = global::SRB_CTR.Properties.Resources.pencil_512px_1175838_easyicon_net;
            this.renameBT.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.renameBT.Name = "renameBT";
            this.renameBT.Size = new System.Drawing.Size(27, 27);
            this.renameBT.Text = "Rename SRB-USB Hardware";
            this.renameBT.Click += new System.EventHandler(this.renameHardware_click);
            // 
            // LoseRateLAB
            // 
            this.LoseRateLAB.Name = "LoseRateLAB";
            this.LoseRateLAB.Size = new System.Drawing.Size(39, 27);
            this.LoseRateLAB.Text = "{0}‰";
            // 
            // EnterNameTB
            // 
            this.EnterNameTB.BackColor = System.Drawing.SystemColors.Window;
            this.EnterNameTB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.EnterNameTB.Location = new System.Drawing.Point(69, 2);
            this.EnterNameTB.Name = "EnterNameTB";
            this.EnterNameTB.Size = new System.Drawing.Size(125, 23);
            this.EnterNameTB.TabIndex = 2;
            this.EnterNameTB.Visible = false;
            // 
            // SRB_Master_USB_Uc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.EnterNameTB);
            this.Controls.Add(this.toolStrip1);
            this.MaximumSize = new System.Drawing.Size(378, 32);
            this.MinimumSize = new System.Drawing.Size(378, 32);
            this.Name = "SRB_Master_USB_Uc";
            this.Size = new System.Drawing.Size(378, 32);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox comSelectCB;
        private System.Windows.Forms.ToolStripButton CloseConnectBTN;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton renameBT;
        private System.Windows.Forms.TextBox EnterNameTB;
        private System.Windows.Forms.ToolStripLabel LoseRateLAB;
    }
}