using System;

namespace Common.Attribute
{
	[AttributeUsage(AttributeTargets.Property)]
	public class TransportAttribute : System.Attribute
	{
		public bool ToClient { get; set; }
		public bool ToServer { get; set; }

		public TransportAttribute(bool toClient, bool toServer)
		{
			this.ToClient = toClient;
			this.ToServer = toServer;
		}
	}
}
