using System;

namespace SRB.Frame
{
    internal class Mapping
    {
        private byte[] up_mapping;
        private byte[] down_mapping;

        public int upMapping(int num)
        {
            return (int)up_mapping[num];
        }
        public int downMapping(int num)
        {
            return (int)down_mapping[num];
        }

        public int Up_len { get => up_mapping.Length; }
        public int Down_len { get => down_mapping.Length; }
        public Mapping(byte[] raw)
        {
            if (raw.Length < 2)
            {
                throw new Exception(
                    "The Mapping Raw Array shold has up_len, down_len and Mapping Table "
                    );
            }
            int up_len = raw[0];
            int down_len = raw[1];
            if (raw.Length < 2 + up_len + down_len)
            {
                throw new Exception(string.Format("The Mapping Raw Array has" +
                    "up_len = {0}, " +
                    "down_len = {1}," +
                    "the totle length shold be {2} ," +
                    "but the Raw Array length is {3}",
                   up_len, down_len, up_len + down_len + 2, raw.Length));
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
