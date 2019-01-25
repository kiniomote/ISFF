using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class RemoveEmployeesCommandFactory : CommonRelayCommandFactory
    {
        public override Action<object> Execute()
        {
            return param =>
            {
                KitParametrsEmployees kitParametrs = param as KitParametrsEmployees;
                if (kitParametrs != null)
                    kitParametrs.IsReadOnly = false;
            };
        }
        public override Func<object, bool> CanExecute()
        {
            return null;
        }
    }
}
