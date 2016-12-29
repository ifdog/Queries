using System;
using Service.Model;
using Common.Static;
using static Common.Static.Strings;

namespace Service.Common.Factory
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
                    Concat(ToPinyin(Filter(name)), ToShortPinyin(Filter(name)), Filter(model), Filter(brand),
                        Filter(spec), Filter(supplier), Filter(remark))
            };
        }
    }
}
