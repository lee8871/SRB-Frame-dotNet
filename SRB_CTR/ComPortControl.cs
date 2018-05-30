using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using pkgs;
namespace lemonReceiver.ToNode
{
    public partial class ComPortControl : UserControl
    {
        private SerialPort mainSP;
        private System.Windows.Forms.Timer mainT;
        public event EventHandler ePortClose;
        public event EventHandler ePortOpen;
        //public event EventHandler ePortNumChange;
        public delegate void dAccessDone(Access pkg);


        private bool isOpend = false;
        public bool IsOpend { get { return isOpend; } }


        new public bool Enabled
        {
            get { return comSelectCB.Enabled; }
            set { comSelectCB.Enabled = value; }
        }
        public ComPortControl()
        {
            InitializeComponent();
            comSelectCB.Click += new EventHandler(comSelectCB_Click);
            comSelectCB.BackColor = Color.LightPink;
            getUartTable();
        }



        private void comStateOpen()
        {
            if (isOpend == false)
            {
                isOpend = true;
                if (ePortOpen != null)
                {
                    ePortOpen.Invoke(this, null);
                }
                comSelectCB.BackColor = Color.LightGreen;
            }
        }
        private void comStateClose()
        {
            if (isOpend == true)
            {
                isOpend = false;
                if (ePortClose != null)
                {
                    ePortClose.Invoke(this, null);
                }
                comSelectCB.BackColor = Color.LightPink;
            }
        }


        void comSelectCB_Click(object sender, EventArgs e)
        {
            getUartTable();
        }
        private void mainCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            openPort();
        }


        private string[] comNumTable;
        private int testComNumConter;
        public bool getUartTable()
        {
            comSelectCB.Items.Clear();
            comNumTable = System.IO.Ports.SerialPort.GetPortNames();
            if (comNumTable.Length != 0)
            {
                foreach (string s in comNumTable)
                {
                    comSelectCB.Items.Insert(comSelectCB.Items.Count, s);
                }
                testComNumConter = 0;
                comSelectCB.Text = comNumTable[testComNumConter];
                testComNumConter++;
                return true;
            }
            else
            {
                return false;
            }
        }
        delegate bool nextComPortCB();
        public bool nextComPort()
        {
            if (testComNumConter < comNumTable.Length)
            {
                if (this.comSelectCB.InvokeRequired)
                {
                    nextComPortCB d = new nextComPortCB(nextComPort);
                    this.Invoke(d, new object[] { });
                }
                else
                {
                    comSelectCB.Text = comNumTable[testComNumConter];
                    testComNumConter++;
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        public void openPort()
        {
            if (mainSP != null)
            {
                try { closePort(); }
                catch { }
            }
            mainSP = new SerialPort(comSelectCB.Text, 115200);
            mainSP.BaudRate = 115200;
            mainSP.DataBits = 8;
            mainSP.DtrEnable = false;
            mainSP.Parity = Parity.None;
            mainSP.RtsEnable = false;
            mainSP.StopBits = StopBits.One;
            try
            {
                mainSP.Open();
            }
            catch( Exception e)
            {
                CMDSpeed.Text = e.Message;
            }

            if (mainSP.IsOpen == true)
            {
                comStateOpen();
                CMDSpeed.Text = "开启成功";
                mainT = new System.Windows.Forms.Timer();
                mainT.Interval = 100;
                mainT.Tick += new EventHandler(mainT_Tick);
                mainT.Start();
            }
            else
            {
                comStateClose();
            }
        }
        public Queue<byte> qRead = new Queue<byte>();
        void mainT_Tick(object sender, EventArgs e)
        {
            try
            {
                while (mainSP.BytesToRead != 0)
                {
                    qRead.Enqueue((byte)mainSP.ReadByte());
                }
            }
            catch (Exception exc)
            {
                CMDSpeed.Text = exc.Message; 
                closePort();
            }
        }
        private bool closePort()
        {
            mainT.Stop();
            if (mainSP != null)
            {
                while (mainSP.IsOpen != false)
                {
                    int times = 0;
                    try
                    {
                        mainSP.Close();
                    }
                    catch
                    {
                        times++;
                        if (times >= 100)
                        {
                            CMDSpeed.Text = "串口无法关闭";
                            throw new Exception("串口关不上了");
                        }
                    }
                }
                mainSP.Dispose();
            }
            comStateClose();
            return false;
        }

        public delegate void dWriteToCom(byte[] data);
        public void writeToCom(Access a0)
        {
            if (mainSP.IsOpen == true)
            {
                mainSP.BaseStream.WriteByte(0xF5);
                mainSP.BaseStream.WriteByte(a0.addr);
                mainSP.BaseStream.WriteByte(a0.bfc);
                if (a0.bfc == 0xF5)
                {
                    mainSP.BaseStream.WriteByte(0xf3);
                }
                foreach (byte b in a0.data)
                {
                    mainSP.BaseStream.WriteByte(b);
                    if (b == 0xF5)
                    {
                        mainSP.BaseStream.WriteByte(0xf3);
                    }
                }
                mainSP.BaseStream.WriteByte(a0.crc);
                if (a0.crc == 0xF5)
                {
                    mainSP.BaseStream.WriteByte(0xf3);
                }
            }
        }

        private void ComPortControl_Load(object sender, EventArgs e)
        {

        }
    }
}
