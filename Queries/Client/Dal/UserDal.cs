using System.Threading.Tasks;
using Common.Structure;
using Common.Structure.Base;
using RestSharp;

namespace Client.Dal
{
	public class UserDal
	{
	    private readonly RestClient _restClient;

	    public UserDal(RestClient restClient)
        {
            _restClient = restClient;
		    _restClient.Proxy = null;
        }

		public Task<BaseResult> Post(RequestUserModel requestUserModel)
		{
			var request = new RestRequest("v2/users/{Action}", Method.POST)
			{
				RequestFormat = DataFormat.Json
			}.AddUrlSegment("Action", requestUserModel.Action.ToLower())
				.AddJsonBody(requestUserModel);
			return _restClient.ExecuteTaskAsync<ResultUserModel>(request)
				.ContinueWith<BaseResult>(i => i.Result.Data);
		}
	}
}
