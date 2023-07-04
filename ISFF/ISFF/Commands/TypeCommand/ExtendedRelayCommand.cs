using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace ISFF
{
    public class ExtendedRelayCommand : ICommand, INotifyPropertyChanged
    {
        #region Constants

        public const bool STATE_NORMAL = true;
        public const bool STATE_ALTERNATIVE = false;

        #endregion

        private string textCommand;
        private bool state;

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
            state = factory.State();
            textCommand = factory.TextCommand();
        }

        public bool CanExecute(object parameter)
        {
            return canExecute_ == null || canExecute_(parameter);
        }

        public void Execute(object parameter)
        {
            if(state == STATE_NORMAL)
                execute_(parameter);
            else
                alternativeExecute_(parameter);
        }

        //_______________________________

        public string TextCommand
        {
            get { return textCommand; }
            set
            {
                textCommand = value;
                OnPropertyChanged("TextCommand");
            }
        }
        public bool State
        {
            get { return state; }
            set
            {
                state = value;
                OnPropertyChanged("State");
            }
        }

        //_______________________________

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
