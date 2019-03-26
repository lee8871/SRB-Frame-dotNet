using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SRB.Frame;
namespace SRB.NodeType.Du_motor
{
    public class Cn : BaseNode
    {
        internal ConfigCluster motor_clu;
        internal AdjustCluster adj_clu;

        public int Speed_a { set => setSpeedA(value); }
        public int Speed_b { set => setSpeedB(value); }

        public int Brake_a { set => setBrakeA(value); }
        public int Brake_b { set => setBrakeB(value); }

        private void setBrakeA(int value)
        {
            int temp;
            temp = value;
            temp |= (1 << 15);
            bank[0] = (byte)temp;
            bank[1] = (byte)(temp >> 8);
        }

        private void setBrakeB(int value)
        {
            int temp;
            temp = value;
            temp |= (1 << 15);
            bank[2] = (byte)temp;
            bank[3] = (byte)(temp >> 8);
        }

        public void setSpeedA(int speed)
        {
            int temp;
            temp = speed;
            temp &= ~(1 << 15);
            bank[0] = (byte)temp;
            bank[1] = (byte)(temp >> 8);
        }
        public void setSpeedB(int speed)
        {
            int temp;
            temp = speed;
            temp &= ~(1 << 15);
            bank[2] = (byte)temp;
            bank[3] = (byte)(temp >> 8);
        }
        public void init()
        {
            motor_clu = new ConfigCluster(10, this);
            clusters[motor_clu.Clustr_ID] = motor_clu;
            adj_clu = new AdjustCluster(11, this);
            clusters[adj_clu.Clustr_ID] = adj_clu;
            //led_phase_clu.read();
            bankInit(new byte[][]{
                new byte[]{ 0,4,0,1,2,3}                        ,
                new byte[]{ 0,4,2,3,0,1}                        ,
                new byte[]{ 0,4,0,1,2,3}                        ,
                new byte[]{ 0,4,2,3,0,1}
            });
        }
        public Cn(byte addr, ISRB_Master f = null)
            : base(addr, f)
        {
            init();
        }

        public Cn(BaseNode n)
            : base(n)
        {
            init();
        }
        public void bulidUpD0()
        {
            this.addAccess(0);
            // byte[] data = { (byte)temp_a, (byte)(temp_a >> 8), (byte)temp_b, (byte)(temp_b >> 8) };
            // this.addAccess(new Access(this, Access.PortEnum.D0, data));
        }

        public override System.Windows.Forms.Control  getClusterControl()
        {
            return new Ctrl(this);
        }
        public override string Describe()
        {
            return @"This node drivers two motors. Without speed or force sensor";
        }
    }
}
