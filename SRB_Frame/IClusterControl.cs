using System;
using System.Windows.Forms;

namespace SRB.Frame
{
    public partial class IClusterControl : UserControl
    {
        private Node.ICluster cluster;
        private bool enable_write = true;

        public bool Enable_write { get => enable_write; set => OnEnable_write(value); }

        private void OnEnable_write(bool value)
        {
            this.writeBTN.Visible = enable_write = value;
        }
        public IClusterControl(Node.ICluster c = null)
        {
            InitializeComponent();
            this.writeBTN.Visible = enable_write;
            cluster = c;
            c.eDataChanged += new EventHandler(c_dataChanged);
        }
        public IClusterControl()
        {
            InitializeComponent();
            this.writeBTN.Visible = enable_write;
        }
        protected virtual void DataUpdata()
        {
            throw new Exception("必须实现数据更新方法");
        }
        protected virtual void WriteData()
        {
            throw new Exception("写数据方法被调用,但是没有实现此方法");
        }


        protected virtual void c_dataChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                EventHandler d = new EventHandler(c_dataChanged);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                DataUpdata();
            }
        }
        protected virtual void OnWriteClick(object sender, EventArgs e)
        {
            if (Enable_write)
            {
                WriteData();
            }
            else
            {
                throw new Exception("readonly node can not write!");
            }
        }
        protected virtual void OnReadClick(object sender, EventArgs e)
        {
            cluster.readAll();
        }
    }
}
