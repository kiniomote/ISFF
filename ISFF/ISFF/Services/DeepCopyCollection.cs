using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace ISFF
{
    public static class DeepCopyCollection<T>
        where T : class
    {
        public static List<T> CopyToList(ICollection<T> collection)
        {
            List<T> newCollection = new List<T>();

            foreach(T element in collection)
            {
                newCollection.Add(element);
            }

            return newCollection;
        }

        public static ObservableCollection<T> CopyToObservableCollection(ICollection<T> collection)
        {
            ObservableCollection<T> newCollection = new ObservableCollection<T>();

            foreach (T element in collection)
            {
                newCollection.Add(element);
            }

            return newCollection;
        }

        public static List<T> CopyToListFromDb(DbSet<T> table)
        {
            List<T> newCollection = new List<T>();

            foreach (T element in table)
            {
                newCollection.Add(element);
            }

            return newCollection;
        }

        public static ObservableCollection<T> CopyToObservableCollectionFromDb(DbSet<T> table)
        {
            ObservableCollection<T> newCollection = new ObservableCollection<T>();

            foreach (T element in table)
            {
                newCollection.Add(element);
            }

            return newCollection;
        }

        public static void CopyElementsFromCollection(ICollection<T> collection, ICollection<T> collection_copy)
        {
            collection.Clear();
            foreach(T element in collection_copy)
            {
                collection.Add(element);
            }
        }

        public static void CopyElementsFromDb(ICollection<T> collection, DbSet<T> db_table)
        {
            collection.Clear();
            foreach (T element in db_table)
            {
                collection.Add(element);
            }
        }
    }
}
