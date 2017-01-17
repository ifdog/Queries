using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Structure;

namespace Service.Structure
{
	public class ItemDbModel
	{
		public byte[] Id { get; set; }
		public ItemModel Item { get; set; }
		public ItemModel FlatItem { get; set; }
	}
}
