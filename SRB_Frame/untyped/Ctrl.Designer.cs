﻿namespace SRB.Frame.untyped
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
            this.mappingRTB = new System.Windows.Forms.RichTextBox();
            this.MappingBTN = new System.Windows.Forms.Button();
            this.recvRTB = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // mappingRTB
            // 
            this.mappingRTB.Location = new System.Drawing.Point(7, 27);
            this.mappingRTB.Name = "mappingRTB";
            this.mappingRTB.Size = new System.Drawing.Size(218, 48);
            this.mappingRTB.TabIndex = 1;
            this.mappingRTB.Text = "";
            // 
            // MappingBTN
            // 
            this.MappingBTN.Location = new System.Drawing.Point(231, 39);
            this.MappingBTN.Name = "MappingBTN";
            this.MappingBTN.Size = new System.Drawing.Size(60, 36);
            this.MappingBTN.TabIndex = 28;
            this.MappingBTN.Text = "Mapping";
            this.MappingBTN.UseVisualStyleBackColor = true;
            // 
            // recvRTB
            // 
            this.recvRTB.Location = new System.Drawing.Point(6, 81);
            this.recvRTB.Name = "recvRTB";
            this.recvRTB.Size = new System.Drawing.Size(284, 48);
            this.recvRTB.TabIndex = 2;
            this.recvRTB.Text = "";
            // 
            // Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Controls.Add(this.MappingBTN);
            this.Controls.Add(this.recvRTB);
            this.Controls.Add(this.mappingRTB);
            this.MinimumSize = new System.Drawing.Size(300, 50);
            this.Name = "Ctrl";
            this.Size = new System.Drawing.Size(300, 132);
            this.Controls.SetChildIndex(this.mappingRTB, 0);
            this.Controls.SetChildIndex(this.recvRTB, 0);
            this.Controls.SetChildIndex(this.MappingBTN, 0);
            this.Controls.SetChildIndex(this.RunStopBTN, 0);
            this.Controls.SetChildIndex(this.HelpBTN, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox mappingRTB;
        private System.Windows.Forms.Button MappingBTN;
        private System.Windows.Forms.RichTextBox recvRTB;
    }
}
