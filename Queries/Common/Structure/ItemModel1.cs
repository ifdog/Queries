using System;
using System.Collections.Generic;
using Common.Attribute;
using Common.Structure.Base;

namespace Common.Structure
{
    public class ItemModel1
    {
        public  List<byte> Id { get; set; }

        [Seen(@"名称", 1)]
        public string Name { get; set; }

        [Seen(@"品牌", 2)]
        public  string Brand { get; set; }

        [Seen(@"型号", 3)]
        public string Model { get; set; }

        [Seen(@"规格", 4)]
        public  string Spec { get; set; }

        [Seen(@"供应商", 5)]
        public  string Supplier { get; set; }

        [Seen(@"折扣", 6)]
        public  float Discount { get; set; }

        [Seen(@"表价", 7)]
        public float OriginPrice { get; set; }

        [Seen(@"价格", 8)]
        public float DiscountedPrice { get; set; }

        [Seen(@"备注", 9)]
        public  string Remark { get; set; }

        [Seen(@"创建者", 10)]
        public  string Owner { get; set; }

        [Seen(@"创建日期", 11)]
        public  DateTime CreateDate { get; set; }

        public string Mess { get; set; }

        public string Tag { get; set; }
    }
}
