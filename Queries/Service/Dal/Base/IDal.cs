using System.Collections.Generic;

namespace Service.Dal.Base
{
	public interface IDal<T>
	{
		void Insert(T obj);
		void Update(T obj);
		void Delete(T obj);
		IEnumerable<T> Find(string query, int page =0, int length=50);
	}
}
