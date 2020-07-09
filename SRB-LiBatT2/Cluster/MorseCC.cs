
using SRB.Frame;

namespace SRB.NodeType.Charger
{
    internal partial class MorseCC : IClusterControl
    {
        private MorseCluster cluster;
        public MorseCC(MorseCluster c) : base(c)
        {
            InitializeComponent();
            cluster = c;
            cluster.readAll();
        }


        protected override void DataUpdata()
        {
            PowerOnMB.Morse_code = cluster.power_on;
            JackInLowMB.Morse_code = cluster.jack_in_vot_low;
            JackInDisableMB.Morse_code = cluster.jack_in_charge_close;
            ChargingMB.Morse_code = cluster.charging;

            ChargeDoneMB.Morse_code = cluster.charge_done;
            ChargeDoneSecondMB.Morse_code = cluster.change_done_next;
            JackRemoveMB.Morse_code = cluster.jack_remove;
            LowPowerMB.Morse_code = cluster.low_power;
        }

        protected override void WriteData()
        {
            cluster.writeBankinit();
            cluster.power_on = PowerOnMB.Morse_code;
            cluster.jack_in_vot_low = JackInLowMB.Morse_code;
            cluster.jack_in_charge_close = JackInDisableMB.Morse_code;
            cluster.charging = ChargingMB.Morse_code;

            cluster.charge_done = ChargeDoneMB.Morse_code;
            cluster.change_done_next = ChargeDoneSecondMB.Morse_code;
            cluster.jack_remove = JackRemoveMB.Morse_code;
            cluster.low_power = LowPowerMB.Morse_code;
            cluster.write();
        }

    }
}
