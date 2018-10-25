using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB_CTR.SRB_Frame;

namespace SRB_CTR.nsBrain.Node_dMotor.Cluster_Du_Motor_v02
{
    class Clu:Cluster
    {
       public ushort  min_pwm_a = 100;	
	   public ushort  min_pwm_b = 100;
	   public ushort  period = 1000;
       public ushort lose_control_ms = 200;
       public byte lose_behavior = 0;

        public Clu(byte ID, Node n)
            : base(ID, n) { }
        public override void write()
        {
            byte[] b = new byte[10];
            int i = 0;
            b[i++] = clustr_ID;
            b[i++] = min_pwm_a.ByteLow();
            b[i++] = min_pwm_a.ByteHigh();
            b[i++] = min_pwm_b.ByteLow();
            b[i++] = min_pwm_b.ByteHigh();
            b[i++] = period.ByteLow();
            b[i++] = period.ByteHigh();
            b[i++] = lose_control_ms.ByteLow();
            b[i++] = lose_control_ms.ByteHigh();
            b[i++] = lose_behavior;
            parent_node.singleAccess(new Access(this.parent_node, Access.PortEnum.Cgf, b));
        }
        public override void readRecv(Access ac)
        {
            int i = 0;
            min_pwm_a = ac.Recv_data.GetUint16(i);
            i += 2;
            min_pwm_b = ac.Recv_data.GetUint16(i);
            i += 2;
            period = ac.Recv_data.GetUint16(i);
            i += 2;
            lose_control_ms = ac.Recv_data.GetUint16(i);
            i += 2;
            lose_behavior = ac.Recv_data[i];
            i++;
            base.readRecv(ac);
            return;
        }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new Ctrl(this);
        }
        public override string ToString()
        {
            return string.Format("Du Motor<ID={0}>", clustr_ID.ToHexSt());
        }
    }
}
