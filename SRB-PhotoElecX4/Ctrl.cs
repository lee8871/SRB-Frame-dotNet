﻿using SRB.Frame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace SRB.NodeType.PhotoElecX4
{
    internal partial class Ctrl : INodeControl
    {

        private Interpreter datas;
        Node node;
        public Ctrl(Node n) :
            base(n)
        {
            datas = (Interpreter)n.Datas;
            InitializeComponent();
            node = n;
            node.eBankChangeByAccess += N_eBankChangeByAccess;
            tableInit();
        }

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            node.eBankChangeByAccess -= N_eBankChangeByAccess;
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void tableInit()
        {
            for (int i = 0; i < datas.Sensor_num; i++) {
                
               int col=ADCtable.Columns.Add("S" + i, "S" + i);
                ADCtable.Columns[col].Width = 40;
                ADCtable.Columns[col].ReadOnly = true;
            }
        }
        private void N_eBankChangeByAccess(object sender, EventArgs e)
        {
            for (int i = 0; i<datas.Sensor_num; i++)
            {
                if (datas.value(i) != -1)
                {
                    ADCtable[i, 0].Value = datas.value(i);
                }
            }
        }
        protected override void OnAccess()
        {
            base.OnAccess();
        }
    }

}