using SRB.Frame;

namespace SRB.NodeType.Charger
{
    internal class BatteryCluster : ICluster
    {
        internal int Low_voltage { get => getBankUshort(0); set => setBankUshort((ushort)value, 0); }
        internal int Max_charge_current { get => getBankUshort(2); set => setBankUshort((ushort)value, 2); }
        internal int Capacity_mAh { get => getBankUshort(4); set => setBankUshort((ushort)value, 4); }
        internal int inn_res_mOhm { get => getBankUshort(6); set => setBankUshort((ushort)value, 6); }
        internal bool power_on_enable_charge { get => getBankBool(8, 0); set => setBankBool(value, 8, 0); }
        internal bool power_on_mute { get => getBankBool(8, 1); set => setBankBool(value, 8, 1); }
        internal bool power_on_led_enable { get => getBankBool(8, 2); set => setBankBool(value, 8, 2); }

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
            return string.Format("Battery Config<ID={0}>", CID);
        }
    }
}
