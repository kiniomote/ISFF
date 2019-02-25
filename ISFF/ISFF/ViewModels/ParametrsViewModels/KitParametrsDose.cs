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
    public class KitParametrsDose : INotifyPropertyChanged
    {
        public KitParametrsDose(List<INameable> items, INameable selected, int count)
        {
            this.items = new ObservableCollection<INameable>();
            foreach(INameable item in items)
            {
                this.items.Add(item);
            }
            selectedItem = selected;
            countItems = count;
        }

        private ObservableCollection<INameable> items;
        private INameable selectedItem;
        private int countItems;

        public ObservableCollection<INameable> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        public INameable SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public int CountItems
        {
            get { return countItems; }
            set
            {
                countItems = value;
                OnPropertyChanged("CountItems");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
