using System;

namespace SRB.Frame
{
    public class MappingCluster : BaseNode.ICluster
    {
        private const int totle_length = 28;
        public int up_len { get => bank.getBankByte(0); }
        public int down_len { get => bank.getBankByte(1); }
        public byte[] up_mapping { get => bank.getBankByteArray(2, up_len); }
        public byte[] down_mapping { get => bank.getBankByteArray(2 + up_len, down_len); }
        public byte[] mapping { get => bank.getBankByteArray(0, up_len + 2 + down_len); }
        public EventHandler eMappingChanged;
        public string description;
        public MappingCluster(byte ID, BaseNode n, string dsc = null)
            : base(n, ID, 30)
        {
            if (dsc == null)
            {
                dsc = "Mapping" + (ID - 3);
            }
            description = dsc;
        }
        public void setMapping(byte[] up, byte[] down)
        {
            if (up.Length + down.Length > totle_length)
            {
                throw new Exception("totleLengthshold less than 28");
            }
            int i = 0;
            bank.temp[i++] = (byte)up.Length;
            bank.temp[i++] = (byte)down.Length;
            foreach (byte b in up)
            {
                bank.temp[i++] = b;
            }
            foreach (byte b in down)
            {
                bank.temp[i++] = b;
            }
        }
        public bool setMapping(byte[] mba)
        {
            if (checkMapping(mba) == "done")
            {
                int length = mba.Length;
                if(length> bank.Length)
                {
                    length = bank.Length;
                }
                for (int i = 0; i < length; i++)
                {
                    bank.temp[i] = mba[i];
                }
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

        protected override System.Windows.Forms.Control createControl()
        {
            return new MappingCC(this);
        }
        public override string ToString()
        {
            return string.Format("{1}<ID={0}>", CID, description);
        }
    }
}
