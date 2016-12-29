using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Model.Base;

namespace Service.Model
{
	public class ItemModel:Item
	{
		public string Name { get; set; }
		public string Model { get; set; }
		public string Spec { get; set; }
		public string Brand { get; set; }
		public string Supplier { get; set; }
		public string Remark { get; set; }
		public float Discount { get; set; }
		public float OriginPrice { get; set; }
		public float DiscountedPrice { get; set; }
		public long OwnerId { get; set; }
		public DateTime CreateDate { get; set; }
		public string Tag { get; set; }
		public string Mess { get; set; }
	}
}
