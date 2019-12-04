﻿using System;


namespace SRB.Frame.Cluster
{
    public class InformationCluster : ICluster
    {
        public string type { get => bank.getBankString(6, 17); }
        public int major_version { get => bank.getBankByte(0); }
        public int minor_version { get => bank.getBankByte(1); }
        public int SRB_major_version { get => bank.getBankByte(2); }
        public int SRB_minor_version { get => bank.getBankByte(3); }
        public int time_stamp { get => bank.getBankUshort(4); }

        public InformationCluster(BaseNode n)
            : base(n, 1, 23)
        {
            char[] ca = "Unknow".ToCharArray();
            int i;
            for (i = 0; i < ca.Length; i++)
            {
                bank[6 + i] = (byte)ca[i];
            }
            bank[6 + i] = (byte)'\0';
        }
        public override void write()
        {
            throw new Exception("read only cluster can not write.");
        }
        public override void writeRecv(Access ac)
        {
            if (ac.Send_data.Length == 2)
            {
                return;
            }
            throw new Exception("read only cluster can not write.");
        }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new InformationCC(this);
        }
        public override string ToString()
        {
            return "Information Cluster";
        }

        public void resetNode()
        {
            Access ac;
            byte[] b = new byte[2];
            int i = 0;
            b[i++] = cID;
            b[i++] = (byte)'R';
            ac = new Access(this,this.parent_node, Access.PortEnum.Cgf, b);
            parent_node.singleAccess(ac);
        }
        public void factorySettingNode()
        {
            Access ac;
            byte[] b = new byte[2];
            int i = 0;
            b[i++] = cID;
            b[i++] = (byte)'F';
            ac = new Access(this, this.parent_node, Access.PortEnum.Cgf, b);
            parent_node.singleAccess(ac);
        }

    }
}
