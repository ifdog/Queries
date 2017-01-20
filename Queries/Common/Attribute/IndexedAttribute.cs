using System;

namespace Common.Attribute
{
	[AttributeUsage(AttributeTargets.Property)]
	public class IndexedAttribute:System.Attribute
	{
		public bool ToFlat { get; set; }

		public IndexedAttribute(bool toFlat = false)
		{
			this.ToFlat = toFlat;
		}
	}
}
