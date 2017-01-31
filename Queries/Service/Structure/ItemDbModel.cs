using Common.Attribute;
using Common.Structure;
using LiteDB;
using Service.Structure.Base;

namespace Service.Structure
{
	public class ItemDbModel:DbModel
	{
		public ItemModel Item { get; set; }
		[BsonField("Flat")]
		[TypeIndexed]
		public ItemModel FlatItem { get; set; }
	}
}
