using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB_CTR.SRB_Frame;

namespace SRB_CTR.nsBrain.Node_PS2_handle.Cluster_handle_cfg
{
    class Clu:Cluster
    {
        public int period { get => getBankByte(1); set => setBankByte((byte)value, 1); }
        internal bool analog { get => getBankBool(0,0); set => setBankBool(value, 0,0); }
        internal bool rumble { get => getBankBool(0,1); set => setBankBool(value, 0,1); }

        public Clu(byte ID, Node n)
            : base(ID, n,2)
        {
            
        }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new Ctrl(this);
        }
        public override string ToString()
        {
            return string.Format("Ps2Handle Config<ID={0}>", Clustr_ID.ToHexSt());
        }
    }
}
