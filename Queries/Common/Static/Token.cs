using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Static
{
	public static class Token
	{
		public static string Create(long id, TimeSpan timeSpan)
		{
			return Base64.Encode($"{id}|{UnixTimeStamp.ToUnixTimaStamp(DateTime.Now + timeSpan)}");
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

		public static long ParseId(string token)
		{
			var o = Base64.Decode(token).Split('|');
			if (o.Length != 2) return -1;
			try
			{
				return Convert.ToInt64(o[0]);
			}
			catch (Exception)
			{
				return -1;
			}
		}

	}
}
