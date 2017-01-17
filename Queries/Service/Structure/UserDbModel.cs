using Common.Structure;
using LiteDB;

namespace Service.Structure
{
	public class UserDbModel
	{
		public byte[] Id { get; set; }
		[BsonIndex]
		[BsonField("User")]
		public UserModel User { get; set; }
	}
}
