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
                if (!kitParametrs.SelectedOrder.TryCompleteOrder())
                {
                    return;
                }
                kitParametrs.db.SaveChanges();
                kitParametrs.SelectedOrder.Ready = true;
                kitParametrs.db.Orders.Update(kitParametrs.SelectedOrder);
                kitParametrs.FinishedOrders.Add(kitParametrs.SelectedOrder);
                kitParametrs.ReadyOrders.Remove(kitParametrs.SelectedOrder);
                kitParametrs.SelectedOrder = kitParametrs.FinishedOrders.Last();
                kitParametrs.db.SaveChanges();
                Record record = new Record(kitParametrs.SelectedOrder, Record.Action.Ready);
                record.WriteRecordToFile(new JsonLogerService<Record>(JsonLogerService<Record>.FILE_NAME_ORDER));
            };
        }

        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
                if (CheckUserAccessService.IsCustomer())
                    return false;
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
