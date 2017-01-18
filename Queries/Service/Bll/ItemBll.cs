using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Attribute;
using Common.Enums;
using Common.Factory;
using Common.Static;
using Common.Structure;
using Service.Dal;
using Service.Structure;

namespace Service.Bll
{
	public class ItemBll
	{
		private readonly LiteDal<ItemDbModel> _itemDal = new LiteDal<ItemDbModel>();

		private readonly string[] _itemPropertyNames = AttributeReader.GetProperty<SaveToDbAttribute>(new ItemModel())
			.Select(i => i.Name).ToArray();

		private readonly Type _itemType = typeof(ItemModel);

		public ResultItemsModel AddItem(ItemModel item, string user = "Default")
		{
			var dbModel = new ItemDbModel
			{
				Item = new ItemModel
				{
					Name = item.Name,
					Model = item.Model,
					Brand = item.Brand,
					Spec = item.Spec,
					Supplier = item.Supplier,
					Discount = item.Discount,
					OriginPrice = item.OriginPrice,
					CreateDate = DateTime.Now,
					Owner = user,
					Remark = item.Remark
				},
				FlatItem = new ItemModel()
			};
			_itemType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
				.Where(i => i.PropertyType == typeof(string) && _itemPropertyNames.Contains(i.Name))
				.ForEach(
					i => i.SetValue(dbModel.FlatItem, Strings.Flatten(_itemType.GetProperty(i.Name).GetValue(dbModel.Item) as string)));
			_itemDal.Insert(dbModel);
			return ResultFactory.CreateItemsResult(ResultCode.Ok);
		}

		public ResultItemsModel AddItems(IEnumerable<ItemModel> items, string user = "Default")
		{
			items.ForEach(i => AddItem(i, user));
			return ResultFactory.CreateItemsResult(ResultCode.Ok);
		}

		public ResultItemsModel Search(string hint, int page, int length)
		{
			return ResultFactory.CreateItemsResult(_itemDal.Find(hint, page, length).Select(i => i.Item));
		}
	}
}
