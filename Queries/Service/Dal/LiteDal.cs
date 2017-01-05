using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LiteDB;
using Service.Dal.Base;

namespace Service.Dal
{
	public class LiteDal<T> : IDal<T>,IDisposable where T : Common.Structure.Base.BaseObject, new()
	{
		private readonly LiteDatabase _database;
		private readonly LiteCollection<T> _collection;
		public LiteDal(string indexProperty)
		{
			_database = new LiteDatabase($"{typeof(T).Name}.db");
			_collection = _database.GetCollection<T>(typeof(T).Name);
			_collection.EnsureIndex(indexProperty);
		}

		public void Insert(T obj)
		{
			obj.Id = ObjectId.NewObjectId().ToByteArray();
			_collection.Insert(obj);
		}

		public IEnumerable<T> Find(Expression<Func<T,bool>> pridecate,int skip=0,int max = 9999999)
		{
			return _collection.Find(pridecate, skip, max);
		}

		public void Update(T obj)
		{
			 _collection.Update(obj);
		}

		public void Delete(T obj)
		{
			throw new NotImplementedException();
		}

		public void BackUp(string path)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			_database.Dispose();
		}
	}
}
