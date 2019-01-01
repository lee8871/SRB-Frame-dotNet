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
        public Cluster_Du_Motor_v02.Clu motor_clu;
        public Cluster_du_motor_adj.Clu adj_clu;

        public int Speed_a {set => setSpeedA(value); }
        public int Speed_b {set => setSpeedB(value); }

        public void setSpeedA(int speed)
        {
            int temp;
            if (speed >= 0)
            {
                temp = speed;
            }
            else
            {
                temp = (-speed) | 0xc000;
            }
            bank[0] = (byte)temp;
            bank[1] = (byte)(temp >> 8);
        }
        public void setSpeedB(int speed)
        {
            int temp;
            if (speed >= 0)
            {
                temp = speed;
            }
            else
            {
                temp = (-speed) | 0xc000;
            }
            bank[2] = (byte)temp;
            bank[3] = (byte)(temp >> 8);
        }

        public Cn(byte addr, SrbFrame f = null)
            : base(addr, f)
        {
            motor_clu = new Cluster_Du_Motor_v02.Clu(10, this);
            clusters[motor_clu.clustr_ID] = motor_clu;
            adj_clu = new Cluster_du_motor_adj.Clu(11, this);
            clusters[adj_clu.clustr_ID] = adj_clu;
            //led_phase_clu.read();
            bankInit(new byte[][]{
                new byte[]{ 0,4,0,1,2,3}                        ,
                new byte[]{ 0,4,2,3,0,1}                        ,
                new byte[]{ 0,4,0,1,2,3}                        ,
                new byte[]{ 0,4,2,3,0,1}
            });
        }


        public Cn(Node n)
            : base(n)
        {
            motor_clu = new Cluster_Du_Motor_v02.Clu(10, this);
            clusters[motor_clu.clustr_ID] = motor_clu;
            adj_clu = new Cluster_du_motor_adj.Clu(11, this);
            clusters[adj_clu.clustr_ID] = adj_clu;
            //led_phase_clu.read();           
            bankInit(new byte[][]{
            new byte[] { 0, 4, 0, 1, 2, 3 }                        ,
                new byte[] { 0, 4, 2, 3, 0, 1 }                        ,
                new byte[] { 0, 4, 0, 1, 2, 3 }                        ,
                new byte[] { 0, 4, 2, 3, 0, 1 }
            });
        }
        public void bulidUpD0()
        {
            this.addAccess(0);
            // byte[] data = { (byte)temp_a, (byte)(temp_a >> 8), (byte)temp_b, (byte)(temp_b >> 8) };
            // this.addAccess(new Access(this, Access.PortEnum.D0, data));
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
