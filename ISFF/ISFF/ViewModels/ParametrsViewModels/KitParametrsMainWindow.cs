using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace ISFF
{
    public class KitParametrsMainWindow : INotifyPropertyChanged
    {
        //_______________________________

        public KitParametrsMainWindow()
        {
            IsReadOnly = true;
            OpenEmployeesWindowCommand = new CommonRelayCommand(new OpenWindowCommandFactory(OpenWindowCommandFactory.WINDOW_EMPLOYEES));
            OpenIngredientsWindowCommand = new CommonRelayCommand(new OpenWindowCommandFactory(OpenWindowCommandFactory.WINDOW_INGREDIENTs));
            OpenProductsWindowCommand = new CommonRelayCommand(new OpenWindowCommandFactory(OpenWindowCommandFactory.WINDOW_PRODUCTS));
            OpenOrdersWindowCommand = new CommonRelayCommand(new OpenWindowCommandFactory(OpenWindowCommandFactory.WINDOW_ORDERS));
            selectedEmployee = null;
        }

        //_______________________________

        #region DataClass

        private bool isReadOnly;
        private Employee selectedEmployee;
        public ObservableCollection<Employee> Employees { get; set; }

        #endregion

        //_______________________________

        #region Commands

        // Open employees window
        public CommonRelayCommand OpenEmployeesWindowCommand { get; }

        // Open ingredients window
        public CommonRelayCommand OpenIngredientsWindowCommand { get; }

        // Open products window
        public CommonRelayCommand OpenProductsWindowCommand { get; }

        // Open orders window
        public CommonRelayCommand OpenOrdersWindowCommand { get; }

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
