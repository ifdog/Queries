using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Common.Factory;
using Common.Static;
using Common.Structure;
using Service.Dal;
using Service.Dal.Base;
using _itemDal = Common.DataBase.ItemDb;

namespace Service.Bll
{
    public class ItemBll
    {

        public ResultItemsModel AddItem(ItemModel item)
        {
            return _itemDal.Insert(item)
                ? ResultFactory.CreateItemsResult(ResultCode.Ok)
                : ResultFactory.CreateItemsResult(ResultCode.DataBaseUndefinedException);
        }

        public ResultItemsModel AddItems(IEnumerable<ItemModel> items)
        {
            return _itemDal.MultiInsert(items)
                ? ResultFactory.CreateItemsResult(ResultCode.Ok)
                : ResultFactory.CreateItemsResult(ResultCode.DataBaseUndefinedException);
        }

        public ResultItemsModel Search(string hint)
        { 
            var x = _itemDal.Select().Where(i => i.Mess.Contains(Strings.Filter(hint)));
            var baseList = new List<ItemModel>();
            x.ForEach(i=>baseList.Add(i));
            return
				ResultFactory.CreateItemsResult(baseList);
        }
    }
}
