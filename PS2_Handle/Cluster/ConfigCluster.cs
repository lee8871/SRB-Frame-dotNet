using SRB.Frame;

namespace SRB.NodeType.PS2_Handle
{
    internal class ConfigCluster : ICluster
    {
        public int online_rumble_10ms { get => getBankByte(0); set => setBankByte((byte)value, 0); }
        public int lose_rumble_10ms { get => getBankByte(1); set => setBankByte((byte)value, 1); }
        public int Strength { get => getBankByte(2); set => setBankByte((byte)value, 2); }


        public ConfigCluster(BaseNode n)
            : base(n, 11, 3)
        {

        }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new ConfigCC(this);
        }
        public override string ToString()
        {
            return string.Format("Ps2Handle Config<ID={0}>", CID.ToHexSt());
        }
    }
}
