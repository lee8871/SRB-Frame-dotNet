namespace SRB.Frame
{
    public abstract class IBus
    {
        public abstract bool Is_opened { get; }
        public abstract bool doAccess(Access[] acs, int n = -1);
        public abstract bool doAccess(Access acs);
        public virtual System.Windows.Forms.Control getConfigControl()
        {
            return null;
        }
        public virtual void checkPort()
        {

        }

    }
}

