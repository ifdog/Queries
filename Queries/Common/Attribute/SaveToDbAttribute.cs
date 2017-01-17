using System;

namespace Common.Attribute
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SaveToDbAttribute:System.Attribute
	{
		public bool Save { get; set; }

		public SaveToDbAttribute(bool save = true)
		{
			this.Save = save;
		}
	}
}
