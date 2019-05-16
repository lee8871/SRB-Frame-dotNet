using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB.Frame
{
    public abstract class IByteBank
    {
        protected byte[] bank;
        protected byte[] bank_write_temp;
        private int bank_length;
        protected IByteBank(int bank_length, bool write_to_temp)
        {
            this.bank_length = bank_length;
            bank = new byte[bank_length];
            if (write_to_temp)
            {
                bank_write_temp = new byte[bank_length];
            }
            else
            {
                bank_write_temp = bank;
            }
        }
        protected string getBankString(int diff, int max_len = -1)
        {
            if (max_len == -1)
            {
                max_len = bank_length;
            }
            char[] cs = new char[max_len];
            int i;
            for (i = 0; i < max_len; i++)
            {
                if (bank[diff + i] == 0)
                {
                    break;
                }
                else
                {
                    cs[i] = (char)bank[diff + i];
                }
            }
            string rev = new string(cs, 0, i);
            return rev;
        }
        protected void setBankString(string str, int diff, int max_len = -1)
        {
            if (max_len == -1)
            {
                max_len = bank_length;
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
                bank_write_temp[diff + i] = (byte)ca[i];
            }
            bank_write_temp[diff + i] = (byte)'\0';
            return;
        }


        protected bool getBankBool(int diff, int bit_diff = 0)
        {
            if( (bit_diff >= 8)|| (bit_diff <0 ))
            {
                throw new Exception("bit_diff shold less than 8");
            }
            return ((bank[diff] & (1 << bit_diff)) != 0);
        }
        protected void setBankBool(bool b, int diff, int bit_diff = 0)
        {
            if ((bit_diff >= 8) || (bit_diff < 0))
            {
                throw new Exception("bit_diff shold less than 8");
            }
            if (b)
            {
                bank_write_temp[diff] |= (byte)(1ul << bit_diff);
            }
            else
            {
                bank_write_temp[diff] &= ((byte)(~(1ul << bit_diff)));
            }
        }


        protected byte getBankByte(int diff)
        {
            return bank[diff];
        }
        protected void setBankByte(byte val, int diff)
        {
            bank_write_temp[diff] = val;
            return;
        }


        protected ushort getBankUshort(int diff)
        {
            ushort rev = 0;
            rev += bank[diff + 1];
            rev <<= 8;
            rev += bank[diff]; ;
            return rev;
        }
        protected void setBankUshort(ushort val, int diff)
        {
            bank_write_temp[diff] = (byte)val;
            val >>= 8;
            bank_write_temp[diff + 1] = (byte)val;
            return;
        }


        protected byte[] getBankByteArray(int diff, int len)
        {
            byte[] ba = new byte[len];
            for (int i = 0; i < len; i++)
            {
                ba[i] = bank[diff + i];
            }
            return ba;
        }
        protected void setBankByteArray(byte[] ba, int diff, int len = -1)
        {
            if (len == -1)
            {
                len = ba.Length;
            }
            for (int i = 0; i < len; i++)
            {
                bank_write_temp[diff + i] = ba[i];
            }
            return;
        }

    }
}
