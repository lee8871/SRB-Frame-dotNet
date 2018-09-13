using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SRB_access
{
    public class Access
    {
        public byte addr;
        public byte sno;
        public DataGridViewRow row;
        private static byte Sno_counter = 0;

        public byte[] send_data;
        public byte send_crc;
        public byte send_bfc
        {
            get { return (byte)(port * 32 + send_data.Length + 2); }
        }
        public int port;
        public int send_len
        {
            get { return (send_data.Length + 2); }
        }


        public byte recv_bfc;
        public byte recv_len
        {
            get { return (byte)(recv_bfc & 0x1f); }
        }
        public byte[] recv_data = new byte[0];
        public byte recv_crc;

        public Access(byte addr, byte[] data, byte port)
        {
            this.addr = addr;
            this.sno = Sno_counter;
            Sno_counter++;
            if (Sno_counter >= 0xf0)
            {
                Sno_counter = 0;
            }
            this.send_data = data;
            if (port > 7)
            {
                port = 7;
            }
            this.port = port;
            send_crc = 0;
            send_crc = Crc_table[send_crc ^ addr];
            send_crc = Crc_table[send_crc ^ send_bfc];
            foreach (byte b in data)
            {
                send_crc = Crc_table[send_crc ^ b];
            }
        }
        public Access(byte addr)
        {
            this.addr = addr;
            this.send_data = null;
        }
        public override string ToString()
        {
            string st_recv, st_send;
            if (recv_data.Length == 0)
            {
                st_recv = "【None】";
            }
            else
            {
                st_recv =recv_data.ToHexSt();
            }
            if (send_data.Length == 0)
            {
                st_send = "【None】";
            }
            else
            {
                st_send = send_data.ToHexSt();
            }
            string st = System.String.Format(
                 @"Addr:{0}.{1} Send:{2}Recv:{3}",
                 addr.ToString(),
                 port, st_send, st_recv);
            return st;
        }
        public string ToHtml()
        {
            string st_recv, st_send;
            if (recv_data.Length == 0)
            {
                st_recv = "【None】";
            }
            else
            {
                st_recv = recv_data.ToHexSt();
            }
            if (send_data.Length == 0)
            {
                st_send = "【None】";
            }
            else
            {
                st_send = send_data.ToHexSt();
            }
            string st = System.String.Format(
                 @"
                <span class=note>Addr:</span>
                <span class=sno>{0}.{1} </span>
                <span class=note>Send:</span>
                <span class=send>{2}</span>
                <span class=note>Recv:</span>
                <span class=recv>{3}</span>
                <br>",
                 addr.ToString(),
                 port, st_send, st_recv);
            return st;
        }
        public int toUartByteArray(ref byte[] ba)
        {
            ba = new byte[70];
            int i = 0;
            ba[i++] = 0xf5;
            ba[i++] = sno;
            ba[i++] = addr;
            if (0xf5 == (ba[i++] = send_bfc))
            {
                ba[i++] = 0xf3;
            }
            foreach (byte b in send_data)
            {
                if (0xf5 == (ba[i++] = b))
                {
                    ba[i++] = 0xf3;
                }
            }
            if (0xf5 == (ba[i++] = send_crc))
            {
                ba[i++] = 0xf3;
            }
            return i;
        }
        public byte[] toUartByteArray()
        {
            Queue<byte> bq = new Queue<byte>();
            bq.Enqueue(0xf5);
            bq.Enqueue(addr);
            bq.Enqueue(send_bfc);
            if (0xf5 == send_bfc)
            {
                bq.Enqueue(0xf3);
            }
            foreach (byte b in send_data)
            {
                bq.Enqueue(b);
                if (0xf5 == b)
                {
                    bq.Enqueue(0xf3);
                }
            }
            return bq.ToArray();
        }



        public void fromUartGetBytes(byte[] bytes)
        {
            if (bytes.Length >= 1)
            {
                this.recv_bfc = bytes[0];
                this.recv_data = new byte[bytes.Length - 1];
                for(int i = 1;i<(recv_len-1);i++)
                {
                    this.recv_data[i - 1] = bytes[i];
                }
            }
        }
        private static byte[] Crc_table = 
		{
			0x00,0x31,0x62,0x53,0xc4,0xf5,0xa6,0x97,0xb9,0x88,0xdb,0xea,0x7d,0x4c,0x1f,0x2e,
			0x43,0x72,0x21,0x10,0x87,0xb6,0xe5,0xd4,0xfa,0xcb,0x98,0xa9,0x3e,0x0f,0x5c,0x6d,
			0x86,0xb7,0xe4,0xd5,0x42,0x73,0x20,0x11,0x3f,0x0e,0x5d,0x6c,0xfb,0xca,0x99,0xa8,
			0xc5,0xf4,0xa7,0x96,0x01,0x30,0x63,0x52,0x7c,0x4d,0x1e,0x2f,0xb8,0x89,0xda,0xeb,
			0x3d,0x0c,0x5f,0x6e,0xf9,0xc8,0x9b,0xaa,0x84,0xb5,0xe6,0xd7,0x40,0x71,0x22,0x13,
			0x7e,0x4f,0x1c,0x2d,0xba,0x8b,0xd8,0xe9,0xc7,0xf6,0xa5,0x94,0x03,0x32,0x61,0x50,
			0xbb,0x8a,0xd9,0xe8,0x7f,0x4e,0x1d,0x2c,0x02,0x33,0x60,0x51,0xc6,0xf7,0xa4,0x95,
			0xf8,0xc9,0x9a,0xab,0x3c,0x0d,0x5e,0x6f,0x41,0x70,0x23,0x12,0x85,0xb4,0xe7,0xd6,
			0x7a,0x4b,0x18,0x29,0xbe,0x8f,0xdc,0xed,0xc3,0xf2,0xa1,0x90,0x07,0x36,0x65,0x54,
			0x39,0x08,0x5b,0x6a,0xfd,0xcc,0x9f,0xae,0x80,0xb1,0xe2,0xd3,0x44,0x75,0x26,0x17,
			0xfc,0xcd,0x9e,0xaf,0x38,0x09,0x5a,0x6b,0x45,0x74,0x27,0x16,0x81,0xb0,0xe3,0xd2,
			0xbf,0x8e,0xdd,0xec,0x7b,0x4a,0x19,0x28,0x06,0x37,0x64,0x55,0xc2,0xf3,0xa0,0x91,
			0x47,0x76,0x25,0x14,0x83,0xb2,0xe1,0xd0,0xfe,0xcf,0x9c,0xad,0x3a,0x0b,0x58,0x69,
			0x04,0x35,0x66,0x57,0xc0,0xf1,0xa2,0x93,0xbd,0x8c,0xdf,0xee,0x79,0x48,0x1b,0x2a,
			0xc1,0xf0,0xa3,0x92,0x05,0x34,0x67,0x56,0x78,0x49,0x1a,0x2b,0xbc,0x8d,0xde,0xef,
			0x82,0xb3,0xe0,0xd1,0x46,0x77,0x24,0x15,0x3b,0x0a,0x59,0x68,0xff,0xce,0x9d,0xac
		};
    }
}
