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
        //_______________________________

        #region Conctruct

        public EmployeesViewModel()
        {
            KitParametrsEmployees = new KitParametrsEmployees();
            AddEmployeeExtendedCommand = new ExtendedRelayCommand(new AddEmployeeCommandFactory());
            EditEmployeeExtendedCommand = new ExtendedRelayCommand(new EditEmployeeCommandFactory());
            RemoveEmployeeCommand = new CommonRelayCommand(new RemoveEmployeesCommandFactory());
        }

        #endregion

        //_______________________________

        #region DataClass

        public KitParametrsEmployees KitParametrsEmployees { get; set; }

        #endregion

        //_______________________________

        #region Commands

        // Command add new employee in 
        public ExtendedRelayCommand AddEmployeeExtendedCommand { get; }

        // Command edit employee in Database
        public ExtendedRelayCommand EditEmployeeExtendedCommand { get; }

        // Command edit employee in Database
        public CommonRelayCommand RemoveEmployeeCommand { get; }

        #endregion

        //_______________________________
    }
}
