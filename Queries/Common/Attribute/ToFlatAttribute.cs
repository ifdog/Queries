using System;

namespace Common.Attribute
{
	[AttributeUsage(AttributeTargets.Property)]
	public class ToFlatAttribute:System.Attribute
	{
		public bool ToFlat { get; set; }

		public ToFlatAttribute(bool toFlat = true)
		{
			this.ToFlat = toFlat;
		}
	}
}
