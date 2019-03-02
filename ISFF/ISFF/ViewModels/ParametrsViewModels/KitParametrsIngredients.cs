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
    public class KitParametrsIngredients : INotifyPropertyChanged
    {
        //_______________________________

        public KitParametrsIngredients()
        {
            IsReadOnly = true;
            IsEnableCollection = true;
            IsBusy = false;
            db = new ConnectionDB();
            db.Ingredients.Load();
            Ingredients = DeepCopyCollection<Ingredient>.CopyToObservableCollectionFromDb(db.Ingredients);
            AddIngredientExtendedCommand = new ExtendedRelayCommand(new AddIngredientCommandFactory());
            EditIngredientExtendedCommand = new ExtendedRelayCommand(new EditIngredientCommandFactory());
            RemoveIngredientCommand = new CommonRelayCommand(new RemoveIngredientCommandFactory());
            SelectedIngredient = null;
        }

        //_______________________________

        #region DataClass

        public ConnectionDB db;

        private bool isReadOnly;
        private bool isEnableCollection;
        private bool isBusy;
        private Ingredient selectedIngredient;
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public Ingredient ReservedCopySelectedIngredient { get; set; }

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

        public Ingredient SelectedIngredient
        {
            get { return selectedIngredient; }
            set
            {
                selectedIngredient = value;
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
