using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB.Ahex
{
    static class Ahex
    {
        public static string byteArrayToAhex(byte[] ba)
        {
            char[] cha = new char[ba.Length * 2 + 2 + 3];
            int ch_counter = 0;
            cha[ch_counter++] = '[';
            byte checksum = 0;
            foreach (byte b in ba)
            {
                cha[ch_counter++] = (char)((0x0f & b) + 'A');
                cha[ch_counter++] = (char)((0x0f & (b >> 4)) + 'A');
                checksum ^= b;
            }
            cha[ch_counter++] = ']';
            cha[ch_counter++] = '-';
            cha[ch_counter++] = (char)((0x0f & checksum) + 'A');
            cha[ch_counter++] = (char)((0x0f & (checksum >> 4)) + 'A');
            return new string(cha);
        }
        public static byte[] ahexToByteArray(string st)
        {
            int begin = st.IndexOf('[') + 1;
            int length = st.IndexOf(']') - begin;

            string ahexST = st.Substring(begin, length);
            begin = st.IndexOf('-') + 1;
            length = 2;
            string sumST = st.Substring(begin, length);


            byte[] ba = new byte[ahexST.Length / 2];
            byte checksum = 0; 
            byte b;
            for (int i = 0; i < ba.Length; i++) { 
                b = (byte)((ahexST[i * 2 + 0]-'A')+((ahexST[i * 2 + 1] - 'A')<<4));
                checksum ^= b;
                ba[i] = b;
            }
            b = (byte)((sumST[0] - 'A') +( (sumST[1] - 'A') << 4));
            if(b == checksum)
            {
                return ba;
            }
            return null;
        }
    }
}
