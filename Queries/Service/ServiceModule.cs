using System;
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
	        Post["/users/register/"] = _ =>
	        {
		        var model = this.Bind<RequestUserModel>();
		        if (model.User == null || model.Action == null)
		        {
			        return Response.AsJson(ResultFactory.CreateUserResult(ResultCode.JsonParseFail));
		        }
		        if (model.Action != "Register")
		        {
			        return Response.AsJson(ResultFactory.CreateUserResult(ResultCode.ActionNotCorresponding));
		        }
		        if (string.IsNullOrEmpty(model.User.UserName) || string.IsNullOrEmpty(model.User.Password))
		        {
			        return Response.AsJson(ResultFactory.CreateUserResult(ResultCode.InvalidParameter));
		        }
		        try
		        {
			        return Response.AsJson(_userBll.Register(model.User));
		        }
		        catch (Exception e)
		        {
			        return Response.AsJson(ResultFactory.CreateUserResult(e));
		        }
	        };

	        Post["/users/login/"] = _ =>
	        {
		        var model = this.Bind<RequestUserModel>();
		        if (model.User == null || model.Action == null)
		        {
			        return Response.AsJson(ResultFactory.CreateUserResult(ResultCode.JsonParseFail));
		        }
		        if (model.Action != "Login")
		        {
			        return Response.AsJson(ResultFactory.CreateUserResult(ResultCode.ActionNotCorresponding));
		        }
		        if (string.IsNullOrEmpty(model.User.UserName) || string.IsNullOrEmpty(model.User.Password))
		        {
			        return Response.AsJson(ResultFactory.CreateUserResult(ResultCode.InvalidParameter));
		        }
				try
				{
					var x = _userBll.Login(model.User);
					return Response.AsJson(x)
					  .WithCookie("Token", Token.Create(model.User.UserName, new TimeSpan(1, 0, 0, 0)));
				}
					catch (Exception e)
				{
					return Response.AsJson(ResultFactory.CreateUserResult(e));
				}
			};

	        Post["/users/updatepassword"] = _ =>
	        {
		        var model = this.Bind<RequestUserModel>();
		        if (model.User == null || model.Action == null)
		        {
			        return Response.AsJson(ResultFactory.CreateUserResult(ResultCode.JsonParseFail));
		        }
		        string token;
		        Request.Cookies.TryGetValue("Token", out token);
		        if (token == null || !Token.Validate(token))
		        {
			        return Response.AsJson(ResultFactory.CreateUserResult(ResultCode.NotLoggedIn));
		        }
		        if (model.Action != "UpdatePassword")
		        {
			        return Response.AsJson(ResultFactory.CreateUserResult(ResultCode.ActionNotCorresponding));
		        }
		        if (string.IsNullOrEmpty(model.User.UserName) || string.IsNullOrEmpty(model.User.Password))
		        {
			        return Response.AsJson(ResultFactory.CreateUserResult(ResultCode.InvalidParameter));
		        }
		        try
		        {
			        return Response.AsJson(_userBll.UpdatePassword(model.User));
		        }
		        catch (Exception e)
		        {
			        return Response.AsJson(ResultFactory.CreateUserResult(e));
		        }
	        };

	        Get["/items/get/{query}/"] = paramerters =>
	        {
		        string token;
		        Request.Cookies.TryGetValue("Token", out token);
		        if (token == null || !Token.Validate(token))
		        {
			        return Response.AsJson(ResultFactory.CreateItemsResult(ResultCode.NotLoggedIn));
		        }
		        if (string.IsNullOrWhiteSpace(paramerters.query))
		        {
			        return Response.AsJson(ResultFactory.CreateItemsResult(ResultCode.InvalidParameter));
		        }
		        int page, length;
		        if (!int.TryParse(Request.Query.Page, out page) | !int.TryParse(Request.Query.Length, out length))
		        {
			        return Response.AsJson(ResultFactory.CreateItemsResult(ResultCode.InvalidParameter));
		        }
		        try
		        {
			        ResultItemsModel x = _itemBll.Search(paramerters.query, page, length);
			        return Response.AsJson(x);
		        }
		        catch (Exception e)
		        {
			        return Response.AsJson(ResultFactory.CreateItemsResult(e));
		        }
	        };

	        Post["/items/add/"] = _ =>
	        {
		        var model = this.Bind<RequestItemsModel>();
		        if (model.Items == null || model.Action == null)
		        {
			        return Response.AsJson(ResultFactory.CreateItemsResult(ResultCode.JsonParseFail));
		        }
		        string token;
		        Request.Cookies.TryGetValue("Token", out token);
		        if (token == null || !Token.Validate(token))
		        {
			        return Response.AsJson(ResultFactory.CreateItemsResult(ResultCode.NotLoggedIn));
		        }
		        if (model.Action != "Add")
		        {
			        return Response.AsJson(ResultFactory.CreateItemsResult(ResultCode.ActionNotCorresponding));
		        }
		        try
		        {
			        return Response.AsJson(_itemBll.AddItems(model.Items, Token.ParseName(token)));
		        }
		        catch (Exception e)
		        {
			        return Response.AsJson(ResultFactory.CreateItemsResult(e));
		        }
	        };

            Get["/test/"] = _ => "done";
        }
	}
}
