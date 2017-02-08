using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Service.Dal.Base;

namespace Service.Dal
{
	public class PgDal<T>:IDal<T>
	{
		internal Npgsql.NpgsqlConnection Connection;
		public PgDal()
		{
			Connection = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=Queries;Password=Queries;Database=Queries;");
			Connection.Open();
			Connection.
		}
		public void Insert(T obj)
		{
			throw new NotImplementedException();
		}

		public void Update(T obj)
		{
			throw new NotImplementedException();
		}

		public void Delete(T obj)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> Find(string query, int page, int length)
		{
			throw new NotImplementedException();
		}
	}
}
