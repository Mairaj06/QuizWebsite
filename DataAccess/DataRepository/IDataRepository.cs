using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataRepository
{
    public interface IDataRepository<T>
    {
        IQueryable<T> Get();
        T Get(int id);
        T Add(T obj);
        T Update(T obj);
        int Delete(bool IsDeleted);
        
    }
}
