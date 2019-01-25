using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace ISFF
{
    public class KitParametrsDialog : INotifyPropertyChanged
    {
        public KitParametrsDialog(int default_answer, string text_message)
        {
            answer = default_answer;
            textMessage = text_message;
        }

        private string textMessage;
        private int answer;

        public string TextMessage
        {
            get { return textMessage; }
            set
            {
                textMessage = value;
                OnPropertyChanged("TextMessage");
            }
        }

        public int Answer
        {
            get { return answer; }
            set
            {
                answer = value;
                OnPropertyChanged("Answer");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
