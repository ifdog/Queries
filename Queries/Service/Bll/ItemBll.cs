﻿using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Common.Static;
using Common.Structure;
using Common.Structure.Base;
using Service.Common.Factory;
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
                ? _resultFactory.Create(ResultCode.Ok)
                : _resultFactory.Create(ResultCode.DataBaseUndefinedException);
        }

        public ResultItemsModel AddItems(IEnumerable<ItemModel> items)
        {
            return _itemDal.MultiInsert(items)
                ? _resultFactory.Create(ResultCode.Ok)
                : _resultFactory.Create(ResultCode.DataBaseUndefinedException);
        }

        public ResultItemsModel Search(string hint)
        { 
            var x = _itemDal.Select().Where(i => i.Mess.Contains(Strings.Filter(hint.ToUpper())));
            var baseList = new List<BaseObject>();
            x.ForEach(i=>baseList.Add(i));
            return
                _resultFactory.Create(baseList);
        }


    }
}
