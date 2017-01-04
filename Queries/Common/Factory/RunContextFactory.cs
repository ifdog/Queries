using System;
using System.Collections.Generic;

namespace Common.Factory
{
	public class RunContextFactory
	{
		private static readonly Dictionary<string, object> ObjectDict = new Dictionary<string, object>();
		private const string KeyTemplate = @"__(Name):{0}__(Uid):{1}__";


		public static void Add<T>(T obj, string key = "Default") where T : class
		{
			var actKey = string.Format(KeyTemplate, typeof(T).FullName, key);
			ObjectDict.Add(actKey, obj);
		}

		public static T Get<T>(string key = "Default") where T : class
		{
			object x;
			var actKey = string.Format(KeyTemplate, typeof(T).FullName, key);
			if (ObjectDict.TryGetValue(actKey, out x))
			{
				return x as T;
			}
			throw new Exception($"No such object \"{actKey}\".");
		}

	}
}
