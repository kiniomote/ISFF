using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ISFF
{
    public class CommonRelayCommand : ICommand
    {
        private Action<object> execute_;
        private Func<object, bool> canExecute_;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public CommonRelayCommand(CommonRelayCommandFactory factory)
        {
            execute_ = factory.Execute();
            canExecute_ = factory.CanExecute();
        }

        public bool CanExecute(object parameter)
        {
            return canExecute_ == null || canExecute_(parameter);
        }

        public void Execute(object parameter)
        {
            execute_(parameter);
        }
    }
}
