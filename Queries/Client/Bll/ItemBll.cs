﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public BaseResult Query(string query)
        {
            if (string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query))
            {
                return ResultFactory.CreateUserResult(ResultCode.InvalidParameter);
            }
            try
            {
                return _itemDal.Query(query);
            }
            catch (Exception e)
            {
                return ResultFactory.CreateItemsResult(e);
            }
        }

        public BaseResult AddItem(List<ItemModel> itemModels)
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
				return ResultFactory.CreateItemsResult(e);
            }
        }
    }
}
