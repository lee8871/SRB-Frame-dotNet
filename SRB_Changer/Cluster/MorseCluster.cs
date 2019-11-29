using SRB.Frame;

namespace SRB.NodeType.Charger
{
    internal class MorseCluster : ICluster
    {
        internal byte power_on { get => getBankByte(0); set => setBankByte(value, 0); }
        internal byte jack_in_vot_low { get => getBankByte(1); set => setBankByte(value, 1); }
        internal byte jack_in_charge_close { get => getBankByte(2); set => setBankByte(value, 2); }
        internal byte charging { get => getBankByte(3); set => setBankByte(value, 3); }
        internal byte charge_done { get => getBankByte(4); set => setBankByte(value, 4); }
        internal byte change_done_next { get => getBankByte(5); set => setBankByte(value, 5); }
        internal byte jack_remove { get => getBankByte(6); set => setBankByte(value, 6); }
        internal byte low_power { get => getBankByte(7); set => setBankByte(value, 7); }
        public MorseCluster(BaseNode n)
            : base(n, 12, 8)
        {
        }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new MorseCC(this);
        }
        public override string ToString()
        {
            return string.Format("Morse alram<ID={0}>", CID);
        }
    }
}
