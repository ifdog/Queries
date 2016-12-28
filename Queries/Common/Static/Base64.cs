using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Static
{
	public static class Base64
	{
		public static string Encode(Encoding encode, string source)
		{
			byte[] bytes = encode.GetBytes(source);
			return Convert.ToBase64String(bytes);
		}

		public static string Encode(string source)
		{
			return Encode(Encoding.UTF8, source);
		}

		public static string Decode(Encoding encode, string result)
		{
			byte[] bytes = Convert.FromBase64String(result);
			return encode.GetString(bytes);
		}

		public static string Decode(string result)
		{
			return Decode(Encoding.UTF8, result);
		}
	}
}
