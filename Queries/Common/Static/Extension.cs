using System;
using System.Collections.Generic;
using System.Reflection;
using Common.Attribute;
using Common.Enums;
using Common.Structure.Base;

namespace Common.Static
{
	public static class Extension
	{
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (T item in enumerable)
			{
				action.Invoke(item);
			}
		}

		public static int ToInt(this ResultCode resultCode)
		{
			return (int) resultCode;
		}

		public static string GetDescription(this ResultCode resultCode)
		{
			return resultCode.GetType()
				.GetField(resultCode.ToString())
				.GetCustomAttribute<StatusDescriptionAttribute>()
				.Description;
		}

		public static ResultCode ToResultCode(this BaseResult baseResult)
		{
			return (ResultCode) baseResult.ResultCode;
		}

		public static bool EqualsResultCode(this BaseResult baseResult, ResultCode resultCode)
		{
			return baseResult.ToResultCode() == resultCode;
		}
	}

	
}