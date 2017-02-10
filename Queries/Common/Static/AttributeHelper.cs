using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Attribute;
using Common.Structure;
using NPOI.HSSF.Record;

namespace Common.Static
{
	public static class AttributeHelper
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

		private static List<PropertyInfo> _seenProperties;

		public static List<PropertyInfo> GetSeenProperties()
		{
			return _seenProperties ?? (_seenProperties = typeof(ItemModel)
				.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.Where(i => i.GetCustomAttribute<SeenFromUiAttribute>() != null)
				.OrderBy(i => i.GetCustomAttribute<SeenFromUiAttribute>().Squence)
				.ToList());
		}

		private static List<string> _seenDescriptions;

		public static List<string> GetSeenDescriptions()
		{
			return _seenDescriptions ?? (_seenDescriptions = GetSeenProperties()
				.Select(i => i.GetCustomAttribute<SeenFromUiAttribute>().Description)
				.ToList());
		}

		private static Dictionary<string, string> _seenPropertyDict;

		public static Dictionary<string, string> GetSeenPropertyDict()
		{
			return _seenPropertyDict ?? (_seenPropertyDict = GetSeenProperties()
				.Select(i => new
				{
					i.GetCustomAttribute<SeenFromUiAttribute>().Description,
					Name = $"Item.{i.Name}"
				})
				.ToDictionary(i => i.Description, i => i.Name));
		}

		private static List<string> _searchPropertyDescriptions;

		public static List<string> GetSearchPropertyDescriptions()
		{
			return _searchPropertyDescriptions ??
			       (_searchPropertyDescriptions = typeof(ItemModel).GetProperties(BindingFlags.Instance | BindingFlags.Public)
				       .Where(i => i.GetCustomAttribute<IndexedAttribute>() != null)
				       .Select(i => i.GetCustomAttribute<SeenFromUiAttribute>().Description)
				       .ToList());
		}

		private static List<string> _searchPropertyNames;

		public static List<string> GetSearchPropertyNames()
		{
			return _searchPropertyNames ??
				   (_searchPropertyNames = typeof(ItemModel).GetProperties(BindingFlags.Instance | BindingFlags.Public)
					   .Where(i => i.GetCustomAttribute<IndexedAttribute>() != null)
					   .Select(i =>$"Item.{i.Name}")
					   .ToList());
		}
	}
}
