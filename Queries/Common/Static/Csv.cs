using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Static
{
	public static class Csv
	{
		public static string[] ParseLine(string aData)
		{
			char forSplit = '✂';
			string[] doubleQuotation = aData.Split('"');
			if (doubleQuotation.Length > 0)
			{
				for (int i = 0; i < doubleQuotation.Length; i++)
				{
					if (doubleQuotation[i].Contains(",") && i % 2 != 0)
					{
						doubleQuotation[i] = doubleQuotation[i].Replace(',', forSplit);
					}
				}
			}

			StringBuilder sbStr = new StringBuilder();
			foreach (string t in doubleQuotation)
			{
				sbStr.Append(t);
			}
			string newData = sbStr.ToString();
			string[] s = newData.Split(',');
			if (s.Length <= 0) return s;
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (s[i].Contains(forSplit.ToString()))
					{
						s[i] = s[i].Replace(forSplit, ',');
					}
				}
			}
			return s;
		}
	}
}
