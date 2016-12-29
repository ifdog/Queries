using Common.Structure;
using Nancy;
using Nancy.ModelBinding;
using 

namespace Service
{
	public class Service : NancyModule
	{
		private readonly UserBll _userBll = new UserBll();
		private readonly ItemBll _itemBll = new ItemBll();
		private readonly TokenHelper _tokenHelper = new TokenHelper();

		public Service() : base("v1")
		{
			Post["/users/register/", true] = async (x, ct) =>
			{
				var result = await _userBll.Register(this.Bind<UserPostBody>());
				return Response.AsJson(result.ReusltPostBody);
			};

			Post["/users/login/", true] = async (x, ct) =>
			{
				var result = await _userBll.Login(this.Bind<UserPostBody>());
				return Response.AsJson(result.ReusltPostBody)
					.WithCookie("Token", result.Token);
			};

			Post["/users/updatepassword", true] = async (x, ct) =>
			{
				string token;
				Request.Cookies.TryGetValue("Token", out token);
				var result = await _userBll.UpdatePassword(this.Bind<UserPostBody>(), token);
				return Response.AsJson(result.ReusltPostBody);
			};

			Get["/items/get/{query}/", true] = async (x, ct) =>
			{
				string token;
				Request.Cookies.TryGetValue("Token", out token);
				BllResult result = await _itemBll.FindItem(x.query, token);
				return Response.AsJson(result.ItemPostBodies);
			};

			Post["/items/add/", true] = async (x, ct) =>
			{
				string token;
				Request.Cookies.TryGetValue("Token", out token);
				var result = await _itemBll.Additem(this.Bind<ItemPostBody>(), token);
				return Response.AsJson(result.ReusltPostBody);
			};

		}
	}
}
