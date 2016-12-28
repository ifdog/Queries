using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Common.Static;
using Common.Structure;
using Service.Dal;
using Service.Model;

namespace Service.Bll
{
	public class ItemBll
	{
		private readonly ItemDal _itemDal = new ItemDal();
		readonly TokenHelper _tokenHelper = new TokenHelper();


		public Task<BllResult> Additem(ItemPostBody itemPostBody, string token)
		{
			return Task.Run(() =>
			{
				if (string.IsNullOrEmpty(token))
				{
					return new BllResult(ResultCode.NotLoggedIn);
				}
				if (!_tokenHelper.Validate(token))
					return new BllResult(ResultCode.TokenExpired);
				if (itemPostBody?.Name == null)
				{
					return new BllResult(ResultCode.JsonParseFail);
				}
				try
				{
					var id = _tokenHelper.ParseId(token);
					_itemDal.AddItem(id, itemPostBody.Name, itemPostBody.Model, itemPostBody.Spec,
						itemPostBody.Brand, itemPostBody.Supplier, itemPostBody.Remark, itemPostBody.Discount,
						itemPostBody.OriginPrice);
					Log.Here($"Added item {itemPostBody.Name}");
				}
				catch (Exception e)
				{
					Log.Here(e);
					return new BllResult(e);
				}
				return new BllResult(ResultCode.Ok);
			});
		}

		public Task<BllResult> FindItem(string hint, string token)
		{
			return Task.Run(() =>
			{
				if (string.IsNullOrEmpty(token))
				{
					return new BllResult(ResultCode.NotLoggedIn);
				}
				if (!_tokenHelper.Validate(token))
					return new BllResult(ResultCode.TokenExpired);
				try
				{
					var result = new BllResult(ResultCode.Ok)
					{
						ItemPostBodies = _itemDal.SearchForItem(Strings.MessFilter(hint)).Select(i => new ItemPostBody
						{
							Id = i.Id,
							Name = i.Name,
							Model = i.Model,
							Spec = i.Spec,
							Brand = i.Brand,
							Supplier = i.Supplier,
							Remark = i.Remark,
							Discount = i.Discount,
							OriginPrice = i.OriginPrice,
							OwnerId = i.OwnerId,
							CreateDate = i.CreateDate,
							UpdateId = i.UpdateId,
							UpdateDate = i.UpdateDate,
							Tag = i.Tag
						}).ToList()
					};
					Log.Here($"Searching for {hint} returned {result.ItemPostBodies.Count} results");
					return result;
				}
				catch (Exception e)
				{
					Log.Here(e);
					return new BllResult(e);
				}
			});
		}
	}
}
