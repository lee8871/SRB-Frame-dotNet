﻿using SRB.Frame;

namespace SRB.NodeType.Joystick
{
    internal class ConfigCluster : Node.ICluster
    {
        public int online_rumble_10ms { get => bank.getBankByte(0); set => bank.setBankByte((byte)value, 0); }
        public int lose_rumble_10ms { get => bank.getBankByte(1); set => bank.setBankByte((byte)value, 1); }
        public int Strength { get => bank.getBankByte(2); set => bank.setBankByte((byte)value, 2); }


        public ConfigCluster(Node n)
            : base(n, 11, 3)
        {

        }
        protected override System.Windows.Forms.Control createControl()
        {
            return new ConfigCC(this);
        }
        public override string ToString()
        {
            return string.Format("Ps2Handle Config<ID={0}>", CID.ToHexSt());
        }
    }
}
