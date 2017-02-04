using System;
using System.Collections.Generic;
using System.Linq;
using Common.Static;

namespace Common.Factory
{
    public class QueryParser
    {
		private readonly char[] _splitAt = { '@' };
		private readonly char[] _splitComma = { ',' };
        private readonly char[] _splitColon = { ':' };

	    public QueryParser(string queryString="")
	    {
		    QueryString = queryString;
	    }

        private string _queryString;

	    public string QueryString
	    {
		    get
		    {
			    _queryString = Queries.Count>0
				    ? $"{QueryHead}@{string.Join(",", Queries.Select(i => $"{i.Key}:{i.Value}"))}"
				    : string.Empty;
			    return _queryString;
		    }
		    set
		    {
			    var split = value.Split(_splitAt, StringSplitOptions.RemoveEmptyEntries);

			    switch (split[0])
			    {
				    case @"All":
					    QueryHead = @"All";
					    split[1].Split(_splitComma, StringSplitOptions.RemoveEmptyEntries)
						    .Select(i => i.Split(_splitColon, StringSplitOptions.RemoveEmptyEntries))
						    .Where(i => i.Length == 2).ForEach(i =>Queries.Add(new KeyValuePair<string, string>(i[0], i[1].ToUpper())));
					    break;
				    case @"Any":
					    QueryHead = @"Any";
						split[1].Split(_splitComma, StringSplitOptions.RemoveEmptyEntries)
						    .Select(i => i.Split(_splitColon, StringSplitOptions.RemoveEmptyEntries))
						    .Where(i => i.Length == 2).ForEach(i => Queries.Add(new KeyValuePair<string, string>(i[0], i[1].ToUpper())));
					    break;
				    case @"Exa":
					    QueryHead = @"Exa";
						split[1].Split(_splitComma, StringSplitOptions.RemoveEmptyEntries)
						    .Select(i => i.Split(_splitColon, StringSplitOptions.RemoveEmptyEntries))
						    .Where(i => i.Length == 2).ForEach(i => Queries.Add(new KeyValuePair<string, string>(i[0], i[1])));
					    break;
				    default:
					    var x = value.Split(_splitComma, StringSplitOptions.RemoveEmptyEntries);
					    x.Select(i => i.Split(_splitColon, StringSplitOptions.RemoveEmptyEntries))
						    .Where(i => i.Length == 2).ForEach(i =>
						    {
							    Queries.Add(
								    new KeyValuePair<string, string>(
									    AttributeHelper.GetSeenPropertyDict().ContainsKey(i[0])
										    ? AttributeHelper.GetSeenPropertyDict()[i[0]]
										    : i[0],
									    i[1].ToUpper()));
						    });
					    if (Queries.Count > 0)
					    {
						    QueryHead = @"All";
					    }
					    else
					    {
						    QueryHead = @"Any";
						    AttributeHelper.GetSearchPropertyNames().ForEach(i => Queries.Add(new KeyValuePair<string, string>(i,value.ToUpper())));
					    }
					    break;
			    }
		    }
	    }

	    public string QueryHead { get; set; }

        public List<KeyValuePair<string,string>> Queries { get; set; } 

    }
}
