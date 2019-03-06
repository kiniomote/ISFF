using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ISFF
{
	public class Employee : IDataErrorInfo
	{
		// Название столбцов в таблице Сотрудники
		public int Id { get; set; }
		public string Fio { get; set; }
		public string Post { get; set; }
		public string IdNumber { get; set; }
		public int Exp { get; set; }
		public double Salary { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Exp":
                        if(Exp < 0)
                            error = "Стаж не может быть отрицательным";
                        break;
                    case "Salary":
                        if (Salary <= 0)
                            error = "Зарплата не может быть отрицательной или нулевой";
                        break;
                    case "IdNumber":
                        if (IdNumber.Length != 12 || !IdNumber.All(char.IsDigit))
                            error = "В идентификационном номере должно быть 12 цифр";
                        break;
                }
                return error;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

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
