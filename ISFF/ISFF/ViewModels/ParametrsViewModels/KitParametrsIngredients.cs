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
    public class KitParametrsIngredients : INotifyPropertyChanged
    {
        //_______________________________

        public KitParametrsIngredients()
        {
            IsReadOnly = true;
            IsEnableCollection = true;
            IsBusy = false;
            Ingredients = new ObservableCollection<Ingredient>
            {
                new Ingredient{ Id=1, Name="Огурец солёный", AmountNow=156, AmountUsed=50, Quantily="кг.", Weight=1000, Price=130},
                new Ingredient{ Id=2, Name="Помидор свежий", AmountNow=96, AmountUsed=37, Quantily="кг.", Weight=1000, Price=200},
                new Ingredient{ Id=3, Name="Булка белая", AmountNow=50, AmountUsed=18, Quantily="шт.", Weight=50, Price=10},
                new Ingredient{ Id=4, Name="Котлета", AmountNow=33, AmountUsed=22, Quantily="шт.", Weight=80, Price=30},
                new Ingredient{ Id=5, Name="Кетчуп Чумак", AmountNow=260, AmountUsed=18, Quantily="кг.", Weight=1000, Price=142},
            };
            AddIngredientExtendedCommand = new ExtendedRelayCommand(new AddIngredientCommandFactory());
            EditIngredientExtendedCommand = new ExtendedRelayCommand(new EditIngredientCommandFactory());
            RemoveIngredientCommand = new CommonRelayCommand(new RemoveIngredientCommandFactory());
            SelectedIngredient = null;
        }

        //_______________________________

        #region DataClass

        private bool isReadOnly;
        private bool isEnableCollection;
        private bool isBusy;
        private Ingredient selectedIngredient;
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        #endregion

        //_______________________________

        #region Commands

        // Command add new employee in 
        public ExtendedRelayCommand AddIngredientExtendedCommand { get; }

        // Command edit employee in Database
        public ExtendedRelayCommand EditIngredientExtendedCommand { get; }

        // Command remove employee from Database
        public CommonRelayCommand RemoveIngredientCommand { get; }

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

        public Ingredient SelectedIngredient
        {
            get { return selectedIngredient; }
            set
            {
                selectedIngredient = value;
                OnPropertyChanged("SelectedIngredient");
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
