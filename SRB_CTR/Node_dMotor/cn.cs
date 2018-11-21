using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SRB_CTR.SRB_Frame;
namespace SRB_CTR.nsBrain.Node_dMotor
{
    class Cn : Node
    {
        public int speed_a = 0;
        public int speed_b = 0;
        public Cluster_Du_Motor_v02.Clu motor_clu;
        public Cluster_du_motor_adj.Clu adj_clu;
        public Cn(byte addr, SrbFrame f = null)
            : base(addr, f)
        {
            motor_clu = new Cluster_Du_Motor_v02.Clu(10, this);
            clusters[motor_clu.clustr_ID] = motor_clu;
            adj_clu = new Cluster_du_motor_adj.Clu(11, this);
            clusters[adj_clu.clustr_ID] = adj_clu;
            //led_phase_clu.read();
        }

        public Cn(Node n)
            : base(n)
        {
            motor_clu = new Cluster_Du_Motor_v02.Clu(10, this);
            clusters[motor_clu.clustr_ID] = motor_clu;
            adj_clu = new Cluster_du_motor_adj.Clu(11, this);
            clusters[adj_clu.clustr_ID] = adj_clu;
            //led_phase_clu.read();
        }
        public void bulidUpD0()
        {
            int temp_a,temp_b;
            if (speed_a >= 0)
            {
                temp_a = speed_a;
            }
            else
            {
                temp_a = (-speed_a) | 0xc000;
            }
            if (speed_b >= 0)
            {
                temp_b = speed_b;
            }
            else
            {
                temp_b = (-speed_b) | 0xc000;
            }
            byte[] data = { (byte)temp_a, (byte)(temp_a >> 8), (byte)temp_b, (byte)(temp_b >> 8) };
            this.addAccess(new Access(this, Access.PortEnum.D0, data));
        }
        protected override void d0AccessDone(Access ac)
        {
            try
            {
                if (ac.Status == Access.StatusEnum.RecvedDone)
                {
                 // color_now = Color.FromArgb(ac._recv_data[1], ac._recv_data[2], ac._recv_data[0]);
                }
            }
            catch (System.IndexOutOfRangeException)
            { }
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
