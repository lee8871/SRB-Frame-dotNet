using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SRB.Frame;

namespace SRB.NodeType.Charger
{
    class BatteryCluster:ICluster
    {
        internal int Low_voltage { get => getBankUshort(0); set => setBankUshort((ushort)value, 0); }
        internal int High_voltage { get => getBankUshort(2); set => setBankUshort((ushort)value, 2); }
        internal bool power_on_enable_charge { get => getBankBool(4, 0); set => setBankBool(value, 4, 0); }
        internal bool power_on_mute { get => getBankBool(4, 1); set => setBankBool(value, 4, 1); }
        internal bool power_on_led_enable { get => getBankBool(4, 2); set => setBankBool(value, 4, 2); }

        internal BatteryCluster(Frame.BaseNode n)
            : base(n, 11, 5)
        {
            
        }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new BatteryCC(this);
        }
        public override string ToString()
        {
            return string.Format("Battery Config<ID={0}>", Clustr_ID.ToHexSt());
        }
    }
}
