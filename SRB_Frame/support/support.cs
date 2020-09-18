using System;
using System.Drawing;

namespace SRB.Frame
{
    public static class support
    {
        internal static Random random = new Random();

        private static Color color_red = Color.FromArgb(unchecked((int)0xffbb433b));
        private static Color color_dank = Color.FromArgb(unchecked((int)0xff332f2f));
        private static Color color_navy = Color.FromArgb(unchecked((int)0xff334753));
        private static Color color_blue = Color.FromArgb(unchecked((int)0xff4a8bcf));
        private static Color color_moon = Color.FromArgb(unchecked((int)0xffadb5c7));
        private static Color 果绿色 = Color.FromArgb(unchecked((int)0xff97e7be));

        private static Color 奶油色 = Color.FromArgb(unchecked((int)0xfff89168));

        private static Color color_pink = Color.FromArgb(unchecked((int)0xfffb548a));


        public static Color Color_red => color_red;
        public static Color Color_dank => color_dank;
        public static Color Color_navy => color_navy;
        public static Color Color_moon => color_moon;

        public static Color Color_HighLight => color_pink;
        public static Color Color_BackGround => color_blue;






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
        static internal UInt32 byteToUint32(byte[] ba,int offset)
        {
            UInt32 rev = 0;
            offset += 3;
            for (int i = 0; i < 3; i++)
            {
                rev += ba[offset--];
                rev <<= 8;
            }
            rev += ba[offset];
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

        static public double getElapsedMs(this System.Diagnostics.Stopwatch sw)
        {
            return (1000.0 * sw.ElapsedTicks) / System.Diagnostics.Stopwatch.Frequency; ;
        }



    }



    public static class Expand_DateTime
    {
        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        static public int ToUtc(this System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>  
        /// Unix时间戳转为C#格式时间  
        /// </summary>  
        /// <param name="timeStamp">Unix时间戳格式,例如1482115779</param>  
        /// <returns>C#格式时间</returns>  
        public static void getUtc(ref this DateTime time, int utc)
        {
            time = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            time = time.AddSeconds(utc);
            return;
        }
    }

}

