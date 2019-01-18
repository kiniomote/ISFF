using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ISFF_v1._0
{
	// Запись и чтение файла лога
	public static class StaticJsonLog<T>
	{
		// Константы, описывающие тип операции для WriteInLogFileAction
		public const int ADD_OPERATION = 0;
		public const int CHANGE_OPERATION = 1;
		public const int REMOVE_OPERATION = 2;

		public static void WriteInLogFileAction(T obj,int type_operation)
		{

		} // Запись операции в файл лога

		public static void ReadFromLogFileOnScreen(T obj)
		{

		} // Отображение всех записей файла лога на экран
	}
}