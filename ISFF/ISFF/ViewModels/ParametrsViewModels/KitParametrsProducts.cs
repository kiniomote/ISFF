using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Data.Entity;

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
            db = new ConnectionDB();
            db.Ingredients.Load();
            db.Products.Load();
            db.DoseIngredients.Load();
            Products = new ObservableCollection<Product>();
            foreach (Product product in db.Products)
            {
                Products.Add(product);
            }
            //Products = new ObservableCollection<Product>
            //{
            //    new Product{ Id=1, Name="Картофель фри", TimeCook=180, Price=60, Weight=80},
            //    new Product{ Id=2, Name="Гамбургер", TimeCook=180, Price=90, Weight=120},
            //    new Product{ Id=3, Name="Кола 50 мл", TimeCook=20, Price=50, Weight=200},
            //    new Product{ Id=4, Name="Рожок 60 гр", TimeCook=15, Price=60, Weight=35},
            //    new Product{ Id=5, Name="Пирожок", TimeCook=0, Price=40, Weight=25},
            //};
            AddProductExtendedCommand = new ExtendedRelayCommand(new AddProductCommandFactory());
            EditProductExtendedCommand = new ExtendedRelayCommand(new EditProductCommandFactory());
            RemoveProductCommand = new CommonRelayCommand(new RemoveProductCommandFactory());
            AddDoseIngredientCommand = new CommonRelayCommand(new AddDoseIngredientCommandFactory());
            EditDoseIngredientCommand = new CommonRelayCommand(new EditDoseIngredientCommandFactory());
            RemoveDoseIngredientCommand = new CommonRelayCommand(new RemoveDoseIngredientCommandFactory());
            SelectedProduct = null;
            SelectedDoseIngredient = null;
        }

        //_______________________________

        #region DataClass

        public ConnectionDB db;

        private bool isReadOnly;
        private bool isEnableCollection;
        private bool isBusy;
        private Product selectedProduct;
        private DoseIngredient selectedDoseIngredient;
        public Product ReservedCopySelectedProduct { get; set; }
        public ObservableCollection<Product> Products { get; set; }

        #endregion

        //_______________________________

        #region Commands
        
        // Command add new employee in Database
        public ExtendedRelayCommand AddProductExtendedCommand { get; }

        // Command edit employee in Database
        public ExtendedRelayCommand EditProductExtendedCommand { get; }

        // Command remove employee from Database
        public CommonRelayCommand RemoveProductCommand { get; }

        // Command add dose ingredient in product
        public CommonRelayCommand AddDoseIngredientCommand { get; }

        // Command edit dose ingredient in product
        public CommonRelayCommand EditDoseIngredientCommand { get; }

        // Command remove dose ingredient in product
        public CommonRelayCommand RemoveDoseIngredientCommand { get; }
        
        #endregion

        //_______________________________

        #region Properties

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

        public DoseIngredient SelectedDoseIngredient
        {
            get { return selectedDoseIngredient; }
            set
            {
                selectedDoseIngredient = value;
                OnPropertyChanged("SelectedDoseIngredient");
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
