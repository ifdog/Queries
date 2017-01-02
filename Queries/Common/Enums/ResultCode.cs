namespace Common.Enums
{
	public enum ResultCode
	{

		/// <summary>
		/// Normal situation
		/// </summary>
		Ok = 10000,

		/// <summary>
		/// Account associate
		/// </summary>
		UserNameAlreadyExist = 10101,
		UserNotExist = 10102,
		InvalidUserNameOrPassword = 10103,
		NotLoggedIn = 10104,
		TokenExpired = 10105,
        UserNameShouldNotBeWhiteSpace = 10106,
        UserNameShouldNotBeEmpty = 10107,
        PasswordShouldNotBeEmpty = 10108,

		/// <summary>
		/// Request Parameter
		/// </summary>
		ActionNotCorresponding = 10201,
		InvalidParameter = 10202,

		/// <summary>
		/// data corrupt
		/// </summary>
		JsonParseFail = 10301,

		/// <summary>
		/// Undefined exception
		/// </summary>
		ServerSideUndefinedException = 20001,
		ClientSideUndefinedException = 20002,
        DataBaseUndefinedException = 20003
	}
}
