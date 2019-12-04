﻿using System;
namespace SRB.Frame.Cluster
{
    public class ErrorCluster : ICluster
    {
        public string error_text { get => bank.getBankString(1, 24); }
        public int err_num { get => (int)bank.getBankByte(0); }
        public byte[] parameter { get => bank.getBankByteArray(25, 5); }

        public ErrorCluster(BaseNode n)
            : base(n, 2, 30) { }
        public override void write()
        {
            throw new Exception("read only cluster can not write.");
        }
        public override void writeRecv(Access ac)
        {
            throw new Exception("read only cluster can not write.");
        }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new ErrorCC(this);
        }
        public override string ToString()
        {
            return "Error Cluster";
        }
    }
}
