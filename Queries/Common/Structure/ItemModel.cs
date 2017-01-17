using System;
using Common.Attribute;
using Common.Structure.Base;

namespace Common.Structure
{
    public class ItemModel : BaseItem
    {
		[Transport(false,false)]
		[SaveToDb]
        public override byte[] Id { get; set; }

        [SeenFromUi(@"名称", 1)]
		[Transport(true,true)]
		[SaveToDb]
		[ToFlat]
        public override string Name { get; set; }

        [SeenFromUi(@"品牌", 2)]
		[Transport(true,true)]
		[SaveToDb]
		[ToFlat]
		public override string Brand { get; set; }

        [SeenFromUi(@"型号", 3)]
		[Transport(true,true)]
		[SaveToDb]
		[ToFlat]
		public override string Model { get; set; }

        [SeenFromUi(@"规格", 4)]
		[Transport(true,true)]
		[SaveToDb]
		[ToFlat]
		public override string Spec { get; set; }

        [SeenFromUi(@"供应商", 5)]
		[Transport(true,true)]
		[SaveToDb]
		[ToFlat]
		public override string Supplier { get; set; }

        [SeenFromUi(@"折扣", 6)]
		[Transport(true,true)]
		[SaveToDb]
        public override float Discount { get; set; }

        [SeenFromUi(@"表价", 7)]
		[Transport(true,true)]
		[SaveToDb]
        public override float OriginPrice { get; set; }

        [SeenFromUi(@"价格", 8)]
		[Transport(false,false)]
		[SaveToDb(false)]
        public float DiscountedPrice { get; set; }

        [SeenFromUi(@"备注", 9)]
		[Transport(true,true)]
		[SaveToDb]
		[ToFlat]
		public override string Remark { get; set; }

        [SeenFromUi(@"创建者", 10)]
		[Transport(true,false)]
		[SaveToDb]
		[ToFlat]
		public override string Owner { get; set; }

        [SeenFromUi(@"创建日期", 11)]
		[Transport(true,false)]
		[SaveToDb]
        public override DateTime CreateDate { get; set; }

		[Transport(false,false)]
		[SaveToDb]
        public string Tag { get; set; }

		[Transport(false,false)]
		[SaveToDb]
		public BaseItem Flat { get; set; }
    }
}
