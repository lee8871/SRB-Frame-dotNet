using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB.Frame
{
    public static class ByteArrayExpand
    {
        static public string ToHexSt(this byte[] ba, int len = -1)
        {
            if (ba == null)
            {
                return "<null>";
            }
            if (len == -1)
            {
                len = ba.Length;
                if (len == 0)
                {
                    return "<empty>";
                }
            }
            string s = "";
            for (int i = 0; i < len; i++)
            {
                byte b = ba[i];
                s += b.ToHexSt() + ' ';
            }
            return s;
        }

        static public string ToArrayString(this byte[] ba, int len = -1)
        {
            if (ba == null)
            {
                return "{}";
            }
            if (len == -1)
            {
                len = ba.Length;
                if (len == 0)
                {
                    return "{};";
                }
            }
            string s = "";
            s += "{";
            int i = 0;
            while (true)
            {
                byte b = ba[i];
                //  s += "0x" + b.ToHexSt();
                s += b.ToString();
                i++;
                if (i < len)
                {
                    s += ",";
                }
                else
                {
                    s += "};";
                    return s;
                }
            }
        }
        static public byte[] SubArray(this byte[] ba, int len)
        {
            if (ba == null)
            {
                return null;
            }
            byte[] nba = new byte[len];
            for (int i = 0; i < len; i++)
            {
                nba[i] = ba[i];
            }
            return nba;
        }
        static public string ToPythonTuple(this byte[] ba, int len = -1)
        {
            if (ba == null)
            {
                return "('null')";
            }
            if (len == -1)
            {
                len = ba.Length;
                if (len == 0)
                {
                    return "('empty')";
                }
            }
            string s = "";
            for (int i = 0; i < len; i++)
            {
                byte b = ba[i];
                s += "0x" + b.ToHexSt() + ',';
            }
            return "(" + s + ")";
        }
        static public ushort GetUint16(this byte[] b, int num)
        {
            return (ushort)((int)b[num] + (((int)b[num + 1]) << 8));
        }
   
        static public uint GetUint(this byte[] b, int bit_diff, int len)
        {
            if ((len < 0) || (len >= 32))
            {
                throw new Exception("bit length should be [0,32].");
            }
            uint mask = ~(uint)(-1 << len);
            uint rev = 0 ;
            int byte_diff = bit_diff / 8;
            int bit_shift = bit_diff % 8;
            int counter = -bit_shift;
            rev = ((uint)b[byte_diff]) >> -counter;
            counter += 8;
            while (counter < len)
            {
                byte_diff++;
                   rev |= ((uint)b[byte_diff]) << counter;
                counter += 8;
            }
            return rev & mask;
        }
        static public void SetUint(this byte[] b, uint val, int bit_diff, int len)
        {
            if ((len < 0) || (len >= 32))
            {
                throw new Exception("bit length should be [0,32].");
            }
            uint mask = ~(uint)(-1 << len);
            int byte_diff = bit_diff / 8;
            int bit_shift = bit_diff % 8;
            int counter = -bit_shift;
            val &= mask;
            b[byte_diff] &= (byte)~(mask << -counter);
            b[byte_diff] |= (byte)(val << -counter);
            counter += 8;
            while (counter < len)
            {
                byte_diff++;
                b[byte_diff] &= (byte)(mask >> counter);
                b[byte_diff] |= (byte)(val >> counter);
                counter += 8;
            }
            return;
        }

        static public int GetInt(this byte[] b, int bit_diff, int len)
        {
            if ((len < 0) || (len >= 32))
            {
                throw new Exception("bit length should be [0,32].");
            }
            uint rev = b.GetUint(bit_diff, len);
            if(0 == (rev & (uint)(0x1 << (len - 1))))
            {
                return (int)rev;
            }
            else
            {
                uint mask = ~(uint)(-1 << len);
                return (int)(rev | ~mask);
            }
        }
        static public void SetInt(this byte[] b, int val, int bit_diff, int len)
        {
            b.SetUint((uint)val, bit_diff, len);
        }
        static public void test_byte_array_handle()
        {
            byte[] test_array = new byte[10];
            while (true)
            {
                test_array.SetUint(228, 0, 10);
                test_array.SetUint(566, 10, 10);
                test_array.SetInt(1020, 20, 10);
                test_array.SetInt(354, 30, 10);
                uint a; int b;
                a = test_array.GetUint(0, 10);
                a = test_array.GetUint(10, 10);
                b = test_array.GetInt(20, 10);
                b = test_array.GetInt(30, 10);
                a = test_array.GetUint(0, 20);
                a = test_array.GetUint(20, 20);
            }
        }
    }
}
