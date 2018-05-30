using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lemonReceiver
{
	public class DataSR<T>
	{
	    public delegate void eTransfer(T data);
		public event eTransfer receive;
		private DataSR<T> that;
		public DataSR(eTransfer receiveFun)
		{
			receive += receiveFun;
		}
        static public void linkBasr(DataSR<T> a, DataSR<T> b)
		{
			a.that = b;
			b.that = a;
		}
        static public void disLinkBasr(DataSR<T> a)
		{
			if (a.that != null)
			{
				a.that.that = null;
				a.that = null;
			}
		}
		public void send(T data)
		{
			if (that != null)
			{
				if (that.that != this)
				{
					throw new Exception("配对的对方不指向自己");
				}
				else
				{
					that.receive.Invoke(data);
				}
			}
			else
			{
				throw new Exception("数据被发往一个没有配对过的端口");
			}
		}
	}
}
