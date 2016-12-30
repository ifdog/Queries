using Service.Model.Base;

namespace Service.Model
{
	public class ItemModel:Item
	{
		public float DiscountedPrice { get; set; }
		public string Tag { get; set; }
	}
}
