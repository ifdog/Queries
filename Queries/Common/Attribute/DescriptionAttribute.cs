using System;

namespace Common.Attribute
{
	[AttributeUsage(AttributeTargets.Field)]
	public class StatusDescriptionAttribute:System.Attribute
	{
		public string Description { get; set; }

		public StatusDescriptionAttribute(string description)
		{
			this.Description = description;
		}
	}
}
