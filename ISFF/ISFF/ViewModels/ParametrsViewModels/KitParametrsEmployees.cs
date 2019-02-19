using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace ISFF
{
    public class KitParametrsEmployees : INotifyPropertyChanged
    {
        //_______________________________

        public KitParametrsEmployees()
        {
            IsReadOnly = true;
            IsEnableCollection = true;
            IsBusy = false;
            db = new ConnectionDB();
            db.Employees.Load();
            Employees = new ObservableCollection<Employee>();
            foreach(Employee employee in db.Employees)
            {
                Employees.Add(employee);
            }
            //Employees = new ObservableCollection<Employee>
            //{
            //    new Employee{ Id=1, Fio="Попадин Дмитрий Владимирович", IdNumber=156712, Post="Директор", Salary=60000, Exp=7},
            //    new Employee{ Id=2, Fio="Василич Вася Владимирович", IdNumber=24497, Post="Повар", Salary=20000, Exp=2},
            //    new Employee{ Id=3, Fio="Иваныч Иван Владимирович", IdNumber=74942, Post="Повар", Salary=20000, Exp=5},
            //    new Employee{ Id=4, Fio="Петров Петя Владимирович", IdNumber=67149, Post="Админ", Salary=40000, Exp=1},
            //    new Employee{ Id=5, Fio="Сидоров Сережа Владимирович", IdNumber=99547, Post="Охрана", Salary=15000, Exp=15},
            //};
            AddEmployeeExtendedCommand = new ExtendedRelayCommand(new AddEmployeeCommandFactory());
            EditEmployeeExtendedCommand = new ExtendedRelayCommand(new EditEmployeeCommandFactory());
            RemoveEmployeeCommand = new CommonRelayCommand(new RemoveEmployeeCommandFactory());
            selectedEmployee = null;
            ReservedCopySelectedEmployee = null;
        }

        //_______________________________

        #region DataClass

        public ConnectionDB db;
        
        private bool isReadOnly;
        private bool isEnableCollection;
        private bool isBusy;
        private Employee selectedEmployee;
        public ObservableCollection<Employee> Employees { get; set; }
        public Employee ReservedCopySelectedEmployee { get; set; }

        #endregion

        //_______________________________

        #region Commands

        // Command add new employee in 
        public ExtendedRelayCommand AddEmployeeExtendedCommand { get; }

        // Command edit employee in Database
        public ExtendedRelayCommand EditEmployeeExtendedCommand { get; }

        // Command remove employee from Database
        public CommonRelayCommand RemoveEmployeeCommand { get; }

        #endregion

        //_______________________________

        #region Propertyes
            
        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                isReadOnly = value;
                OnPropertyChanged("IsReadOnly");
            }
        }

        public bool IsEnableCollection
        {
            get { return isEnableCollection; }
            set
            {
                isEnableCollection = value;
                OnPropertyChanged("IsEnableCollection");
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        #endregion

        //_______________________________

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
