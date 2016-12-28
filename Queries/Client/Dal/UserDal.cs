using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Dal
{
	public class UserDal
	{
		public string ServerDomain { get; set; }
		private readonly HttpHelper _httpHelper;
		private readonly ResultHelper _resultHelper = new ResultHelper();
		private RunContext _run;

		public UserDal(RunContext run, HttpHelper httpHelper)
		{
			_run = run;
			ServerDomain = run.Configuration.RequestingPath;
			_httpHelper = httpHelper;
		}

		public ResultCode Register(string userName, string password)
		{
			var body = new UserPostBody
			{
				Action = "Register",
				UserName = userName,
				Password = password
			};
			var postString = Json.FromObject(body);
			ResultCode resultCode;
			try
			{
				var result = _httpHelper.Post($"{this.ServerDomain}/v1/users/register/", postString);
				resultCode = (ResultCode)_resultHelper.GetResultCode(result);
			}
			catch (Exception)
			{
				return ResultCode.ClientSideUndefinedException;
			}
			return resultCode;
		}

		public ResultCode Login(string userName, string password)
		{
			var body = new UserPostBody
			{
				Action = "Login",
				UserName = userName,
				Password = password
			};
			var postString = Json.FromObject(body);
			ResultCode resultCode;
			try
			{
				var result = _httpHelper.Post($"{this.ServerDomain}/v1/users/login/", postString);
				resultCode = (ResultCode)_resultHelper.GetResultCode(result);
			}
			catch (Exception)
			{
				return ResultCode.ClientSideUndefinedException;
			}
			return resultCode;
		}

		public ResultCode UpdatePassword(string userName, string password)
		{
			var body = new UserPostBody
			{
				Action = "UpdatePassword",
				UserName = userName,
				Password = password
			};
			var postString = Json.FromObject(body);
			ResultCode resultCode;
			try
			{
				var result = _httpHelper.Post($"{this.ServerDomain}/v1/users/updatepassword/", postString);
				resultCode = (ResultCode)_resultHelper.GetResultCode(result);
			}
			catch (Exception)
			{
				return ResultCode.ClientSideUndefinedException;
			}
			return resultCode;
		}

	}
}
