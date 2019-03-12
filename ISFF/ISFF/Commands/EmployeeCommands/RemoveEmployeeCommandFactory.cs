using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class RemoveEmployeeCommandFactory : CommonRelayCommandFactory
    {
        public override Action<object> Execute()
        {
            return param =>
            {
                KitParametrsEmployees kitParametrs = param as KitParametrsEmployees;
                Record record = new Record(kitParametrs.SelectedEmployee, Record.Action.Remove);
                record.WriteRecordToFile(new JsonLogerService<Record>(JsonLogerService<Record>.FILE_NAME_EMPLOYEE));
                kitParametrs.db.Employees.Remove(kitParametrs.SelectedEmployee);
                kitParametrs.Employees.Remove(kitParametrs.SelectedEmployee);
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
                if (param is KitParametrsEmployees kitParametrs && kitParametrs.SelectedEmployee != null)
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
