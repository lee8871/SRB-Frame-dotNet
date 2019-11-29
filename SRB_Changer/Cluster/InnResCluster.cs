using SRB.Frame;

namespace SRB.NodeType.Charger
{
    internal class InnResCluster : ICluster
    {
        internal int mOhm(int num)
        {
            return (short)getBankUshort(num * 2);
        }

        internal InnResCluster(Frame.BaseNode n)
            : base(n, 13, 5)
        {

        }
        public override System.Windows.Forms.UserControl createControl()
        {
            return new InnResCC(this);
        }
        public override string ToString()
        {
            return string.Format("Internal resistance get<ID={0}>", CID);
        }
    }
}
