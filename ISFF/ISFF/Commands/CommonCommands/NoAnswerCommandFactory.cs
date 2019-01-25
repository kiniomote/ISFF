using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ISFF
{
    public class NoAnswerCommandFactory : CommonRelayCommandFactory
    {
        public override Action<object> Execute()
        {
            return param =>
            {
                KitParametrsDialog kitParametrs = param as KitParametrsDialog;
                kitParametrs.Answer = DialogViewModel.ANSWER_NO;
            };
        }

        public override Func<object, bool> CanExecute()
        {
            return null;
        }
    }
}
