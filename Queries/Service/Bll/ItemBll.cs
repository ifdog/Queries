using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Service.Common.Factory;
using Service.Dal;
using Service.Dal.Base;
using Service.Model;

namespace Service.Bll
{
    public class ItemBll
    {
        private IDal<ItemModel> _itemDal = new BaseDal<ItemModel>();
        private ItemFactory _itemFactory = new ItemFactory();

        public ResultCode AddItem(string name, string model, string brand, string spec, string supplier, float discount,
            float orignPrice, float discountedprice, string remark, long ownerId)
        {
            
        }

        public ResultCode AddItems()
        {
            
        }

        public ResultCode Search()
        {
            
        }
    }
}
