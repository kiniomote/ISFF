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
    /// Логика взаимодействия для ChoseDoseWindow.xaml
    /// </summary>
    public partial class ChoseDoseWindow : Window
    {
        public ChoseDoseWindow(List<INameable> items, INameable selected = null, int count = 0)
        {
            InitializeComponent();
            DataContext = new DoseViewModel(items, selected, count);
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
