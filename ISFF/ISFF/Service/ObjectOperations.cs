using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
	// Работа с данными в базе данных
	public class ObjectOperations<T> : IOperations<T>
	{
		public T ChosenObject { get; set; } // Объект, с которым будут операции

		public void AddToDataBase()
		{
            StaticControlDbService.db.Employees.Add(new Employee());
		}

		public void ChangeToDataBase(T changed_object)
		{

		}

		public void RemoveFromDataBase()
		{

		}
	}
}
