using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Dal
{
	public class ItemDal
	{
		public string ServerDomain { get; set; }
		private readonly HttpHelper _httpHelper;
		private readonly ResultHelper _resultHelper = new ResultHelper();
		private RunContext _run;

		public ItemDal(RunContext run, HttpHelper httpHelper)
		{
			_run = run;
			ServerDomain = run.Configuration.RequestingPath;
			_httpHelper = httpHelper;
		}

		public ResultCode Query(string s, out List<ItemPostBody> items)
		{
			items = null;
			try
			{
				var result = _httpHelper.Get($"{this.ServerDomain}/v1/items/get/{s}");
				items = _resultHelper.GetResultItems(result);
				return ResultCode.Ok;
			}
			catch (Exception)
			{
				return ResultCode.ClientSideUndefinedException;
			}
		}

		public ResultCode AddItem(string name, string model, string spec, string brand,
			string supplier, string remark, float discount, float originPrice)
		{
			var postBody = new ItemPostBody
			{
				Name = name,
				Model = model,
				Spec = spec,
				Brand = brand,
				Supplier = supplier,
				Remark = remark,
				Discount = discount,
				OriginPrice = originPrice
			};
			var postString = Json.FromObject(postBody);
			try
			{
				var result = _httpHelper.Post($"{this.ServerDomain}/v1/items/add/", postString);
				return (ResultCode)_resultHelper.GetResultCode(result);
			}
			catch (Exception)
			{
				return ResultCode.ClientSideUndefinedException;
			}
		}
	}
}
