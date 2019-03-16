using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace ISFF
{
    public class KitParametrsOrders : INotifyPropertyChanged
    {
        //_______________________________

        public KitParametrsOrders(IGenericRepository db)
        {
            IsReadOnly = true;
            ColorStateOrder = new SolidColorBrush(Colors.Transparent);
            this.db = db;
            LoadOrders();
            ChangeStateOrderCommand = new CommonRelayCommand(new ChangeStateOrderCommandFactory());
            SelectedOrder = null;
            SelectedDoseProduct = null;

            currentTimer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1),
                IsEnabled = true
            };
            currentTimer.Tick += (o, e) => 
            {
                CurrentTime = DateTime.Now.ToLongTimeString();
            };
            currentTimer.Start();
        }

        //_______________________________

        #region DataClass

        public IGenericRepository db;
        private DispatcherTimer currentTimer;
        
        private bool isReadOnly;
        private string currentTime;
        private SolidColorBrush colorStateOrder;
        private Order selectedOrder;
        private DoseProduct selectedDoseProduct;
        public ObservableCollection<Order> ReadyOrders { get; set; }
        public ObservableCollection<Order> FinishedOrders { get; set; }

        #endregion

        //_______________________________

        #region Commands

        // Command change state order
        public CommonRelayCommand ChangeStateOrderCommand { get; }
        
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

        public string CurrentTime
        {
            get { return currentTime; }
            set
            {
                currentTime = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush ColorStateOrder
        {
            get { return colorStateOrder; }
            set
            {
                colorStateOrder = value;
                OnPropertyChanged();
            }
        }

        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set
            {
                selectedOrder = value;
                if (selectedOrder != null)
                    ColorStateOrder = selectedOrder.Ready ? new SolidColorBrush(Order.COLOR_READY) : new SolidColorBrush(Order.COLOR_NOT_READY);
                OnPropertyChanged();
            }
        }

        public DoseProduct SelectedDoseProduct
        {
            get { return selectedDoseProduct; }
            set
            {
                selectedDoseProduct = value;
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

        public void LoadOrders()
        {
            FinishedOrders = new ObservableCollection<Order>();
            ReadyOrders = new ObservableCollection<Order>();
            foreach(Order order in db.Orders.ToCollection())
            {
                if (order.Ready)
                {
                    FinishedOrders.Add(order);
                }
                else
                {
                    ReadyOrders.Add(order);
                }
            }
        }
    }
}
