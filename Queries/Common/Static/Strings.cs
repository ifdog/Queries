using System;
using System.Text;
using System.Text.RegularExpressions;


namespace Common.Static
{
	public static class Strings
	{


		public static string Concat(params string[] strings)
		{
			return string.Join("|", strings);
		}

		public static string Filter(string s)
		{
			if (s == null) return string.Empty;
			var a = Regex.Replace(s, @"[×|~|@|!|?|<|,|, |／|/|.|-|\s|#|$|\t|\n|#|\r|（|）|≤|、|(|)|℃|*]", string.Empty)
				.ToUpper();
			return a;
		}

	    public static string ToPinyin(string s)
	    {
	        return s.ToPinYin().ToUpper();
	    }

	    public static string ToShortPinyin(string s)
	    {
	        return s.ToPinYinAbbr().ToUpper();
	    }

		public static string Flatten(string s)
		{
			return Concat(Filter(s), ToPinyin(Filter(s)), ToShortPinyin(Filter(s)));
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

