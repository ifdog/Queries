using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Common.Structure;

namespace Common.Factory
{
    public class ResultFactory
    {
        public static ResultUserModel CreateUserResult(ResultCode resultCode)
        {        
            return new ResultUserModel
            {
                ResultCode = (int)resultCode,
                Information = resultCode.ToString(),
                User = null
            };
        }

        public static ResultUserModel CreateUserResult(Exception e)
        {
            return new ResultUserModel
            {
                ResultCode =  (int)ResultCode.ServerSideUndefinedException,
                Information = e.Message,
                User = null
            };
        }
        public static ResultItemsModel CreateItemsResult(ResultCode resultCode)
        {
            return new ResultItemsModel
            {
                ResultCode = (int)resultCode,
                Information = resultCode.ToString(),
                Items = null
            };
        }

        public static ResultItemsModel CreateItemsResult(Exception e)
        {
            return new ResultItemsModel
            {
                ResultCode = (int)ResultCode.ServerSideUndefinedException,
                Information = e.Message,
                Items = null
            };
        }

        public static ResultItemsModel CreateItemsResult(IEnumerable<ItemModel> result)
        {
            return new ResultItemsModel
            {
                ResultCode = (int)ResultCode.Ok,
                Information = ResultCode.Ok.ToString(),
                Items = result.ToList()
            };
        }

        public static ResultItemsModel CreateItemsResult(ResultCode resultCode, List<ItemModel> result)
        {
            return new ResultItemsModel
            {
                ResultCode = (int)resultCode,
                Information = resultCode.ToString(),
                Items = result
            };
        }
    }
}
