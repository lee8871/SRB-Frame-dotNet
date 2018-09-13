using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using System.IO;
using LibUsbDotNet;
using LibUsbDotNet.LibUsb;
using LibUsbDotNet.Main;
using LibUsbDotNet.LudnMonoLibUsb;
using EC = LibUsbDotNet.Main.ErrorCode;


namespace testUSB
{
    public partial class TestUSBForm : Form
    {
        private UsbDevice mUsbDevice;
        private UsbEndpointReader mEpReader;
        private UsbEndpointWriter mEpWriter;
        private string mLogFileName = String.Empty;
        private FileStream mLogFileStream;
        private UsbRegDeviceList mRegDevices;
        public TestUSBForm()
        {
            InitializeComponent();
        } 
       
            }
}
