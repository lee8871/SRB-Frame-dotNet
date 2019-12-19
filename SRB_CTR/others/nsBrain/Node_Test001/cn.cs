using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using SRB_CTR.nsFrame;
namespace SRB_CTR.nsBrain.Node_Test001
{
    class cn:Node
    {
        public Color color_set = Color.Wheat;
        public Color color_now = new Color();
        public Cluster_led_phase.Clu led_phase_clu;
        public cn(byte addr,SrbFrame f = null):base(addr,f)
        {
            led_phase_clu = new Cluster_led_phase.Clu(10, this);
            clusters[led_phase_clu.clustr_ID] = led_phase_clu;            
            //if (Can_post_access)
            //{
            //    led_phase_clu.read();
            //}
        }
        public void bulidUpD0()
        {
            byte[] data =  { color_set.B, color_set.R, color_set.G };
            this.addAccess(new Access(this, Access.PortEnum.D0, data));
        }
        protected override void d0AccessDone(Access ac)
        {
            try
            {
                if (ac.Status == Access.StatusEnum.RecvedDone)
                {
                    color_now = Color.FromArgb(ac.Recv_data[1], ac.Recv_data[2], ac.Recv_data[0]);
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
