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

        public KitParametrsEmployees(IGenericRepository db)
        {
            IsReadOnly = true;
            IsEnableCollection = true;
            IsBusy = false;
            this.db = db;
            Employees = DeepCopyCollection<Employee>.CopyToObservableCollection(db.Employees.ToCollection());
            AddEmployeeExtendedCommand = new ExtendedRelayCommand(new AddEmployeeCommandFactory());
            EditEmployeeExtendedCommand = new ExtendedRelayCommand(new EditEmployeeCommandFactory());
            RemoveEmployeeCommand = new CommonRelayCommand(new RemoveEmployeeCommandFactory());
            selectedEmployee = null;
            ReservedCopySelectedEmployee = null;
        }

        //_______________________________

        #region DataClass

        public IGenericRepository db;
        
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
                OnPropertyChanged();
            }
        }

        public bool IsEnableCollection
        {
            get { return isEnableCollection; }
            set
            {
                isEnableCollection = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged();
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
