using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB_CTR.SRB_Frame;

namespace SRB_CTR.nsBrain.Node_dMotor.Cluster_du_motor_adj
{
    class Clu:Cluster
    {
       public bool motor_a_tog = false;	
	   public bool motor_b_tog = false;
	   public byte adj = 0;

        public Clu(byte ID, Node n)
            : base(ID, n) { }
        public override void write()
        {
            byte[] b = new byte[4];
            int i = 0;
            b[i++] = clustr_ID;
            b[i++] = adj;
            if(motor_a_tog)
            {
                b[i++] = 1;
            }
            else
            {
                b[i++] = 0;
            }
            if (motor_b_tog)
            {
                b[i++] = 1;
            }
            else
            {
                b[i++] = 0;
            }
            parent_node.singleAccess(new Access(this.parent_node, Access.PortEnum.Cgf, b));
        }
        public override void readRecv(Access ac)
        {
            int i = 0;
            adj = ac.Recv_data[i];
            i++;
            motor_a_tog = (ac.Recv_data[i] != 0);
            i++;
            motor_b_tog = (ac.Recv_data[i] != 0);
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
