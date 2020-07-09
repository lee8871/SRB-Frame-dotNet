using SRB.Frame;
using System;
using System.Windows.Forms;

namespace SRB.NodeType.SpeedMotor
{
    internal class TestPwmCluster : Node.ICluster
    {
        public byte Direction { get => bank.getBankByte(0); set => bank.setBankByte(value, 0); }
        public ushort Pwm { get => bank.getBankUshort(1); set => bank.setBankUshort(value, 1); }

        public TestPwmCluster(Node n)
            : base(n, 12, 3) 
        {
            is_follower = false;
        }
        protected override Control createControl()
        {
            throw new NotImplementedException("Cluster do not have a Control");
        }
        public override string ToString()
        {
            return string.Format("PWM Test<ID={0}>", CID);
        }
        public override void readRecv(Access ac)
        {
            base.readRecv(ac);
        }
    }
}
