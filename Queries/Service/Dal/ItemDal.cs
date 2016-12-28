using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Static;
using Service.Model;

namespace Service.Dal
{
	public class ItemDal
	{
		private readonly ItemContext _itemContext = new ItemContext();

		public void AddItem(long ownerId, string name, string model, string spec, string brand,
			string supplier, string remark, float discount, float originPrice)
		{
			_itemContext.Items.Add(new ItemModel
			{
				Name = name,
				Model = model,
				Spec = spec,
				Brand = brand,
				Supplier = supplier,
				Remark = remark,
				Discount = discount,
				OriginPrice = originPrice,
				OwnerId = ownerId,
				CreateDate = UnixTimeStamp.ToUnixTimaStamp(DateTime.Now),
				UpdateId = -1,
				UpdateDate = -1,
				Tag = string.Empty,
				Mess = Strings.ConcatAll(name, model, spec, brand, supplier, remark)
			});
			_itemContext.SaveChanges();
		}

		public void AddItem(ItemModel item)
		{
			_itemContext.Items.Add(item);
			_itemContext.SaveChanges();
		}

		public void AddItems(IEnumerable<ItemModel> items)
		{
			_itemContext.Items.AddRange(items);
			_itemContext.SaveChanges();
		}

		public IEnumerable<ItemModel> SearchForItem(string hint)
		{
			return _itemContext.Items.Where(i => i.Mess.Contains(hint)).Take(20).ToArray();
		}

		public void UpdateItem(long itemId, string name, string model, string spec, string brand,
			string supplier, string remark, float discount, float originPrice,
			long updateId)
		{
			var item = _itemContext.Items.FirstOrDefault(i => i.Id == itemId);
			if (item == null) return;
			item.Name = name;
			item.Model = model;
			item.Spec = spec;
			item.Brand = brand;
			item.Supplier = supplier;
			item.Remark = remark;
			item.Discount = discount;
			item.OriginPrice = originPrice;
			item.UpdateId = updateId;
			item.UpdateDate = UnixTimeStamp.ToUnixTimaStamp(DateTime.Now);
			item.Mess = Strings.ConcatAll(name, model, spec, brand, supplier, remark);
			_itemContext.SaveChanges();
		}

		public void DeleteItem(long itemId)
		{
			var item = _itemContext.Items.FirstOrDefault(i => i.Id == itemId);
			if (item == null) return;
			_itemContext.Items.Remove(item);
			_itemContext.SaveChanges();
		}
	}
}
