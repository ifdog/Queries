using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Service.Dal.Base
{
	public interface IDal<T> where T : Common.Structure.Base.BaseObject
	{
		void Insert(T obj);
		void Update(T obj);
		void Delete(T obj);
		IEnumerable<T> Find(Expression<Func<T, bool>> pridecate, int skip, int max);
	}
}
