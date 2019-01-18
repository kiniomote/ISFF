using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF_v1._0
{
	// Работа с данными в базе данных
	public class ObjectOperations<T> : IOperations<T>
	{
		public T Object { get; set; } // Объект, с которым будут операции

		public void AddToDataBase()
		{

		}

		public void ChangeToDataBase(T changed_object)
		{

		}

		public void RemoveFromDataBase()
		{

		}
	}
}
