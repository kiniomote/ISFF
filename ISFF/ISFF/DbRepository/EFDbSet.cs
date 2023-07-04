using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ISFF
{
    public class EFDbSet<T> : IDbSet<T>
        where T : class, IEntity
    {
        protected DbSet<T> _collectionElement;

        public EFDbSet(DbSet<T> dbset)
        {
            _collectionElement = dbset;
        }

        public void Add(T obj)
        {
            _collectionElement.Add(obj);
        }

        public T GetItem(int id)
        {
            return _collectionElement.Find(id);
        }

        public virtual T GetItem(T obj)
        {
            return _collectionElement.Find(obj.Id);
        }

        public void Remove(T obj)
        {
            _collectionElement.Remove(obj);
        }

        public ICollection<T> ToCollection()
        {
            return _collectionElement.ToList();
        }

        public void Update(T obj)
        {
            T searchItem = _collectionElement.Find(obj.Id);
            searchItem = obj;
        }
    }
}
