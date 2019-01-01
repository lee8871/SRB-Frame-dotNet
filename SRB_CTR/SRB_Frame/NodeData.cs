using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRB_CTR.SRB_Frame
{

    class Mapping
    {
        private byte[] up_mapping;
        private byte[] down_mapping;

        public byte[] Up_mapping { get => up_mapping; }
        public byte[] Down_mapping { get => down_mapping; }

        public int Up_len { get => up_mapping.Length; }
        public int Down_len { get => down_mapping.Length; }
        public Mapping(byte[] raw)
        {
            if (raw.Length <= 2)
            {
                throw new Exception(
                    "The Mapping Raw Array shold has up_len, down_len and Mapping Table "
                    );
            }
            int up_len = raw[0];
            int down_len = raw[1];
            if (raw.Length < 2+ up_len+ down_len)
            {
                throw new Exception(string.Format("The Mapping Raw Array has" +
                    "up_len = {0}, " +
                    "down_len = {1}," +
                    "the totle length shold be {2} ," +
                    "but the Raw Array length is {3}",
                   up_len, down_len, up_len+down_len+ 2, raw.Length));
            }
            else
            {
                int j = 2;
                up_mapping = new byte[up_len];
                down_mapping = new byte[down_len];
                for (int i = 0; i < up_len; i++)
                {
                    up_mapping[i] = raw[j++];
                }
                for (int i = 0; i < down_len; i++)
                {
                    down_mapping[i] = raw[j++];
                }
            }
        }
    }
}
