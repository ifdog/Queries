using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
	public class ItemModel
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Model { get; set; }
		public string Spec { get; set; }
		public string Brand { get; set; }
		public string Supplier { get; set; }
		public string Remark { get; set; }
		public float Discount { get; set; }
		public float OriginPrice { get; set; }
		public long OwnerId { get; set; }
		public long CreateDate { get; set; }
		public long UpdateId { get; set; }
		public long UpdateDate { get; set; }
		public string Tag { get; set; }
		public string Mess { get; set; }
	}
}
