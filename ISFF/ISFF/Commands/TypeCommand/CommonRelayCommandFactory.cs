using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public abstract class CommonRelayCommandFactory
    {
        public abstract Action<object> Execute();
        public abstract Func<object, bool> CanExecute();
    }
}
