using System;

namespace Common.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SeenFromUiAttribute:System.Attribute
    {
        public int Squence { get; set; }
        public string Description { get; set; }

        public SeenFromUiAttribute(string description , int sequence)
        {
            this.Description = description;
            this.Squence = sequence;
        }
    }
}
