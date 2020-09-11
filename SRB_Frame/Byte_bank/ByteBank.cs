using System;

namespace SRB.Frame
{
    
    public class ByteBank
    {
        public byte this[int i] { get => ba[i]; set => send_ba[i] = value; }
        public uint this[int bit_diff, int len] { get => ba.GetUint(bit_diff, len); set => send_ba.SetUint(value, bit_diff, len); }
        public uint this[BitField bf] { get => ba.GetUint(bf); set => send_ba.SetUint(value, bf); }

        private byte[] ba;
        private byte[] send_ba;

        public byte[] Load_ba => ba;
        private int length;

        private bool is_write_to_temp;
        public int Length => length;
        public byte[] temp => send_ba;


        public ByteBank(int bank_length, bool is_write_to_temp)
        {
            this.is_write_to_temp = is_write_to_temp;
            this.length = bank_length;
            ba = new byte[bank_length];
            if (is_write_to_temp)
            {
                send_ba = new byte[bank_length];
            }
            else
            {
                send_ba = ba;
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
                ba[i] = send_ba[i];
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
                send_ba[i] = ba[i];
            }
        }

        public string getBankString(int diff, int max_len = -1)
        {
            if (max_len == -1)
            {
                max_len = length-diff;
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
                send_ba[diff + i] = (byte)ca[i];
            }
            send_ba[diff + i] = (byte)'\0';
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
                send_ba[diff] |= (byte)(1ul << bit_diff);
            }
            else
            {
                send_ba[diff] &= ((byte)(~(1ul << bit_diff)));
            }
        }


        public byte getBankByte(int diff)
        {
            return ba[diff];
        }
        public void setBankByte(byte val, int diff)
        {
            send_ba[diff] = val;
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
        public short getBankShort(int diff)
        {
            return (short)getBankUshort(diff);
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
        public int getBankInt(int diff)
        {
            return (int)getBankUint(diff);
        }
        public void setBankUshort(ushort val, int diff)
        {
            send_ba[diff] = (byte)val;
            val >>= 8;
            send_ba[diff + 1] = (byte)val;
            return;
        }
        public void setBankShort(short val, int diff)
        {
            setBankUshort((ushort)val, diff);
        }
        public void setBankUint(uint val, int diff)
        {
            send_ba[diff] = (byte)val;
            val >>= 8;
            send_ba[diff + 1] = (byte)val;
            val >>= 8;
            send_ba[diff + 2] = (byte)val;
            val >>= 8;
            send_ba[diff + 3] = (byte)val;
            return;
        }
        public void setBankInt(int val, int diff)
        {
            setBankUint((uint)val, diff);
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
                send_ba[diff + i] = ba[i];
            }
            return;
        }
        public uint getBankUint(int diff, int bit_length ,int bit_diff = 0)
        {
            if((bit_diff<0)|| (bit_diff >= 8))
            {
                throw new Exception("bit_diff should be within 0 to 7.");
            }
            if ((bit_length < 0) || (bit_length >= 32))
            {
                throw new Exception("bit_length should be within 0 to 32.");
            }
            ulong rev = 0;
            for (int i = 4; i > 0; i--)
            {
                if (ba.Length > diff + i)
                {
                    rev += ba[diff + i];
                    rev <<= 8;
                }
            }
            rev += ba[diff];
            rev >>=bit_diff;
            rev &= ~(0xffffffffffffffff << bit_length);
            return (uint)rev;
        }


    }
}
