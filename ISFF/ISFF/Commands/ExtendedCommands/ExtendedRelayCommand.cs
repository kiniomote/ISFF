using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ISFF
{
    public class ExtendedRelayCommand : ICommand
    {
        public const bool STATE_NORMAL = true;
        public const bool STATE_ACCEPT = false;

        public string TextCommand { get; set; }
        public bool State { get; set; }

        private Action<object> execute_;
        private Action<object> alternativeExecute_;
        private Func<object, bool> canExecute_;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ExtendedRelayCommand(ExtendedRelayCommandFactory factory)
        {
            execute_ = factory.Execute();
            alternativeExecute_ = factory.AlternativeExecute();
            canExecute_ = factory.CanExecute();
            State = factory.State();
            TextCommand = factory.TextCommand();
        }

        public bool CanExecute(object parameter)
        {
            return canExecute_ == null || canExecute_(parameter);
        }

        public void Execute(object parameter)
        {
            if(State)
                execute_(parameter);
            else
                alternativeExecute_(parameter);
        }
    }
}
