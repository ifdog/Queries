using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Common.Static;
using Service.Common.Factory;
using Service.Dal;
using Service.Dal.Base;
using Service.Model;
using Service.Model.Base;

namespace Service.Bll
{
    public class ItemBll
    {
        private readonly IDal<Item> _itemDal = new BaseDal<Item>();

        public ResultCode AddItem(Item item)
        {
            return _itemDal.Insert(item) ? ResultCode.Ok : ResultCode.DataBaseUndefinedException;
        }

        public ResultCode AddItems(IEnumerable<Item> items)
        {
            return _itemDal.MultiInsert(items) ? ResultCode.Ok : ResultCode.DataBaseUndefinedException;
        }

        public IEnumerable<Item> Search(string hint)
        {
            return _itemDal.Select().Where(i=>i.Mess.Contains(Strings.Filter(hint.ToUpper())));
        }


    }
}
