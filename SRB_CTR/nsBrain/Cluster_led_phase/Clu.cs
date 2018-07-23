using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRB_CTR.nsBrain.Cluster_led_phase
{
    class Clu:Cluster
    {
        public byte add_per_ms = 100;
        public byte dec_per_ms = 50;
        public ushort low_ms = 0;
        public ushort period = 3000;

        public double fase_sec
        {
            get { return (double)( 25600.0 / dec_per_ms)/1000.0; }
            set { dec_per_ms = (byte)(25600.0 / (1000.0 * value)); }
        }
        public double cycle_sec
        {
            get { return period / 1000.0; }
            set 
            {
                value *= 1000;
                if (value > 60000)
                {
                    value = 60000;
                }
                if(value <10)
                {
                    value = 10;
                }
                period = (ushort)(value+0.5);
            }
        }




        public Clu(byte ID, Node n)
            : base(ID, n) { }
        public override void write()
        {
            byte[] b = new byte[7];
            int i = 0;
            b[i++] = clustr_ID;
            b[i++] = add_per_ms;
            b[i++] = dec_per_ms;
            b[i++] = low_ms.ByteLow();
            b[i++] = low_ms.ByteHigh();
            b[i++] = period.ByteLow();
            b[i++] = period.ByteHigh();
            parent_node.access_queue.Enqueue(new Access(this.parent_node.Addr, Access.ePort.Cgf, b));
        }
        public override void readRecv(Access ac)
        {
            int i = 0;
            add_per_ms = ac.Recv_data[i++];

            period = ac.Recv_data.GetUint16(i);
            base.readRecv(ac);
            return;
        }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new Ctrl(this);
        }
        public override string ToString()
        {
            return string.Format("Led Phase Cluster<ID={0}>", clustr_ID.ToHexSt());
        }
    }
}
