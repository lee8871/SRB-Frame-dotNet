﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace SRB.Frame.Cluster
{
    public class AddressCluster : ICluster
    {
        public byte error_behave;

        public byte addr { get => bank[0]; set => bank[0] = value; }
        public string name { get => getBankString(1, 17); set => setBankString(value, 1, 17); }
        public byte error_behavior { get => getBankByte(18); set => setBankByte(value,18); }
        public byte addr_new { get => bank_write[0]; set => bank_write[0] = value; }

        public override void writeRecv(Access ac)
        {
            if (ac.Recv_error == false)
            {
                if(ac.Send_data.Length==2)
                {
                    return;
                }
                if (addr != ac.Send_data[1])
                {
                    base.writeRecv(ac);
                    parent_node.onAddrChanged();
                }
                else {
                    base.writeRecv(ac);
                }
            }
        }
        public AddressCluster(byte ID,Node n,byte addr)
            : base(ID,n,19)
        {
            bank[0]  = addr;
        }

        public override UserControl createControl()
        {
            return new AddressCC(this);
        }
        public override string ToString()
        {
            return string.Format("Address Cluster", Clustr_ID.ToHexSt());
        }

        public bool isNewAddrAvaliable(byte addr)
        {
            return  parent_node.isNewAddrAvaliable(addr);
        }
        public enum LedAddrType{High,Low,Close};
        public void ledAddr(LedAddrType adt)
        {
            Access ac;
            byte[] b = new byte[2];
            int i = 0;
            b[i++] = Clustr_ID;
            switch(adt)
            {
                case LedAddrType.Close:
                    b[i++] = 0xf5;break;
                case LedAddrType.High:
                    b[i++] = 0xf4; break;
                case LedAddrType.Low:
                    b[i++] = 0xf3; break;

            }
            ac = new Access(this.parent_node, Access.PortEnum.Cgf, b);
            parent_node.singleAccess(ac);
        }
    }
}