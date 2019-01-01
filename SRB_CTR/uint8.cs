using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System//SRB_CTR.nsByteExtensions
{
    static class Expand_Byte
    {
        static public string ToHexSt(this byte b)
        {
            char[] ca = new char[2];
            ca[0] = (char)(b / 16);
            if (ca[0] > 9)
            {
                ca[0] += (char)('A' - 10);
            }
            else
            {
                ca[0] += '0';
            }
            ca[1] = (char)(b % 16);
            if (ca[1] > 9)
            {
                ca[1] += (char)('A' - 10);
            }
            else
            {
                ca[1] += '0';
            }
            return new string(ca, 0, 2);
        }
       static public int enterRound(this int i,int min ,int max)
        {
            if (i >= max)
            {
                return max;
            }
            if(i<=min)
            {
                return min;
            }
            return i;
        }
        static public string AsByteToHexSt(this int i)
        {
            byte b = (byte)i;
            return b.ToHexSt();
        }
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
           for(int i = 0;i<len;i++)
           {
               byte b = ba[i];
               s += b.ToHexSt() + ' ';
           }
           return s;
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
                s += "0x"+b.ToHexSt() + ',';
            }
            return "("+s+")";
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
       static public byte ByteLow(this ushort u16)
       {
           return (byte)(u16);
       }
       static public byte ByteHigh(this ushort u16)
       {
           return (byte)(u16 >> 8);
       }
       static public ushort GetUint16(this byte[] b, int num)
       {
           return (ushort)((int)b[num] + (((int)b[num + 1]) << 8));
       }
    }
}
