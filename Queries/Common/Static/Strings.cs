using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Static
{
	public static class Strings
	{
		public static string ConcatAll(params string[] strings)
		{
			strings = strings.Select(MessFilter).ToArray();
			return string.Join(string.Empty, strings);
		}

		public static string MessFilter(string s)
		{
			var a = Regex.Replace(s, @"[×|~|@|!|?|<|,|, |／|/|.|-|\s|#|$|\t|\n|#|\r|（|）|≤|、|(|)|℃|*]", string.Empty)
				.ToUpper();
			return a;
		}

		public static string GetWarp(string s, int warp = 15)
		{
			if (s.Length < 10) return s;
			var sb = new StringBuilder();
			for (int i = 0; i < s.Length / warp; i++)
			{
				sb.Append(s.Substring(i, warp));
				sb.Append("\r\n");
			}
			return sb.ToString();
		}
	}
}

