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
            CheckUserAccessService.AuthorizationDefault();
            IsEnable = true;
            OpenEmployeesWindowCommand = new CommonRelayCommand(new OpenWindowCommandFactory(OpenWindowCommandFactory.WINDOW_EMPLOYEES));
            OpenIngredientsWindowCommand = new CommonRelayCommand(new OpenWindowCommandFactory(OpenWindowCommandFactory.WINDOW_INGREDIENTS));
            OpenProductsWindowCommand = new CommonRelayCommand(new OpenWindowCommandFactory(OpenWindowCommandFactory.WINDOW_PRODUCTS));
            OpenOrdersWindowCommand = new CommonRelayCommand(new OpenWindowCommandFactory(OpenWindowCommandFactory.WINDOW_ORDERS));
            AuthorizationCommand = new ExtendedRelayCommand(new AuthorizationCommandFactory());
            LogShowCommand = new CommonRelayCommand(new LogShowCommandFactory());
            Login = "user";
            Employee = new Employee();
            Ingredient = new Ingredient();
            Product = new Product();
            Order = new Order();
        }

        //_______________________________

        #region DataClass

        private bool isEnable;
        private string login;
        private string password;

        public Employee Employee { get; }
        public Ingredient Ingredient { get; }
        public Product Product { get; }
        public Order Order { get; }

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

        // Authorization
        public ExtendedRelayCommand AuthorizationCommand { get; }

        // Show log
        public CommonRelayCommand LogShowCommand { get; }

        #endregion

        //_______________________________

        #region Propertyes

        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                OnPropertyChanged();
            }
        }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
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
