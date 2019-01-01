using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SRB_CTR.SRB_Frame;
namespace SRB_CTR.nsBrain.Node_PS2_handle
{
    class Cn : Node
    {
        public int rumble = 0;
        public int joy_rx = 0;
        public int joy_ry = 0;
        public int joy_lx = 0;
        public int joy_ly = 0;
        public bool L1, L2, L3, R1, R2, R3;
        public bool up, down, right, left;

        public bool select,start;
        public bool trag, circle, cross, square;





        public Cluster_du_motor_adj.Clu adj_clu;
        public Cn(byte addr, SrbFrame f = null)
            : base(addr, f)
        {
            adj_clu = new Cluster_du_motor_adj.Clu(11, this);
            clusters[adj_clu.clustr_ID] = adj_clu;
            //led_phase_clu.read();
        }

        public Cn(Node n)
            : base(n)
        {
            adj_clu = new Cluster_du_motor_adj.Clu(11, this);
            clusters[adj_clu.clustr_ID] = adj_clu;
            //led_phase_clu.read();
        }
        public void bulidUpD0()
        {
            byte[] data = {};
            this.addAccess(new Access(this, Access.PortEnum.D0, data));
        }
        public void bulidUpD0(ushort rumble_ms)
        {
            rumble = rumble.enterRound(0, 255);
            byte[] data = { rumble_ms.ByteLow(), rumble_ms.ByteHigh(), (byte)rumble };
            this.addAccess(new Access(this, Access.PortEnum.D0, data));
        }
        protected override void dataAccessDone(Access ac)
        {
            if (ac.Port == Access.PortEnum.D0)
            {
                try
                {
                    if (ac.Status == Access.StatusEnum.RecvedDone)
                    {
                        // color_now = Color.FromArgb(ac._recv_data[1], ac._recv_data[2], ac._recv_data[0]);
                        this.joy_rx = ac.Recv_data[2];
                        this.joy_ry = ac.Recv_data[3];
                        this.joy_lx = ac.Recv_data[4];
                        this.joy_ly = ac.Recv_data[5];
                        this.select = (ac.Recv_data[0] & (1 << 0)) == 0;
                        this.L3 = (ac.Recv_data[0] & (1 << 1)) == 0;
                        this.R3 = (ac.Recv_data[0] & (1 << 2)) == 0;
                        this.start = (ac.Recv_data[0] & (1 << 3)) == 0;

                        this.up = (ac.Recv_data[0] & (1 << 4)) == 0;
                        this.right = (ac.Recv_data[0] & (1 << 5)) == 0;
                        this.down = (ac.Recv_data[0] & (1 << 6)) == 0;
                        this.left = (ac.Recv_data[0] & (1 << 7)) == 0;

                        this.L2 = (ac.Recv_data[1] & (1 << 0)) == 0;
                        this.R2 = (ac.Recv_data[1] & (1 << 1)) == 0;
                        this.L1 = (ac.Recv_data[1] & (1 << 2)) == 0;
                        this.R1 = (ac.Recv_data[1] & (1 << 3)) == 0;

                        this.trag = (ac.Recv_data[1] & (1 << 4)) == 0;
                        this.circle = (ac.Recv_data[1] & (1 << 5)) == 0;
                        this.cross = (ac.Recv_data[1] & (1 << 6)) == 0;
                        this.square = (ac.Recv_data[1] & (1 << 7)) == 0;

                    }
                }
                catch (System.IndexOutOfRangeException)
                { }
            }
        }
        internal override System.Windows.Forms.Control  getClusterControl()
        {
            return new Ctrl(this);
        }
        public override string Describe()
        {
            return @"This node drivers two motors. Without speed or force sensor";
        }
    }
}
