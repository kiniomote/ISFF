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
    public class KitParametrsDose : INotifyPropertyChanged, IDataErrorInfo
    {
        public KitParametrsDose(List<INameable> items, INameable selected, int count)
        {
            this.items = DeepCopyCollection<INameable>.CopyToObservableCollection(items);
            selectedItem = selected;
            countItems = count;
        }

        private ObservableCollection<INameable> items;
        private INameable selectedItem;
        private int countItems;

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "CountItems":
                        if (CountItems <= 0)
                            error = "Количество не может быть меньше одного";
                        break;
                }
                return error;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public ObservableCollection<INameable> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }

        public INameable SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        public int CountItems
        {
            get { return countItems; }
            set
            {
                countItems = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
