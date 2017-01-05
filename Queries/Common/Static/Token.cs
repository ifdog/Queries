using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Static
{
	public static class Token
	{
		public static string Create(string userName, TimeSpan timeSpan)
		{
			return Base64.Encode($"{userName}|{UnixTimeStamp.ToUnixTimaStamp(DateTime.Now + timeSpan)}");
		}

		public static bool Validate(string token)
		{
			var o = Base64.Decode(token).Split('|');
			if (o.Length != 2) return false;
			try
			{
				var date = UnixTimeStamp.FromUnixTimeStamp(o[1]);
				if (date > DateTime.Now) return true;
			}
			catch (Exception)
			{
				return false;
			}
			return false;
		}

		public static string ParseName(string token)
		{
			var o = Base64.Decode(token).Split('|');
			if (o.Length != 2) return null;
			try
			{
				return o[0];
			}
			catch (Exception)
			{
				return null;
			}
		}

	}
}
