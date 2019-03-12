using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISFF
{
	public class Employee : IDataErrorInfo, ILogable
	{
        #region Constant

        const string FIO = "Fio";
        const string POST = "Post";
        const string ID_NUMBER = "IdNumber";
        const string EXP = "Exp";
        const string SALARY = "Salary";

        #endregion

        #region DataEntity
        // Название столбцов в таблице Сотрудники
        public int Id { get; set; }
		public string Fio { get; set; }
		public string Post { get; set; }
		public string IdNumber { get; set; }
		public int Exp { get; set; }
		public double Salary { get; set; }

        public Employee()
        {
            CorrectData = new CorrectDataService(new Dictionary<string, bool>
            {
                { FIO, false }, { POST, false}, { ID_NUMBER, false}, { EXP, false }, { SALARY, false}
            });
        }

        #endregion

        #region Validation

        [NotMapped]
        public CorrectDataService CorrectData { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case FIO:
                        if (Fio == null || Fio == string.Empty)
                            error = "Поле должно быть заполнено";
                        break;
                    case POST:
                        if (Post == null || Post == string.Empty)
                            error = "Поле должно быть заполнено";
                        break;
                    case EXP:
                        if(Exp < 0)
                            error = "Стаж не может быть отрицательным";
                        break;
                    case SALARY:
                        if (Salary <= 0)
                            error = "Зарплата не может быть отрицательной или нулевой";
                        break;
                    case ID_NUMBER:
                        if (IdNumber.Length != 12 || !IdNumber.All(char.IsDigit))
                            error = "В идентификационном номере должно быть 12 цифр";
                        break;
                }
                CorrectData.CheckCorrect(columnName, error);
                return error;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Fio;
        }

        #endregion

        #region CopyMethods

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

        #endregion
    }
}
