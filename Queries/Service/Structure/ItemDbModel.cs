using Common.Structure;
using LiteDB;

namespace Service.Structure
{
	public class ItemDbModel
	{
		public byte[] Id { get; set; }
		public ItemModel Item { get; set; }
		[BsonIndex]
		[BsonField("Flat")]
		public ItemModel FlatItem { get; set; }
	}
}
