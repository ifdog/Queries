using System.Collections.Generic;
using Service.Model.Base;

namespace Service.Dal.Base
{
    public interface IDal<T> where T : Model.Base.Model
    {
        bool Insert(T obj);
	    bool MultiInsert(IEnumerable<T> objects);
        IEnumerable<T> Select();
        bool Update(T obj);
	    bool MultiUpdate(IEnumerable<T> objects);
        bool Delete(T obj);
	    bool MultiDelete(IEnumerable<T> objects);
	    bool BackUp(string path);
    }
}
