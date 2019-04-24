using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace System
{
    public static class support
    {
        internal static Random random = new Random();

        private static Color color_red = Color.FromArgb(unchecked((int)0xffbb433b));
        private static Color color_dank = Color.FromArgb(unchecked((int)0xff332f2f));
        private static Color color_navy = Color.FromArgb(unchecked((int)0xff334753));
        private static Color color_blue = Color.FromArgb(unchecked((int)0xff4a8bcf));
        private static Color color_moon = Color.FromArgb(unchecked((int)0xffadb5c7));


        public static Color Color_red { get => color_red; }
        public static Color Color_dank { get => color_dank; }
        public static Color Color_navy { get => color_navy; }
        public static Color Color_blue { get => color_blue; }
        public static Color Color_moon { get => color_moon; }


        internal enum YNU
        {
            UnKnow, Yes, No
        }

        static internal UInt16 byteToUint16(byte low, byte high)
        {
            UInt16 rev = 0;
            rev += high;
            rev <<= 8;
            rev += low;
            return rev;
        }
        static internal byte Uint16LowByte(UInt16 u16)
        {
            return (byte)u16;
        }
        static internal byte Uint16HighByte(UInt16 u16)
        {
            return (byte)(u16 >> 8);
        }

        static internal string ushorToString(ushort b)
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
        static internal string getTime()
        {
            string a = "[";
            a += System.DateTime.Now.Hour;
            a += ":";
            a += System.DateTime.Now.Minute;
            a += ":";
            a += System.DateTime.Now.Second;
            a += "]";
            return a;
        }




    }
}

