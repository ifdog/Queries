using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Common.Factory;
using Common.Static;
using Common.Structure;
using Service.Dal;

namespace Service.Bll
{
    public class ItemBll
    {
		private readonly LiteDal<ItemModel> _itemDal = new LiteDal<ItemModel>("Mess");

	    public ResultItemsModel AddItem(ItemModel item)
	    {
	        item.Mess = Strings.Filter(Strings.Concat(item.Name, item.Model, item.Spec, item.Remark).ToUpper());
		    _itemDal.Insert(item);
		    return ResultFactory.CreateItemsResult(ResultCode.Ok);
	    }

	    public ResultItemsModel AddItems(IEnumerable<ItemModel> items)
	    {
		    items.ForEach(i => AddItem(i));
		    return ResultFactory.CreateItemsResult(ResultCode.Ok);
	    }

	    public DisplayModel Search(string hint)
	    {
		    var x = _itemDal.Find(i=>i.Mess.Contains(hint.ToUpper()));
            return new DisplayModel
            {
                ResultCode = (int)ResultCode.Ok,
                Information = ResultCode.Ok.ToString(),
                Items = ItemMapping.Map(x)
            };
        }
    }
}
