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
    public class KitParametrsProducts : INotifyPropertyChanged
    {
        //_______________________________

        public KitParametrsProducts()
        {
            IsReadOnly = true;
            IsEnableCollection = true;
            IsBusy = false;
            Products = new ObservableCollection<Product>
            {
                new Product{ Id=1, Name="Картофель фри", TimeCook=180, Price=60, Weight=80},
                new Product{ Id=2, Name="Гамбургер", TimeCook=180, Price=90, Weight=120},
                new Product{ Id=3, Name="Кола 50 мл", TimeCook=20, Price=50, Weight=200},
                new Product{ Id=4, Name="Рожок 60 гр", TimeCook=15, Price=60, Weight=35},
                new Product{ Id=5, Name="Пирожок", TimeCook=0, Price=40, Weight=25},
            };
            AddProductExtendedCommand = new ExtendedRelayCommand(new AddProductCommandFactory());
            EditProductExtendedCommand = new ExtendedRelayCommand(new EditProductCommandFactory());
            RemoveProductCommand = new CommonRelayCommand(new RemoveProductCommandFactory());
            SelectedProduct = null;
        }

        //_______________________________

        #region DataClass

        private bool isReadOnly;
        private bool isEnableCollection;
        private bool isBusy;
        private Product selectedProduct;
        public ObservableCollection<Product> Products { get; set; }

        #endregion

        //_______________________________

        #region Commands

        // Command add new employee in 
        public ExtendedRelayCommand AddProductExtendedCommand { get; }

        // Command edit employee in Database
        public ExtendedRelayCommand EditProductExtendedCommand { get; }

        // Command remove employee from Database
        public CommonRelayCommand RemoveProductCommand { get; }

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

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
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
