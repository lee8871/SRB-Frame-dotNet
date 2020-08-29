using SRB.Frame;

namespace SRB.NodeType.SpeedMotorF
{
    internal class PidCluster : Node.ICluster
    {
        public ushort k1 { get => bank.getBankUshort(0); set => bank.setBankUshort(value, 0); }
        public ushort k0 { get => bank.getBankUshort(2); set => bank.setBankUshort(value, 2); }
        public double kp { get => (bank.getBankShort(4) / 65536.0); set => bank.setBankShort((short)(value * 65536), 4); }
        public double ki { get => (bank.getBankShort(6) / 65536.0); set => bank.setBankShort((short)(value * 65536), 6); }
        public double kd { get => (bank.getBankShort(8) / 65536.0); set => bank.setBankShort((short)(value * 65536), 8); }
        public uint Speed_dividend { get => bank.getBankUint(10) ; set => bank.setBankUint(value,10); }

        public PidCluster(Node n)
            : base(n, 11, 14) { }
        protected override System.Windows.Forms.Control createControl()
        {
            return new PidCC(this);
        }
        public override string ToString()
        {
            return string.Format("PID parameter<ID={0}>", CID);
        }
        public override void readRecv(Access ac)
        {
            base.readRecv(ac);
        }
    }
}
