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
    public class LogViewModel
    {
        public LogViewModel(ILogable element)
        {
            string fileName = string.Empty;
            if (element.GetType() == typeof(Employee))
                fileName = JsonLogerService<object>.FILE_NAME_EMPLOYEE;
            if (element.GetType() == typeof(Ingredient))
                fileName = JsonLogerService<object>.FILE_NAME_INGREDIENT;
            if (element.GetType() == typeof(Product))
                fileName = JsonLogerService<object>.FILE_NAME_PRODUCT;
            if (element.GetType() == typeof(Order))
                fileName = JsonLogerService<object>.FILE_NAME_ORDER;
            JsonLogerService<Record> jsonLoger = new JsonLogerService<Record>(fileName);
            List<Record> jsonRead = jsonLoger.ReadFromFile();
            if (jsonRead != null)
                Records = DeepCopyCollection<Record>.CopyToObservableCollection(jsonRead);
            SelectedRecord = null;
        }

        public ObservableCollection<Record> Records { get; set; }
        public Record SelectedRecord { get; set; }
    }
}
