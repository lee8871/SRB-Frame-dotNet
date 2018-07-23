using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SRB_access;
namespace SRB_CTR
{
    public partial class uartDara_web : Form
    {
        public uartDara_web()
        {
            InitializeComponent();
            mainWB.DocumentText = @"<style>
                                        span {font-family:consolas}
	                                    span.note {color:black;}
	                                    span.sno {color:OrangeRed;}
	                                    span.send {color:DarkOrange;}
	                                    span.recv {color:SteelBlue;}
                                    </style>";
        }
        private byte Addr=0x00;
        private void sendPkg(byte addr, byte[] data, byte port)
        {
            string st = " \nsno: " + addr.ToHexSt() + "." + port.ToHexSt();
            st += " send: ";
            mainRT.SelectionColor = Color.Black;
            mainRT.AppendText(st);
            mainRT.SelectionColor = Color.Orange;
            foreach (byte b in data)
            {
                mainRT.AppendText(b.ToHexSt() + " ");
            }
            mainRT.SelectionColor = Color.Black;
            mainRT.AppendText("receive: ");
            Access ac = new SRB_access.Access(addr, data, port);
            this.comport0.writeToCom(ac);
            accesses[ac.sno] = ac;
        }


        private Access[] accesses = new Access[256];
        private Queue<byte> current_bytes = new Queue<byte>();
        private byte current_sno = 0xf8;
        bool Escaping = false;
        private void getNextByte(byte b)
        {
            if (b == 0xf5)
            {
                Escaping = true;
                return;
            }
            else
            {
                if (Escaping == true)
                {
                    if (b == 0xf3)
                    {
                        current_bytes.Enqueue(0xf5);
                    }
                    else
                    {
                        if (current_sno != 0xf8)
                        {
                            if (accesses[current_sno] != null)
                            {
                                accesses[current_sno].fromUartGetBytes(current_bytes.ToArray());
                                mainWB.DocumentText += accesses[current_sno].ToHtml();
                            }
                        }
                        current_bytes.Clear();
                        current_sno = b;
                    }
                    Escaping = false;
                }
                else
                {

                    current_bytes.Enqueue(b);
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            mainRT.SelectionColor = Color.SteelBlue;
            while (this.comport0.qRead.Count != 0)
            {
                byte b = this.comport0.qRead.Dequeue();
               mainRT.AppendText(b.ToHexSt() + " ");
                getNextByte(b);
            }
        }

        private void colorBTN_Click(object sender, EventArgs e)
        {
            
            Color c = ((Button)sender).ForeColor;
            sendPkg(Addr, new byte[] { c.B, c.R, c.G }, 0);
        }
        int h = 180;
        private void LEDChange_Tick(object sender, EventArgs e)
        {
            ColorHSV rc = new ColorHSV(h, 255, 255);
            Color c = ColorHelper.HsvToRgb(rc).GetColor();
            sendPkg(Addr, new byte[] { c.B, c.R, c.G }, 0);
            h++; h %= 360;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LEDChange.Enabled = true;
        }

        private void stopTest_Click(object sender, EventArgs e)
        {
            LEDChange.Enabled = false;
        }

        private void writeintoCluster0BTN_Click(object sender, EventArgs e)
        {
            sendPkg(Addr, new byte[] { 0x00, Addr, 0x01, 0xaa, 0xbb, 0xcc, 0xdd, 0xaa, 0xbb, 0xcc, 0xdd }, 5);
        }

        private void readFromCluster0BTN_Click(object sender, EventArgs e)
        {
            sendPkg(Addr, new byte[] { 0x00 }, 5);
        }
        private void error_Click(object sender, EventArgs e){
            sendPkg(Addr, new byte[] { 0x89 }, 4);
        }

        private void writeInfoBT_Click(object sender, EventArgs e)
        {
            byte addr, addr_change_to;
            addr = (byte)Convert.ToInt32(this.addrTB.Text, 16);
            addr_change_to = (byte)Convert.ToInt32(this.addrChangeToBT.Text, 16);
            if (nameBT.Text.Length > 8)
            {
                nameBT.Text = nameBT.Text.Substring(8);
            }

            char[] name_c = nameBT.Text.ToCharArray();
            byte[] pkg_datas = new byte[name_c.Length + 3];
            int i = 0,j=0;
            pkg_datas[i++] = 0x00;
            pkg_datas[i++] = addr_change_to;
            pkg_datas[i++] = 0x0f;
            while (i < pkg_datas.Length)
            {
                pkg_datas[i++] = (byte)name_c[j++];
            }
            sendPkg(addr, pkg_datas, 5);
        }

        private void readBT_Click(object sender, EventArgs e)
        {
            byte[] pkg_datas = new byte[1];
            pkg_datas[0] = 0x00;
            sendPkg(Addr, pkg_datas, 5);
        }

        private void addrTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Addr = (byte)Convert.ToInt32(this.addrTB.Text, 16);
                this.addrTB.BackColor = Color.White;
            }
            catch
            {
                this.addrTB.BackColor = Color.Salmon;
                //when user input a error address, this catch
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sendPkg(Addr, new byte[] { 200,240,0,}, 0);
        }

        private void OrangeYellowGreenBT_Click(object sender, EventArgs e)
        {
            sendPkg(Addr, new byte[] { 0x00, 0x22, 0xdd, 0x00, 0xaa, 0xaa, 0x00,  0xdd, 0x22, 0x00}, 0);
        }

        private void pinkVioletBlueBT_Click(object sender, EventArgs e)
        {
            sendPkg(Addr, new byte[] { 0x22, 0x22, 0xdd,  0xee, 0x11, 0x66,  0xff, 0xdd, 0x22, 0x00 }, 0);
        }

        private void addrChangeBT_Click(object sender, EventArgs e)
        {
            Button clicked = (Button)sender;
            this.addrTB.Text = clicked.Text;
        }

        private void SynchronizeBTN_Click(object sender, EventArgs e)
        {
            this.comport0.writeToCom(new SRB_access.Access(0xf0));
        }



        byte clusterID = 0x00;
        private void sendClusterBTN_Click(object sender, EventArgs e)
        {
            byte[] datas = this.hexInput.getBytes();
            byte[] pkg = new byte[datas.Length+1];
            pkg[0] = clusterID;
            for (int i = 0; i < datas.Length; i++)
            {
                pkg[i + 1] = datas[i];
            }
            sendPkg(Addr,pkg , 5);
        }


        void clusterTBGotFocus(object sender, System.EventArgs e)
        {
            clusterTB_0.BackColor = Color.Gold;
            clusterTB_1.BackColor = Color.Gold;
            clusterTB_2.BackColor = Color.Gold;
            clusterTB_3.BackColor = Color.Gold;
            clusterTB_4.BackColor = Color.Gold;
            clusterTB_5.BackColor = Color.Gold;
            clusterTB_6.BackColor = Color.Gold;
            clusterTB_7.BackColor = Color.Gold;
            TextBox theTB = (TextBox)sender;
            try
            {
                clusterID = (byte)Convert.ToInt32(theTB.Text, 16);
                theTB.BackColor = Color.Yellow;
            }
            catch
            {
                this.addrTB.BackColor = Color.Salmon;
                //when user input a error address, this catch
            }
        }
    }
}
