using System;
using System.Collections.Generic;
using Common.Enums;
using Common.Structure;

namespace Common.Factory
{
    public class ResultFactory
    {
        public ResultUserModel CreateUserResult(ResultCode resultCode)
        {        
            return new ResultUserModel
            {
                ResultCode = (int)resultCode,
                Information = resultCode.ToString(),
                User = null
            };
        }

        public ResultUserModel CreateUserResult(Exception e)
        {
            return new ResultUserModel
            {
                ResultCode =  (int)ResultCode.ServerSideUndefinedException,
                Information = e.Message,
                User = null
            };
        }
        public ResultItemsModel CreateItemsResult(ResultCode resultCode)
        {
            return new ResultItemsModel
            {
                ResultCode = (int)resultCode,
                Information = resultCode.ToString(),
                Items = null
            };
        }

        public ResultItemsModel CreateItemsResult(Exception e)
        {
            return new ResultItemsModel
            {
                ResultCode = (int)ResultCode.ServerSideUndefinedException,
                Information = e.Message,
                Items = null
            };
        }

        public ResultItemsModel CreateItemsResult(List<ItemModel> result)
        {
            return new ResultItemsModel
            {
                ResultCode = (int)ResultCode.Ok,
                Information = ResultCode.Ok.ToString(),
                Items = result
            };
        }

        public ResultItemsModel CreateItemsResult(ResultCode resultCode, List<ItemModel> result)
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
