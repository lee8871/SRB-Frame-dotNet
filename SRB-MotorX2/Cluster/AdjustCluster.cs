using SRB.Frame;

namespace SRB.NodeType.Du_motor
{
    internal class AdjustCluster : Node.ICluster
    {
        public byte adj { get => bank.getBankByte(0); set => bank.setBankByte(value, 0); }
        public bool motor_a_tog { get => bank.getBankBool(1); set => bank.setBankBool(value, 1); }
        public bool motor_b_tog { get => bank.getBankBool(2); set => bank.setBankBool(value, 2); }

        public AdjustCluster(Node n)
            : base(n, 11, 3) { }
        protected override System.Windows.Forms.Control createControl()
        {
            return new AdjustCC(this);
        }
        public override string ToString()
        {
            return string.Format("Adjest<ID={0}>", CID.ToHexSt());
        }
    }
}
