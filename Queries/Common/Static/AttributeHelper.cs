using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Common.Static
{
	public static class AttributeReader
	{
		public static IEnumerable<PropertyInfo> GetProperty<T>(object obj) where T : System.Attribute
		{
			return obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.Where(i => i.GetCustomAttribute<T>() != null);
		}

		public static T GetAttribute<T>(PropertyInfo property) where T : System.Attribute
		{
			return property.GetCustomAttribute<T>();
		}
	}
}
