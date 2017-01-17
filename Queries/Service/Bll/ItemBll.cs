﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Attribute;
using Common.Enums;
using Common.Factory;
using Common.Static;
using Common.Structure;
using Service.Dal;

namespace Service.Bll
{
    public class ItemBll
    {
		private readonly LiteDal<ItemModel> _itemDal = new LiteDal<ItemModel>("Mess");
	    private readonly string[] _itemPropertyNames = AttributeReader.GetProperty<SaveToDbAttribute>(new ItemModel()).Select(i=>i.Name).ToArray();


	    public ResultItemsModel AddItem(ItemModel item)
	    {
		    item.Flat.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
			    .Where(i => i.PropertyType == typeof(string) && _itemPropertyNames.Contains(i.Name))
			    .ForEach(i => i.SetValue(item.Flat, Strings.Flatten(item.GetType().GetProperty(i.Name).GetValue(item) as string)));
		    _itemDal.Insert(item);
		    return ResultFactory.CreateItemsResult(ResultCode.Ok);
	    }

	    public ResultItemsModel AddItems(IEnumerable<ItemModel> items)
	    {
		    items.ForEach(i => AddItem(i));
		    return ResultFactory.CreateItemsResult(ResultCode.Ok);
	    }

	    public DisplayModel Search(string hint)
	    {
		    var q = hint.Split(' ').Where(i => !string.IsNullOrEmpty(i)).Select(Strings.Filter).ToArray();
		    var items = _itemDal.Find(i => i.Mess.Contains(q[0]));
		    if (q.Length > 1) items = items.Where(i => q.Skip(1).All(j => i.Mess.Contains(j)));
		    return new DisplayModel
		    {
			    ResultCode = (int) ResultCode.Ok,
			    Information = ResultCode.Ok.ToString(),
			    Items = ItemMapping.Map(items)
		    };
	    }
    }
}
