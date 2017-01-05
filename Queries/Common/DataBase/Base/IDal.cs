using System.Collections.Generic;

namespace Service.Dal.Base
{
    public interface IDal<T> where T : Common.Structure.Base.BaseObject
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
