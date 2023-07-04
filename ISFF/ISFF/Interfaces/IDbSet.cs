using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public interface IDbSet<T>
        where T : class, IEntity
    {
        ICollection<T> ToCollection();
        T GetItem(int id);
        T GetItem(T obj);
        void Add(T obj);
        void Update(T obj);
        void Remove(T obj);
    }
}
