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
    /// Логика взаимодействия для WindowIngredients.xaml
    /// </summary>
    public partial class WindowIngredients : Window
    {
        public WindowIngredients(IGenericRepository db)
        {
            InitializeComponent();
            DataContext = new IngredientsViewModel(db);
        }
    }
}
