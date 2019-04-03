using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB.Frame;

namespace SRB.NodeType.PS2_Handle
{
    class ConfigCluster:ICluster
    {
        public int period { get => getBankByte(1); set => setBankByte((byte)value, 1); }
        internal bool analog { get => getBankBool(0,0); set => setBankBool(value, 0,0); }
        internal bool rumble { get => getBankBool(0,1); set => setBankBool(value, 0,1); }

        public ConfigCluster(BaseNode n)
            : base(n, 11, 2)
        {
            
        }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new ConfigCC(this);
        }
        public override string ToString()
        {
            return string.Format("Ps2Handle Config<ID={0}>", Clustr_ID.ToHexSt());
        }
    }
}
