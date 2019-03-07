using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ISFF
{
    /// <summary>
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class ResponseDialogWindow : Window
    {
        public int Answer { get; set; }

        public ResponseDialogWindow(string text_message)
        {
            InitializeComponent();
            DataContext = new DialogViewModel(text_message);
            Answer = DialogViewModel.ANSWER_CANCEL;
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            Answer = DialogViewModel.ANSWER_NO;
            DialogResult = false;
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            Answer = DialogViewModel.ANSWER_YES;
            DialogResult = true;
        }
    }
}
