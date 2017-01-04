using System;
using System.Threading.Tasks;
using Common.Enums;
using Common.Factory;
using Common.Static;
using Common.Structure;
using Nancy;
using Nancy.ModelBinding;
using Service.Bll;


namespace Service
{
    public class ServiceModule : NancyModule
    {
        private readonly UserBll _userBll = new UserBll();
        private readonly ItemBll _itemBll = new ItemBll();

        public ServiceModule() : base("v2")
        {
            Post["/users/register/", true] = async (x, ct) =>
            {
                var result = await new Task<ResultUserModel>(() =>
                {
                    var model = this.Bind<RequestUserModel>();
                    if (model.User == null || model.Action == null)
                    {
                        return ResultFactory.CreateUserResult(ResultCode.JsonParseFail);
                    }
                    if (model.Action != "Register")
                    {
                        return ResultFactory.CreateUserResult(ResultCode.ActionNotCorresponding);
                    }
                    if (string.IsNullOrEmpty(model.User.UserName) || string.IsNullOrEmpty(model.User.Password))
                    {
                        return ResultFactory.CreateUserResult(ResultCode.InvalidParameter);
                    }
                    try
                    {
                        return _userBll.Register(model.User);
                    }
                    catch (Exception e)
                    {
                        return ResultFactory.CreateUserResult(e);
                    }
			    });
				return Response.AsJson(result);
			};

            Post["/users/login/", true] = async (x, ct) =>
            {
                var result = await new Task<ResultUserModel>(() =>
                {
                    var model = this.Bind<RequestUserModel>();
                    if (model.User == null || model.Action == null)
                    {
                        return ResultFactory.CreateUserResult(ResultCode.JsonParseFail);
                    }
                    if (model.Action != "Login")
                    {
                        return ResultFactory.CreateUserResult(ResultCode.ActionNotCorresponding);
                    }
                    if (string.IsNullOrEmpty(model.User.UserName) || string.IsNullOrEmpty(model.User.Password))
                    {
                        return ResultFactory.CreateUserResult(ResultCode.InvalidParameter);
                    }
                    try
                    {
                        return _userBll.Login(model.User);
                    }
                    catch (Exception e)
                    {
                        return ResultFactory.CreateUserResult(e);
                    }
                });
                return result.ResultCode == (int) ResultCode.Ok
                    ? Response.AsJson(result)
                        .WithCookie("Token", Token.Create(result.User.Id, new TimeSpan(1, 0, 0, 0)))
                    : Response.AsJson(result);
            };

			Post["/users/updatepassword", true] = async (x, ct) =>
			{
                var result = await new Task<ResultUserModel>(() =>
                {
                    var model = this.Bind<RequestUserModel>();
                    if (model.User == null || model.Action == null)
                    {
                        return ResultFactory.CreateUserResult(ResultCode.JsonParseFail);
                    }
                    string token;
                    Request.Cookies.TryGetValue("Token", out token);
                    if (token == null || !Token.Validate(token))
                    {
                        return ResultFactory.CreateUserResult(ResultCode.NotLoggedIn);
                    }
                    if (model.Action != "UpdatePassword")
                    {
                        return ResultFactory.CreateUserResult(ResultCode.ActionNotCorresponding);
                    }
                    if (string.IsNullOrEmpty(model.User.UserName) || string.IsNullOrEmpty(model.User.Password))
                    {
                        return ResultFactory.CreateUserResult(ResultCode.InvalidParameter);
                    }
                    try
                    {
                        return _userBll.UpdatePassword(model.User);
                    }
                    catch (Exception e)
                    {
                        return ResultFactory.CreateUserResult(e);
                    }
                });
                return Response.AsJson(result);
            };

			Get["/items/get/{query}/", true] = async (x, ct) =>
			{
                var result = await new Task<ResultItemsModel>(() =>
                {
                    string token;
                    Request.Cookies.TryGetValue("Token", out token);
                    if (token == null || !Token.Validate(token))
                    {
                        return ResultFactory.CreateItemsResult(ResultCode.NotLoggedIn);
                    }
                    if (x.query==null)
                    {
                        return ResultFactory.CreateItemsResult(ResultCode.InvalidParameter);
                    }
                    try
                    {
                        return _itemBll.Search(x.query);
                    }
                    catch (Exception e)
                    {
                        return ResultFactory.CreateItemsResult(e);
                    }
                });
                return Response.AsJson(result);
            };

			Post["/items/add/", true] = async (x, ct) =>
			{
                var result = await new Task<ResultItemsModel>(() =>
                {
                    var model = this.Bind<RequestItemsModel>();
                    if (model.Items == null || model.Action == null)
                    {
                        return ResultFactory.CreateItemsResult(ResultCode.JsonParseFail);
                    }
                    string token;
                    Request.Cookies.TryGetValue("Token", out token);
                    if (token == null || !Token.Validate(token))
                    {
                        return ResultFactory.CreateItemsResult(ResultCode.NotLoggedIn);
                    }
                    if (model.Action != "Add")
                    {
                        return ResultFactory.CreateItemsResult(ResultCode.ActionNotCorresponding);
                    }
                    try
                    {
                        return _itemBll.AddItems(model.Items);
                    }
                    catch (Exception e)
                    {
                        return ResultFactory.CreateItemsResult(e);
                    }
                });
                return Response.AsJson(result);
            };

		}
	}
}
