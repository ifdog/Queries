using Common.Attribute;

namespace Common.Enums
{
	public enum ResultCode
	{

		/// <summary>
		/// Normal situation
		/// </summary>
		[StatusDescription("成功")]
		Ok = 10000,

		/// <summary>
		/// Account associate
		/// </summary>
		[StatusDescription("用户名已存在")]
		UserNameAlreadyExist = 10101,
		[StatusDescription("用户名不存在")]
		UserNotExist = 10102,
		[StatusDescription("用户名或密码错误")]
		InvalidUserNameOrPassword = 10103,
		[StatusDescription("用户未登录")]
		NotLoggedIn = 10104,
		[StatusDescription("令牌超时")]
		TokenExpired = 10105,
		[StatusDescription("用户名不能包含空白字符")]
		UserNameShouldNotBeWhiteSpace = 10106,
		[StatusDescription("用户名不能为空")]
		UserNameShouldNotBeEmpty = 10107,
		[StatusDescription("密码不能为空")]
		PasswordShouldNotBeEmpty = 10108,

		/// <summary>
		/// Request Parameter
		/// </summary>
		[StatusDescription("动作不匹配")]
		ActionNotCorresponding = 10201,
		[StatusDescription("参数错误")]
		InvalidParameter = 10202,

		/// <summary>
		/// data corrupt
		/// </summary>
		[StatusDescription("Json解析失败")]
		JsonParseFail = 10301,

		/// <summary>
		/// Undefined exception
		/// </summary>
		[StatusDescription("服务端未定义错误")]
		ServerSideUndefinedException = 20001,
		[StatusDescription("客户端端未定义错误")]
		ClientSideUndefinedException = 20002,
		[StatusDescription("数据库未定义错误")]
		DataBaseUndefinedException = 20003
	}
}
