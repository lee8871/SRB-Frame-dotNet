using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB.Frame;

namespace SRB.NodeType.Du_motor
{
    class AdjustCluster:ICluster
    {
        public byte adj { get => getBankByte(0); set => setBankByte(value, 0); }
        public bool motor_a_tog { get => getBankBool(1); set => setBankBool(value, 1); }	
	   public bool motor_b_tog { get => getBankBool(2); set => setBankBool(value, 2); }

        public AdjustCluster(byte ID, Node n)
            : base(n, ID, 3) { }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new AdjustCC(this);
        }
        public override string ToString()
        {
            return string.Format("Adjest<ID={0}>", Clustr_ID.ToHexSt());
        }
    }
}
