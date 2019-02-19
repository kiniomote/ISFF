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

        public static Employee Copy(Employee employee_copy)
        {
            Employee employee = new Employee
            {
                Id = employee_copy.Id,
                Fio = employee_copy.Fio,
                Post = employee_copy.Post,
                IdNumber = employee_copy.IdNumber,
                Exp = employee_copy.Exp,
                Salary = employee_copy.Salary
            };
            return employee;
        }

        public static void CopyProperties(Employee employee, Employee employee_copy)
        {
            employee.Id = employee_copy.Id;
            employee.Fio = employee_copy.Fio;
            employee.Post = employee_copy.Post;
            employee.IdNumber = employee_copy.IdNumber;
            employee.Exp = employee_copy.Exp;
            employee.Salary = employee_copy.Salary;
        }
	}
}
