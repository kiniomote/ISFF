using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ISFF
{
    public static class CheckUserAccessService
    {
        public static readonly User DefaultUser = new User()
        {
            Role = User.UserRole.Customer,
            Login = "user",
            Password = ""
        };

        public static User User { get; set; }

        public static void Authorization(string login, string password)
        {
            using(ConnectionDB db = new ConnectionDB())
            {
                db.Users.Load();
                User = db.Users.SingleOrDefault(user => user.Login == login && user.Password == password);
                if (User == null)
                    AuthorizationDefault();
            }
        }

        public static void AuthorizationDefault()
        {
            User = DefaultUser;
        }

        public static bool IsAdministrator()
        {
            return User.Role == User.UserRole.Administrator;
        }

        public static bool IsEmployee()
        {
            return User.Role == User.UserRole.Employee;
        }

        public static bool IsCustomer()
        {
            return User.Role == User.UserRole.Customer;
        }
    }
}
