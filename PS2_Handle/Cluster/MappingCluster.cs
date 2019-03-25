using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB.Frame;

namespace SRB.NodeType.PS2_Handle
{
    class MappingCluster:ICluster
    {
        private const int totle_length=28;
        public int up_len { get => getBankByte(0);}
        public int down_len { get => getBankByte(1);}
        public byte[] up_mapping { get => getBankByteArray(2, up_len); }
        public byte[] down_mapping { get => getBankByteArray(2 + up_len, down_len); }
        public byte[] mapping { get => getBankByteArray(0, up_len + 2 + down_len); }
        public EventHandler eMappingChanged;
        public string description;
        public MappingCluster(byte ID, Node n, string dsc = "Mapping")
            : base(n, ID, 30)
        {
            description = dsc;
        }
        public void setMapping(byte[] up, byte[] down)
        {
            if(up.Length+down.Length> totle_length)
            {
                throw new Exception("totleLengthshold less than 28");
            }
            int i = 0;
            bank_write = new byte[up.Length + down.Length + 2];
            bank_write[i++] = (byte)up.Length;
            bank_write[i++] = (byte)down.Length;
            foreach(byte b in up)
            {
                bank_write[i++] = b;
            }
            foreach (byte b in down)
            {
                bank_write[i++] = b;
            }

        }


        public override System.Windows.Forms.UserControl createControl()
        {
            return new MappingCC(this);
        }
        public override string ToString()
        {
            return string.Format("{1}<ID={0}>", Clustr_ID.ToHexSt(), description);
        }
    }
}
