using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Common.Factory;
using Common.Static;
using Common.Structure;
using Service.Dal;
using Service.Dal.Base;

namespace Service.Bll
{
    public class ItemBll
    {
        private readonly IDal<ItemModel> _itemDal = new BaseDal<ItemModel>();
        private readonly ResultFactory _resultFactory = new ResultFactory();

        public ResultItemsModel AddItem(ItemModel item)
        {
            return _itemDal.Insert(item)
                ? _resultFactory.CreateItemsResult(ResultCode.Ok)
                : _resultFactory.CreateItemsResult(ResultCode.DataBaseUndefinedException);
        }

        public ResultItemsModel AddItems(IEnumerable<ItemModel> items)
        {
            return _itemDal.MultiInsert(items)
                ? _resultFactory.CreateItemsResult(ResultCode.Ok)
                : _resultFactory.CreateItemsResult(ResultCode.DataBaseUndefinedException);
        }

        public ResultItemsModel Search(string hint)
        { 
            var x = _itemDal.Select().Where(i => i.Mess.Contains(Strings.Filter(hint)));
            var baseList = new List<ItemModel>();
            x.ForEach(i=>baseList.Add(i));
            return
                _resultFactory.CreateItemsResult(baseList);
        }
    }
}
