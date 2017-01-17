using System;

namespace Common.Structure.Base
{
    public abstract class BaseItem:BaseObject
    {
        public abstract string Name { get; set; }
        public abstract string Brand { get; set; }
        public abstract string Model { get; set; }
        public abstract string Spec { get; set; }
        public abstract string Supplier { get; set; }
        public abstract string Remark { get; set; }
        public abstract string Owner { get; set; }
        public abstract float Discount { get; set; }
        public abstract float OriginPrice { get; set; }
        public abstract DateTime CreateDate { get; set; }
    }
}
