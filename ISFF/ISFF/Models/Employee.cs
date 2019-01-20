using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
	public class Employee
	{
		// Название столбцов в таблице Сотрудники
		public int Id { get; set; }
		public string Fio { get; set; }
		public string Post { get; set; }
		public int IdNumber { get; set; }
		public int Exp { get; set; }
		public double Salary { get; set; }
	}
}
