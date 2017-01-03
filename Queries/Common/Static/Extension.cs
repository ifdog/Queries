using System;
using System.Collections.Generic;
using Common.Enums;

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
	}

	
}