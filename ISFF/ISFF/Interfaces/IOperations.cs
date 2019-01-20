using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
	// Интерфейс операций с базой данных
	public interface IOperations<T>
	{
		void AddToDataBase(); // Добавить в базу данных строку
		void ChangeToDataBase(T changed_object); // Изменить строку в базе данных
		void RemoveFromDataBase(); // Удалить строку из базы данных
	}
}
