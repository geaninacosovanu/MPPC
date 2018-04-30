using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPPLab3.repository
{
    public interface IRepository<ID,T>
    {
        int Size();
        void Save(T entity);
        void Delete(ID id);
        void Update(ID id, T entity);
        T FindOne(ID id);
        IEnumerable<T> FindAll();
    }
}
