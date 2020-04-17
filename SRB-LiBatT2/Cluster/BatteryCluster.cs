using SRB.Frame;

namespace SRB.NodeType.Charger
{
    internal class BatteryCluster : BaseNode.ICluster
    {
        internal int Low_voltage { get => bank.getBankUshort(0); set => bank.setBankUshort((ushort)value, 0); }
        internal int Max_charge_current { get => bank.getBankUshort(2); set => bank.setBankUshort((ushort)value, 2); }
        internal int Capacity_mAh { get => bank.getBankUshort(4); set => bank.setBankUshort((ushort)value, 4); }
        internal int inn_res_mOhm { get => bank.getBankUshort(6); set => bank.setBankUshort((ushort)value, 6); }
        internal bool power_on_enable_charge { get => bank.getBankBool(8, 0); set => bank.setBankBool(value, 8, 0); }
        internal bool power_on_mute { get => bank.getBankBool(8, 1); set => bank.setBankBool(value, 8, 1); }
        internal bool power_on_led_enable { get => bank.getBankBool(8, 2); set => bank.setBankBool(value, 8, 2); }

        internal BatteryCluster(Frame.BaseNode n)
            : base(n, 11, 9)
        {

        }
        protected override System.Windows.Forms.Control createControl()
        {
            return new BatteryCC(this);
        }
        public override string ToString()
        {
            return string.Format("Battery Config<ID={0}>", CID);
        }
    }
}
