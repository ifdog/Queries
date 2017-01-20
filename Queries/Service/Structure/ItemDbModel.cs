using Common.Attribute;
using Common.Structure;
using LiteDB;
using Service.Structure.Base;

namespace Service.Structure
{
	public class ItemDbModel:DbModel
	{
		public ItemModel Item { get; set; }
		[BsonIndex]
		[BsonField("Flat")]
		[TypeIndexed]
		public ItemModel FlatItem { get; set; }
	}
}
