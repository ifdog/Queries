using System;
using Common.Static;
using Common.Structure;

namespace Common.Factory
{
    public class ItemFactory
    {
        public static ItemModel Create(string name, string model, string brand, string spec, string supplier, float discount,
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
               
            };
        }
    }
}
