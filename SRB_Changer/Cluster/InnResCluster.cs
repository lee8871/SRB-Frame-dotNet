using SRB.Frame;

namespace SRB.NodeType.Charger
{
    internal class InnResCluster : BaseNode.ICluster
    {
        internal int mOhm(int num)
        {
            return (short)bank.getBankUshort(num * 2);
        }

        internal InnResCluster(Frame.BaseNode n)
            : base(n, 13, 30)
        {

        }
        protected override System.Windows.Forms.Control createControl()
        {
            return new InnResCC(this);
        }
        public override string ToString()
        {
            return string.Format("Internal resistance get<ID={0}>", CID);
        }
    }
}
