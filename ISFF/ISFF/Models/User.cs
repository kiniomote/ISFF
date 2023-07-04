using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public class User : IEntity
    {
        public enum UserRole : byte
        {
            Administrator = 1,
            Employee = 2,
            Customer = 3
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
