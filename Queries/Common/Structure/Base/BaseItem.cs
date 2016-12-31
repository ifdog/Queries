using System;

namespace Common.Structure.Base
{
    public class BaseItem:BaseObject
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Spec { get; set; }
        public string Supplier { get; set; }
        public string Remark { get; set; }
        public string Owner { get; set; }
        public string Mess { get; set; }
        public float Discount { get; set; }
        public float OriginPrice { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
