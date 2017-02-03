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

        private string _queryString;

        public string QueryString
        {
            get
            {
                _queryString = Queries.Count > 0
                    ? $"{QueryHead}@{string.Join(",", Queries.Select(i => $"{i.Key}:{i.Value}"))}"
                    : string.Empty;
                return _queryString;
            }
            set
            {
                _queryString = value;
                var w = _queryString.Split(_splitAt, StringSplitOptions.RemoveEmptyEntries);
                if (w.Length != 2) return;
                this.QueryHead = w[0];
                var x = w[1].Split(_splitComma, StringSplitOptions.RemoveEmptyEntries);
                x.Select(i => i.Split(_splitColon, StringSplitOptions.RemoveEmptyEntries))
                    .Where(i => i.Length == 2).ForEach(i => Queries.Add(i[0], i[1]));
            }
        }

        public string QueryHead { get; set; }

        public Dictionary<string, string> Queries { get; set; } = new Dictionary<string, string>();

    }
}
