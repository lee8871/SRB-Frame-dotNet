using System;

namespace SRB.Frame
{
    [Serializable]
    public class SrbException : Exception
    {
        protected SrbException(string message) : base(message)
        {
        }
        protected SrbException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected SrbException()
        {
        }
        protected SrbException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new NotImplementedException();
        }
    }


    [System.Serializable]
    public class NodeNotContainException : SrbException
    {
        IBus bus;
        Node node;
        public NodeNotContainException(IBus bus, Node node, string message = "no set msg") : base(message)
        {
            this.bus = bus;
            this.node = node;
        }
        public override string ToString()
        {
            return string.Format("{0} is not node for {1}. {2}", node, bus, base.ToString());
        }
        protected NodeNotContainException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new System.NotImplementedException();
        }
    }

    [System.Serializable]
    public class PerformedException : SrbException
    {
        public PerformedException(string message) : base(message)
        {

        }
        protected PerformedException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new System.NotImplementedException();
        }



    }
    [System.Serializable]
    public class AccessRecvBadValue : SrbException
    {
        Access access;
        public AccessRecvBadValue(string message, Access access) : base(message)
        {
            this.access = access;
        }
        public override string ToString()
        {
            var rev = base.ToString() +"\n" ;
            rev += access.toJson();
            return rev;
        }
        protected AccessRecvBadValue(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
            throw new System.NotImplementedException();
        }

    }
}
