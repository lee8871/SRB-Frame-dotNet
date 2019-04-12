using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB.Frame;

namespace SRB.Frame.Cluster
{
    public class MappingCluster:ICluster
    {
        private const int totle_length=28;
        public int up_len { get => getBankByte(0);}
        public int down_len { get => getBankByte(1);}
        public byte[] up_mapping { get => getBankByteArray(2, up_len); }
        public byte[] down_mapping { get => getBankByteArray(2 + up_len, down_len); }
        public byte[] mapping { get => getBankByteArray(0, up_len + 2 + down_len); }
        public EventHandler eMappingChanged;
        public string description;
        public MappingCluster(byte ID, BaseNode n, string dsc = null)
            : base(n, ID, 30)
        {
            if(dsc == null)
            {
                dsc = "Mapping" + (ID - 3);
            }
            description = dsc;
        }
        public void setMapping(byte[] up, byte[] down)
        {
            if(up.Length+down.Length> totle_length)
            {
                throw new Exception("totleLengthshold less than 28");
            }
            int i = 0;
            bank_write_temp = new byte[up.Length + down.Length + 2];
            bank_write_temp[i++] = (byte)up.Length;
            bank_write_temp[i++] = (byte)down.Length;
            foreach(byte b in up)
            {
                bank_write_temp[i++] = b;
            }
            foreach (byte b in down)
            {
                bank_write_temp[i++] = b;
            }
        }
        public bool setMapping(byte[] mba)
        {
            if (checkMapping(mba)=="done")
            {
                bank_write_temp = mba;
                return true;
            }
            return false;
        }
        public string checkMapping(byte[] mba)
        {
            if (mba.Length > totle_length + 2)
            {
                return
                    "Array too long. Shold Less than 30";
            }
            if (mba.Length < 2)
            {
                return
                    "Array too short. No up and down length.";
            }
            int len;
            len = mba.Length;
            if (mba.Length != 2 + mba[0] + mba[1])
            {
                return
                    "Length error. [0] + [1] ≠ Length -2";
            }
            return "done";
        }

        public override System.Windows.Forms.UserControl createControl()
        {
            return new MappingCC(this);
        }
        public override string ToString()
        {
            return string.Format("{1}<ID={0}>", Clustr_ID, description);
        }
    }
}
