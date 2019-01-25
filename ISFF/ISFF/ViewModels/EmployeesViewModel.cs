using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ISFF
{
    public class EmployeesViewModel
    {
        // Construct
        public EmployeesViewModel()
        {
            KitParametrsEmployees = new KitParametrsEmployees();
            EditEmployeeExtendedCommand = new ExtendedRelayCommand(new EditEmployeeCommandFactory());
            AddEmployeeExtendedCommand = new ExtendedRelayCommand(new AddEmployeeCommandFactory());
            RemoveEmployeeCommand = new CommonRelayCommand(new RemoveEmployeesCommandFactory());
        }

        // Data class
        public KitParametrsEmployees KitParametrsEmployees { get; set; }

        // Commands
        //_______________________________

        // Command add new employee in 
        public ExtendedRelayCommand AddEmployeeExtendedCommand { get; }

        // Command edit employee in Database
        public ExtendedRelayCommand EditEmployeeExtendedCommand { get; }

        // Command edit employee in Database
        public CommonRelayCommand RemoveEmployeeCommand { get; }

        //_______________________________
    }
}
