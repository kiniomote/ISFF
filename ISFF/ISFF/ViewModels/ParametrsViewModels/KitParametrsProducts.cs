using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
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
            Products = DeepCopyCollection<Product>.CopyToObservableCollectionFromDb(db.Products);
            DoseIngredients = new ObservableCollection<DoseIngredient>();
            IdElementsForRemove = new List<int>();
            AddProductExtendedCommand = new ExtendedRelayCommand(new AddProductCommandFactory());
            EditProductExtendedCommand = new ExtendedRelayCommand(new EditProductCommandFactory());
            RemoveProductCommand = new CommonRelayCommand(new RemoveProductCommandFactory());
            AddDoseIngredientCommand = new CommonRelayCommand(new AddDoseIngredientCommandFactory());
            EditDoseIngredientCommand = new CommonRelayCommand(new EditDoseIngredientCommandFactory());
            RemoveDoseIngredientCommand = new CommonRelayCommand(new RemoveDoseIngredientCommandFactory());
            LoadImageCommand = new CommonRelayCommand(new LoadImageCommandFactory());
            RemoveImageCommand = new CommonRelayCommand(new RemoveImageCommandFactory());
            SelectedProduct = null;
            SelectedDoseIngredient = null;
            Image = ConverterByteImage.ToBitmapImage(Properties.Resources.None_image);
        }

        //_______________________________

        #region DataClass

        public ConnectionDB db;

        private bool isReadOnly;
        private bool isEnableCollection;
        private bool isBusy;
        private Product selectedProduct;
        private DoseIngredient selectedDoseIngredient;
        private BitmapImage image;
        public Product ReservedCopySelectedProduct { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<DoseIngredient> DoseIngredients { get; set; }
        public List<int> IdElementsForRemove { get; set; }

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

        // Command load image product from file
        public CommonRelayCommand LoadImageCommand { get; }

        // Command remove image selected product
        public CommonRelayCommand RemoveImageCommand { get; }
        
        #endregion

        //_______________________________

        #region Properties

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

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                if (selectedProduct == null)
                    return;
                DeepCopyCollection<DoseIngredient>.CopyElementsFromCollection(DoseIngredients, selectedProduct.DoseIngredients);
                OnPropertyChanged();
                Image = ConverterByteImage.ByteToImage(selectedProduct.Image);
            }
        }

        public DoseIngredient SelectedDoseIngredient
        {
            get { return selectedDoseIngredient; }
            set
            {
                selectedDoseIngredient = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage Image
        {
            get { return image; }
            set
            {
                image = value;
                if (value != null && SelectedProduct != null)
                    SelectedProduct.Image = ConverterByteImage.ImageToByte(value);
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
