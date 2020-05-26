using SRB.Frame;

namespace SRB.NodeType.Charger
{
    internal class MorseCluster : Node.ICluster
    {
        internal byte power_on { get => bank.getBankByte(0); set => bank.setBankByte(value, 0); }
        internal byte jack_in_vot_low { get => bank.getBankByte(1); set => bank.setBankByte(value, 1); }
        internal byte jack_in_charge_close { get => bank.getBankByte(2); set => bank.setBankByte(value, 2); }
        internal byte charging { get => bank.getBankByte(3); set => bank.setBankByte(value, 3); }
        internal byte charge_done { get => bank.getBankByte(4); set => bank.setBankByte(value, 4); }
        internal byte change_done_next { get => bank.getBankByte(5); set => bank.setBankByte(value, 5); }
        internal byte jack_remove { get => bank.getBankByte(6); set => bank.setBankByte(value, 6); }
        internal byte low_power { get => bank.getBankByte(7); set => bank.setBankByte(value, 7); }
        public MorseCluster(Node n)
            : base(n, 12, 8)
        {
        }
        protected override System.Windows.Forms.Control createControl()
        {
            return new MorseCC(this);
        }
        public override string ToString()
        {
            return string.Format("Morse alram<ID={0}>", CID);
        }
    }
}
