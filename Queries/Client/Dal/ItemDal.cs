using System.Linq;
using System.Threading.Tasks;
using Common.Static;
using Common.Structure;
using Common.Structure.Base;
using RestSharp;

namespace Client.Dal
{
	public class ItemDal
	{
		private readonly RestClient _restClient;

		public ItemDal(RestClient restClient)
		{
			this._restClient = restClient;
			_restClient.Proxy = null;
		}

		public Task<BaseResult> Post(RequestItemsModel requestItemsModel)
		{
			var request = new RestRequest("v2/items/{Action}", Method.POST)
			{
				RequestFormat = DataFormat.Json
			}.AddUrlSegment("Action", requestItemsModel.Action.ToLower())
				.AddJsonBody(requestItemsModel);
			return _restClient.ExecuteTaskAsync(request)
				.ContinueWith<BaseResult>(i => Json.ToObject<ResultItemsModel>(i.Result.Content));
		}

		public Task<BaseResult> Query(string query, int page, int length)
		{
			var request = new RestRequest("v2/items/get/{Query}", Method.GET)
				.AddUrlSegment("Query", query)
				.AddQueryParameter("Page", page.ToString())
				.AddQueryParameter("Length", length.ToString());

			return _restClient.ExecuteTaskAsync(request)
				.ContinueWith<BaseResult>(i => Json.ToObject<ResultItemsModel>(i.Result.Content));
		}
	}
}
