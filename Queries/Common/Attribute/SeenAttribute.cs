using System;

namespace Common.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SeenAttribute:System.Attribute
    {
        public int Squence { get; set; }
        public string Description { get; set; }

        public SeenAttribute(string description , int sequence)
        {
            this.Description = description;
            this.Squence = sequence;
        }
    }
}
