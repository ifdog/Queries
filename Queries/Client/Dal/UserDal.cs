using Common.Structure;
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

	    public ResultUserModel Post(RequestUserModel requestUserModel)
	    {
	        var request = new RestRequest("v2/users/{Action}", Method.POST)
	            {
	                RequestFormat = DataFormat.Json
	            }.AddUrlSegment("Action", requestUserModel.Action.ToLower())
	            .AddJsonBody(requestUserModel);
	        var response = _restClient.Execute<ResultUserModel>(request);
	        return response.Data;
	    }

	}
}
