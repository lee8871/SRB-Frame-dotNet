using SRB.Frame;

namespace SRB.NodeType.SpeedMotor
{
    internal class ConfigCluster : Node.ICluster
    {
        public ushort min_pwm_a { get => bank.getBankUshort(0); set => bank.setBankUshort(value, 0); }
        public ushort min_pwm_b { get => bank.getBankUshort(2); set => bank.setBankUshort(value, 2); }
        public ushort period { get => bank.getBankUshort(4); set => bank.setBankUshort(value, 4); }
        public byte lose_control_ms { get => bank.getBankByte(6); set => bank.setBankByte(value, 6); }
        public byte lose_behavior { get => bank.getBankByte(7); set => bank.setBankByte(value, 7); }

        public ConfigCluster(Node n)
            : base(n, 10, 8)
        {
        }
        protected override System.Windows.Forms.Control createControl()
        {
            return new ConfigCC(this);
        }
        public override string ToString()
        {
            return string.Format("Motor config<ID={0}>", CID.ToHexSt());
        }
    }
}
