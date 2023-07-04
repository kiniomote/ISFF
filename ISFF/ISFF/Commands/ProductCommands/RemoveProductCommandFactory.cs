using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class RemoveProductCommandFactory : CommonRelayCommandFactory
    {
        public override Action<object> Execute()
        {
            return param =>
            {
                KitParametrsProducts kitParametrs = param as KitParametrsProducts;
                Record record = new Record(kitParametrs.SelectedProduct, Record.Action.Remove);
                record.WriteRecordToFile(new JsonLogerService<Record>(JsonLogerService<Record>.FILE_NAME_PRODUCT));
                kitParametrs.DoseIngredients.Clear();
                kitParametrs.db.Products.Remove(kitParametrs.SelectedProduct);
                kitParametrs.Products.Remove(kitParametrs.SelectedProduct);
                kitParametrs.db.SaveChanges();
            };
        }
        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
                if (CheckUserAccessService.IsNotAdministrator())
                    return false;
                bool enable = true;
                if (param is KitParametrsProducts kitParametrs && kitParametrs.SelectedProduct != null)
                {
                    if (kitParametrs.IsBusy)
                        enable = false;
                }
                else
                    enable = false;
                return enable;
            };
        }
    }
}
