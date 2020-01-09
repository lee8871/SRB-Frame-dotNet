using System;

namespace SRB.Frame
{
    public class ByteBank
    {
        public byte this[int i] { get => ba[i]; set=> ba[i]=value; }
        private byte[] ba;
        private byte[] ba_temp;
        private int length;
        private bool is_write_to_temp;
        public int Length => length;
        public byte[] temp => ba_temp;


        public ByteBank(int bank_length, bool is_write_to_temp)
        {
            this.is_write_to_temp = is_write_to_temp;
            this.length = bank_length;
            ba = new byte[bank_length];
            if (is_write_to_temp)
            {
                ba_temp = new byte[bank_length];
            }
            else
            {
                ba_temp = ba;
            }
        }

        internal void writeDone()
        {
            if (is_write_to_temp == false)
            {
                throw new Exception("这个bank不是双缓冲的，不能进行初始化");
            }
            for (int i = 0; i < Length; i++)
            {
                ba[i] = ba_temp[i];
            }
        }
        public void writeInit()
        {
            if (is_write_to_temp == false)
            {
                throw new Exception("这个bank不是双缓冲的，不能进行初始化");
            }
            for (int i = 0; i < Length; i++)
            {
                ba_temp[i] = ba[i];
            }
        }

        public string getBankString(int diff, int max_len = -1)
        {
            if (max_len == -1)
            {
                max_len = length;
            }
            char[] cs = new char[max_len];
            int i;
            for (i = 0; i < max_len; i++)
            {
                if (ba[diff + i] == 0)
                {
                    break;
                }
                else
                {
                    cs[i] = (char)ba[diff + i];
                }
            }
            string rev = new string(cs, 0, i);
            return rev;
        }
        public void setBankString(string str, int diff, int max_len = -1)
        {
            if (max_len == -1)
            {
                max_len = length;
            }
            char[] ca = str.ToCharArray();
            if (ca.Length >= max_len)//there should a \0 in the end. So ca len shold small than max 
            {
                throw new Exception("string too long!");
            }
            //if (ca[ca.Length - 1] != '\0')
            //{
            //    throw new Exception("transform char array do not has\0 at they end.");

            //}
            int i;
            for (i = 0; i < ca.Length; i++)
            {
                ba_temp[diff + i] = (byte)ca[i];
            }
            ba_temp[diff + i] = (byte)'\0';
            return;
        }


        public bool getBankBool(int diff, int bit_diff = 0)
        {
            if ((bit_diff >= 8) || (bit_diff < 0))
            {
                throw new Exception("bit_diff shold less than 8");
            }
            return ((ba[diff] & (1 << bit_diff)) != 0);
        }
        public void setBankBool(bool b, int diff, int bit_diff = 0)
        {
            if ((bit_diff >= 8) || (bit_diff < 0))
            {
                throw new Exception("bit_diff shold less than 8");
            }
            if (b)
            {
                ba_temp[diff] |= (byte)(1ul << bit_diff);
            }
            else
            {
                ba_temp[diff] &= ((byte)(~(1ul << bit_diff)));
            }
        }


        public byte getBankByte(int diff)
        {
            return ba[diff];
        }
        public void setBankByte(byte val, int diff)
        {
            ba_temp[diff] = val;
            return;
        }


        public ushort getBankUshort(int diff)
        {
            ushort rev = 0;
            rev += ba[diff + 1];
            rev <<= 8;
            rev += ba[diff]; ;
            return rev;
        }
        public uint getBankUint(int diff)
        {
            uint rev = 0; 
            rev += ba[diff + 3];
            rev <<= 8;
            rev += ba[diff + 2];
            rev <<= 8;
            rev += ba[diff + 1];
            rev <<= 8;
            rev += ba[diff]; ;
            return rev;
        }
        public void setBankUshort(ushort val, int diff)
        {
            ba_temp[diff] = (byte)val;
            val >>= 8;
            ba_temp[diff + 1] = (byte)val;
            return;
        }


        public byte[] getBankByteArray(int diff, int len)
        {
            byte[] ba = new byte[len];
            for (int i = 0; i < len; i++)
            {
                ba[i] = this.ba[diff + i];
            }
            return ba;
        }
        public void setBankByteArray(byte[] ba, int diff, int len = -1)
        {
            if (len == -1)
            {
                len = ba.Length;
            }
            for (int i = 0; i < len; i++)
            {
                ba_temp[diff + i] = ba[i];
            }
            return;
        }

    }
}
