using System;
using System.Collections.Generic;
using Common.Static;
using Npgsql;
using Service.Dal.Base;
using Service.Structure.Base;
using System.Linq;
using Common.Factory;
using Service.Structure;

namespace Service.Dal
{
	public class PgDal<T>:IDal<T> where T:DbModel
	{
		internal NpgsqlConnection Connection;
		internal string TableName;
		private readonly char[] _splitAt = { '@' };
		private readonly char[] _splitComma = { ',' };
		private readonly char[] _splitColon = { ':' };
		private readonly char[] _splitDot = { '.' };

		public PgDal()
		{
			TableName = typeof(T).Name;
			Connection = new NpgsqlConnection("Server=127.0.0.1;Port=5433;User Id=Queries;Password=Queries;Database=Queries;");
			Connection.Open();
			using (var cmd = new NpgsqlCommand())
			{
				cmd.Connection = this.Connection;
				cmd.CommandText = $@"CREATE TABLE IF NOT EXISTS {TableName} (Id integer PRIMARY KEY, Item jsonb NOT NULL)";
				cmd.ExecuteNonQuery();
				
			}
		}
		public void Insert(T obj)
		{
			obj.Id = Identify.NewId();
			using (var cmd = new NpgsqlCommand())
			{
				cmd.Connection = this.Connection;
				cmd.CommandText =$@"INSERT INTO {TableName} VALUES ({obj.Id},'{Json.FromObject(obj)}'); ";
				cmd.ExecuteNonQuery();
			}
		}

		public void Update(T obj)
		{
			using (var cmd = new NpgsqlCommand())
			{
				cmd.Connection = this.Connection;
				cmd.CommandText = $@"UPDATE {TableName} SET Item = '{Json.FromObject(obj)}' WHERE Id ={obj.Id}; ";
				cmd.ExecuteNonQuery();
			}
		}

		public void Delete(T obj)
		{
			using (var cmd = new NpgsqlCommand())
			{
				cmd.Connection = this.Connection;
				cmd.CommandText = $@"DELETE FROM {TableName} WHERE Id = {obj.Id}; ";
				cmd.ExecuteNonQuery();
			}
		}

		public IEnumerable<T> Find(string query, int page =0, int length=50)
		{
			var w = query.Split(_splitAt, StringSplitOptions.RemoveEmptyEntries);
			if (w.Length < 2) return new List<T>();
			var x = w[1].Split(_splitComma, StringSplitOptions.RemoveEmptyEntries);
			if (x.Length == 0) return new List<T>();
			if (!x.All(i => i.Contains(':'))) return new List<T>();
			var y = x.Select(i => i.Split(_splitColon, StringSplitOptions.RemoveEmptyEntries)).ToArray();
			if (y.Any(i => i.Length != 2)) return new List<T>();
			var parser = new QueryParser(query);
			string searchCondition;
			string orderCmd = String.Empty;
			string _table = TableName;
			if (parser.Queries.Count == 0) return null;
			switch (parser.QueryHead)
			{
				case "All":
					if (typeof(T) == typeof(ItemDbModel))
					{
						orderCmd = "order by length(content->'Item'->>'Name');";
					}
					searchCondition = string.Join(" AND ",parser.Queries.Select(i => $" Item -> '{i.Key.Split(_splitDot)[0]}' ->> '{i.Key.Split(_splitDot)[1]}' ilike '%{i.Value}%'"));
					break;
				case "Any":
					searchCondition = string.Join(" OR ", parser.Queries.Select(i => $" Item ->  '{i.Key.Split(_splitDot)[0]}' ->> '{i.Key.Split(_splitDot)[1]}' ilike '%{i.Value}%'"));
					break;
				case "Exa":
					searchCondition = string.Join(" AND ", parser.Queries.Select(i => $" Item ->  '{i.Key.Split(_splitDot)[0]}' ->> '{i.Key.Split(_splitDot)[1]}' = '{i.Value}'"));
					break;
				case "Eth":
					_table = @"lateral jsonb_each_text(content->'Item')";
					searchCondition = string.Join(" AND ", parser.Queries.Select(i => $" value ilike '%{i.Value}%'"));
					break;
				default:
					return null;
			}
			using (var cmd = new NpgsqlCommand())
			{
				cmd.Connection = this.Connection;
				cmd.CommandText = $@"SELECT * FROM {_table} WHERE {searchCondition} {orderCmd};";
				var l = new List<T>();
				using (var r = cmd.ExecuteReader())
				{
					while (r.Read())
					{
						l.Add(Json.ToObject<T>(r.GetString(1)));
					}
					r.Close();
				}
				return l;
			}
		}
	}
}
