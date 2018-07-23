using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SRB_CTR.nsBrain.Node_dMotor
{
    class cn:Node
    {
        public int speed_a = 0;
        public int speed_b = 0;
        //public Cluster_led_phase.Clu led_phase_clu;
        public cn(byte addr):base(addr)
        {
            //led_phase_clu = new Cluster_led_phase.Clu(10, this);
            //clusters[led_phase_clu.clustr_ID] = led_phase_clu;
            //led_phase_clu.read();
        }
        public override Access[] bulidUp()
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
            this.access_queue.Enqueue(new Access(this.Addr, Access.ePort.D0, data));
            return base.bulidUp();
        }
        protected override void d0AccessDone(Access ac)
        {
            try
            {
                if (ac.status == Access.Status.RecvedDone)
                {
                 // color_now = Color.FromArgb(ac.Recv_data[1], ac.Recv_data[2], ac.Recv_data[0]);
                }
            }
            catch (System.IndexOutOfRangeException)
            { }
        }
        internal override System.Windows.Forms.Control  getClusterControl()
        {
            return new Ctrl(this);
        }
    }
}
