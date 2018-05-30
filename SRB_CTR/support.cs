using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
	public static class support
    {

        public static Random random = new Random();
        public enum YNU
        {
            UnKnow,Yes,No
        }
		static public UInt16 byteToUint16(byte low, byte high)
		{
			UInt16 rev = 0;
			rev += high;
			rev <<= 8;
			rev += low;
			return rev;
		}
		static public byte Uint16LowByte(UInt16 u16)
		{
			return (byte)u16;
		}
		static public byte Uint16HighByte(UInt16 u16)
		{
			return (byte)(u16>>8);
		}

        static public string ByteArrayToString(byte[] ba)
        {
            string s="";
            foreach (byte b in ba)
            {
                s += ByteToString(b) + ' ';
            }
            return s;
        }
        static public string ByteToString(byte b)
        {
            string a = "";
            if (b < 0x10)
            {
                a += "0";
            }
            a += String.Format("{0:X}", b);
            return a;
        }
        static public string ushorToString(ushort b)
        {
            string a = "";
            if (b < 0x10)
            {
                a += "000";
            }
            else if (b < 0x100)
            {
                a += "00";
            }
            else if (b < 0x1000)
            {
                a += "0";
            }
            a += String.Format("{0:X}", b);
            return a;
        }
        static public string getTime()
        {
            string a ="[";
            a+=System.DateTime.Now.Hour;
            a+=":";
            a+=System.DateTime.Now.Minute;
            a += ":";
            a += System.DateTime.Now.Second;
            a += "]";
            return a ;
        }
    }
}
