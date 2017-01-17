using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.Attribute;
using Common.Structure;

namespace Common.Static
{
    public static class ItemMapping
    {
        public static List<string[]> Map<T>(IEnumerable<T> models)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var seenProperties = properties.Where(i => i.GetCustomAttribute<SeenFromUiAttribute>() != null)
                .OrderBy(i => i.GetCustomAttribute<SeenFromUiAttribute>().Squence);
            var a = seenProperties.Select(i => i.GetCustomAttribute<SeenFromUiAttribute>().Description).ToArray();
            var b = models.Select(
                i => seenProperties.Select(
                    j => j.GetValue(i)?.ToString() ?? string.Empty).ToArray()
            ).ToList();
            b.Insert(0, a);
            return b;
        }
    }
}
