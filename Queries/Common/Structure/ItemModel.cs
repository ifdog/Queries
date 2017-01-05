using System;
using Common.Attribute;
using Common.Structure.Base;

namespace Common.Structure
{
    public class ItemModel : BaseItem
    {
        public override byte[] Id { get; set; }

        [Seen(@"名称", 1)]
        public override string Name { get; set; }

        [Seen(@"品牌", 2)]
        public override string Brand { get; set; }

        [Seen(@"型号", 3)]
        public override string Model { get; set; }

        [Seen(@"规格", 4)]
        public override string Spec { get; set; }

        [Seen(@"供应商", 5)]
        public override string Supplier { get; set; }

        [Seen(@"折扣", 6)]
        public override float Discount { get; set; }

        [Seen(@"表价", 7)]
        public override float OriginPrice { get; set; }

        [Seen(@"价格", 8)]
        public float DiscountedPrice { get; set; }

        [Seen(@"备注", 9)]
        public override string Remark { get; set; }

        [Seen(@"创建者", 10)]
        public override string Owner { get; set; }

        [Seen(@"创建日期", 11)]
        public override DateTime CreateDate { get; set; }

        public override string Mess { get; set; }

        public string Tag { get; set; }
    }
}
