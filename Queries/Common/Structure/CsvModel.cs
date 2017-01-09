using NPOI.Extension;

namespace Common.Structure
{
	public class CsvModel
	{
		[Column(Index = 1,Title = "名称")]
		public string Name { get; set; }
		[Column(Index = 2, Title = "型号")]
		public string Model { get; set; }
		[Column(Index = 3, Title = "规格")]
		public string Spec { get; set; }
		[Column(Index = 4, Title = "品牌")]
		public string Brand { get; set; }
		[Column(Index = 5, Title = "供应商")]
		public string Supplier { get; set; }
		[Column(Index = 6, Title = "备注")]
		public string Remark { get; set; }
		[Column(Index = 7, Title = "折扣")]
		public string Discount { get; set; }
		[Column(Index = 8, Title = "表价")]
		public string OriginPrice { get; set; }
	}
}
