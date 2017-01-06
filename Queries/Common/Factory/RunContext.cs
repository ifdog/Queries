using System;
using System.Collections.Generic;

namespace Common.Factory
{
	public class RunContext
	{
		private static readonly Dictionary<string, object> ObjectDict = new Dictionary<string, object>();
		private static readonly Dictionary<string, Delegate> Delegates = new Dictionary<string, Delegate>(); 
		private const string KeyTemplate = @"__(Name):{0}__(Uid):{1}__";
		public static void Add<T>(Func<T> func, string key = "Default") where T : class
		{
			var actKey = string.Format(KeyTemplate, typeof(T).FullName, key);
			Delegates.Add(actKey,func);
		}

        public static void TryAdd<T>(Func<T> func, string key = "Default") where T : class
        {
            if (ExistFun<T>(key)) return;
            var actKey = string.Format(KeyTemplate, typeof(T).FullName, key);
            Delegates.Add(actKey, func);
        }

        public static bool ExistObj<T>(string key = "Default") where T : class
        {
            var actKey = string.Format(KeyTemplate, typeof(T).FullName, key);
            return ObjectDict.ContainsKey(actKey);
        }

        public static bool ExistFun<T>(string key = "Default") where T : class
        {
            var actKey = string.Format(KeyTemplate, typeof(T).FullName, key);
            return Delegates.ContainsKey(actKey);
        }

        public static T Get<T>(string key = "Default") where T : class
		{
			var actKey = string.Format(KeyTemplate, typeof(T).FullName, key);
			if (ObjectDict.ContainsKey(actKey)) return ObjectDict[actKey] as T;
			var obj = GetNew<T>();
			return obj;
		}

        public static T GetNew<T>(string key = "Default") where T : class
		{
			Delegate x;
			var actKey = string.Format(KeyTemplate, typeof(T).FullName, key);
			if (!Delegates.TryGetValue(actKey, out x)) throw new Exception($"No such Func \"{actKey}\".");
			var func = x as Func<T>;
			if (func == null) throw new Exception($"No such Func \"{actKey}\".");
			var obj = func.Invoke();
			ObjectDict[actKey] = obj;
			return obj;
		}

		public static bool RemoveCache<T>(string key = "Default") where T : class
		{
			var actKey = string.Format(KeyTemplate, typeof(T).FullName, key);
			return ObjectDict.Remove(actKey);
		}

		public static bool RemoveFunc<T>(string key = "Default") where T : class
		{
			var actKey = string.Format(KeyTemplate, typeof(T).FullName, key);
			return Delegates.Remove(actKey);
		}

	}
}
