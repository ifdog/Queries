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

		public PgDal()
		{
			TableName = typeof(T).Name;
			Connection = new NpgsqlConnection("Server=127.0.0.1;Port=5433;User Id=Queries;Password=Queries;Database=Queries;");
			Connection.Open();
			using (var cmd = new NpgsqlCommand())
			{
				cmd.Connection = this.Connection;
				cmd.CommandText = $@"CREATE TABLE IF NOT EXISTS {TableName} (
									Id integer PRIMARY KEY,
									Item jsonb NOT NULL)";
				cmd.ExecuteNonQuery();
			}
		}
		public void Insert(T obj)
		{
			using (var cmd = new NpgsqlCommand())
			{
				cmd.Connection = this.Connection;
				cmd.CommandText =$@"INSERT INTO {TableName} VALUES ({obj.Id},{Json.FromObject(obj)}::jsonb); ";
				cmd.ExecuteNonQuery();
			}
		}

		public void Update(T obj)
		{
			using (var cmd = new NpgsqlCommand())
			{
				cmd.Connection = this.Connection;
				cmd.CommandText = $@"UPDATE {TableName} SET Item = {Json.FromObject(obj)} WHERE Id ={obj.Id}; ";
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
			switch (parser.QueryHead)
			{
				case "All":
					if (typeof(T) == typeof(ItemDbModel))
					{
						orderCmd = "";
					}
					searchCondition = string.Join(" AND ",parser.Queries.Select(i=>$" Item.data->'Item' -> 'Item' ->> {i.Key} LIKE {i.Value}"));
					break;
				case "Any":
					searchCondition = string.Join(" OR ", parser.Queries.Select(i => $" Item.data->'Item' -> 'Item' ->> {i.Key} LIKE {i.Value}"));
					break;
				case "Exa":
					searchCondition = string.Join(" AND ", parser.Queries.Select(i => $" Item.data->'Item' -> 'Item' ->> {i.Key} = {i.Value}"));
					break;
				default:
					return null;
			}
			using (var cmd = new NpgsqlCommand())
			{
				cmd.Connection = this.Connection;
				cmd.CommandText = $@"SELECT Item FROM {TableName} WHERE {searchCondition};  ";
				var r = cmd.ExecuteReader();
				return r.Cast<Model>().Select(i => Json.ToObject<T>(i.Item));
			}
		}
		private class Model
		{
			public int Id { get; set; }
			public string Item { get; set; }
		}
	}


}
