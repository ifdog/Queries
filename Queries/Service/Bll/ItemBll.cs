using System.Collections.Generic;
using Common.DataBase;
using Common.Enums;
using Common.Factory;
using Common.Static;
using Common.Structure;

namespace Service.Bll
{
    public class ItemBll
    {
		private readonly LiteDal<ItemModel> _itemDal = new LiteDal<ItemModel>("Mess");

	    public ResultItemsModel AddItem(ItemModel item)
	    {
		    _itemDal.Insert(item);
		    return ResultFactory.CreateItemsResult(ResultCode.Ok);
	    }

	    public ResultItemsModel AddItems(IEnumerable<ItemModel> items)
	    {
		    items.ForEach(i => _itemDal.Insert(i));
		    return ResultFactory.CreateItemsResult(ResultCode.Ok);
	    }

	    public ResultItemsModel Search(string hint)
	    {
		    var x = _itemDal.Find(i => i.Mess.Contains(Strings.Filter(hint)));
            var baseList = new List<ItemModel>();
            x.ForEach(i=>baseList.Add(i));
            return
				ResultFactory.CreateItemsResult(baseList);
        }
    }
}
