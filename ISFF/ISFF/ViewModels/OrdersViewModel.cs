using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class OrdersViewModel
    {
        //_______________________________

        #region Conctruct

        public OrdersViewModel()
        {
            KitParametrsOrders = new KitParametrsOrders();
        }

        #endregion

        //_______________________________

        #region DataClass

        public KitParametrsOrders KitParametrsOrders { get; set; }

        #endregion

        //_______________________________



        //_______________________________
    }
}
