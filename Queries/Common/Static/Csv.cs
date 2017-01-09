using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Structure;
using NPOI.Extension;

namespace Common.Static
{
	public static class Csv
	{
		public static IEnumerable<CsvModel> Parse(string path)
		{
			return NPOI.Extension.Excel.Load<CsvModel>(path);
		}
	}
}
