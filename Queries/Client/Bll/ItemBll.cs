using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Dal;
using Common.Enums;
using Common.Factory;
using Common.Static;
using Common.Structure;
using Common.Structure.Base;
using RestSharp;

namespace Client.Bll
{
    public class ItemBll
    {
        private readonly ItemDal _itemDal;

        public ItemBll(RestClient restClient)
        {
            _itemDal = new ItemDal(restClient);
        }

        public Task<BaseResult> Query(string query,int page, int length)
        {
            if (string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query))
            {
	            return Task.Run<BaseResult>(() => ResultFactory.CreateItemsResult(ResultCode.InvalidParameter));
            }
            try
            {
                return _itemDal.Query(query,page,length);
            }
            catch (Exception e)
            {
	            return Task.Run<BaseResult>(() => ResultFactory.CreateItemsResult(e));
            }
        }

        public Task<BaseResult> AddItem(List<ItemModel> itemModels)
        {
            var x = itemModels.Where(ModelCheck.Check).ToList();
            var r = new RequestItemsModel()
            {
                Action = "Add",
                Parameter = string.Empty,
                Items = x
            };
            try
            {
                return _itemDal.Post(r);
            }
            catch (Exception e)
            {
	            return Task.Run<BaseResult>(() => ResultFactory.CreateItemsResult(e));
            }
        }
    }
}
