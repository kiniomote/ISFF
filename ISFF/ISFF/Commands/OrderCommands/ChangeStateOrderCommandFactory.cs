using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class ChangeStateOrderCommandFactory : CommonRelayCommandFactory
    {
        public override Action<object> Execute()
        {
            return param =>
            {
                KitParametrsOrders kitParametrs = param as KitParametrsOrders;
                kitParametrs.SelectedOrder.Ready = true;
                Order searchOrder = kitParametrs.db.Orders.SingleOrDefault(c => c.Id == kitParametrs.SelectedOrder.Id);
                searchOrder.Ready = kitParametrs.SelectedOrder.Ready;
                kitParametrs.FinishedOrders.Add(kitParametrs.SelectedOrder);
                kitParametrs.ReadyOrders.Remove(kitParametrs.SelectedOrder);
                kitParametrs.SelectedOrder = kitParametrs.FinishedOrders.Last();
                kitParametrs.db.SaveChanges();
            };
        }

        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
                bool enable = true;
                if (param is KitParametrsOrders kitParametrs && (kitParametrs.SelectedOrder == null || kitParametrs.SelectedOrder.Ready == true))
                {
                    enable = false;
                }
                return enable;
            };
        }
    }
}
