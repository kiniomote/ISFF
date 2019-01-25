using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public abstract class ExtendedRelayCommandFactory
    {
        public abstract string TextCommand();
        public abstract bool State();
        public abstract Action<object> Execute();
        public abstract Action<object> AlternativeExecute();
        public abstract Func<object, bool> CanExecute();
    }
}
