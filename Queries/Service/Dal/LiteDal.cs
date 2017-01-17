using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LiteDB;
using Service.Dal.Base;

namespace Service.Dal
{
	public class LiteDal<T> : IDal<T>,IDisposable where T :  new()
	{
		private readonly LiteDatabase _database;
		private readonly LiteCollection<T> _collection;
		public LiteDal()
		{
			_database = new LiteDatabase($"{typeof(T).Name}s.db");
			_collection = _database.GetCollection<T>(typeof(T).Name);
		}

		public void Insert(T obj)
		{
			_collection.Insert(obj);
		}

		public void Update(T obj)
		{
			 _collection.Update(obj);
		}

		public void Delete(T obj)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<T> Find(string query, int page, int length)
		{
			throw new NotImplementedException();
		}


		public void Dispose()
		{
			_database.Dispose();
		}
	}
}
