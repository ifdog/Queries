using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Bll
{
	public class ItemBll
	{
		private RunContext _run;
		private readonly ItemDal _itemDal;
		public ItemBll(RunContext run, ItemDal itemDal)
		{
			_run = run;
			_itemDal = itemDal;
		}

		public ResultCode Query(string hint, out List<ItemPostBody> itemList)
		{
			return _itemDal.Query(hint, out itemList);
		}

		public ResultCode AddItem(string name, string model, string spec, string brand,
			string supplier, string remark, float discount, float originPrice)
		{
			return _itemDal.AddItem(name, model, spec, brand, supplier, remark, discount, originPrice);
		}
	}
}
