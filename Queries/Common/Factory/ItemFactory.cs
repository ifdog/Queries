using System;
using Common.Static;
using Common.Structure;

namespace Common.Factory
{
    public class ItemFactory
    {
        public ItemModel Create(string name, string model, string brand, string spec, string supplier, float discount,
            float orignPrice, float discountedprice, string remark, long ownerId)
        {
            return new ItemModel
            {
                Name = name,
                Brand = brand,
                Model = model,
                Spec = spec,
                Discount = discount,
                OriginPrice = orignPrice,
                DiscountedPrice = discountedprice,
                Remark = remark,
                CreateDate = DateTime.Now,
                Mess =
                    Strings.Concat(Strings.ToPinyin(Strings.Filter(name)), Strings.ToShortPinyin(Strings.Filter(name)), Strings.Filter(model), Strings.Filter(brand),
                        Strings.Filter(spec), Strings.Filter(supplier), Strings.Filter(remark))
            };
        }
    }
}
