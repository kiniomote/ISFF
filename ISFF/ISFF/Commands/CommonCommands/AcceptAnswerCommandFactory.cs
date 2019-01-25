using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ISFF
{
    public class AcceptAnswerCommandFactory : CommonRelayCommandFactory
    {
        public override Action<object> Execute()
        {
            return param => 
            {
                Window dialogWindow = param as Window;
                dialogWindow.DialogResult = true;
            };
        }

        public override Func<object, bool> CanExecute()
        {
            return null;
        }
    }
}
