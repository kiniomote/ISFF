using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class LogShowCommandFactory : CommonRelayCommandFactory
    {
        public override Action<object> Execute()
        {
            return param =>
            {
                if(param is ILogable)
                    DialogWindowService.OpenLogWindow(param as ILogable);
            };
        }

        public override Func<object, bool> CanExecute()
        {
            return param =>
            {
                if (CheckUserAccessService.IsCustomer())
                    return false;
                else
                    return true;
            };
        }
    }
}
