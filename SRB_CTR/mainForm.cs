using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SRB_CTR.nsBrain;
using System.Diagnostics;

namespace SRB_CTR
{
	public partial class mainForm : Form
	{
		public mainForm()
        {
            InitializeComponent();
        }
        public void testComPort()
        {
            int state;
            long[] delay = new long[1000];
            long tickbase; 
            int tick_counter = 0;
            ComPort com1;            
            com1 = new ComPort();

            com1.BaudRate = 115200;
            com1.Port = "com5";
            com1.ReadTimeout = 1;
            com1.Open();

            long tick0,tick1;
            tick0 = Stopwatch.GetTimestamp() + 100 * 1000;

            byte[] recv_bytes = new byte[100];
            byte[] send_byte = new byte[100];
            for (int i = 0; i < 100; i++)
            {
                send_byte[i] = (byte)(i + 100);
            }
            for (int i = 0; i < 1000; i++)
            {
                tickbase = Stopwatch.GetTimestamp();
                send_byte[0] = (byte)i;
                com1.Write(ref send_byte, 100);
                state = com1.Read(ref recv_bytes, 100);
                delay[tick_counter] = Stopwatch.GetTimestamp() - tickbase;
                tick_counter++;
                do
                {
                    tick1 = Stopwatch.GetTimestamp();
                }
                while (tick1 < (tick0));
                tick0 = tick1 + 100 * 1000;
            }
		}
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
	}
}
