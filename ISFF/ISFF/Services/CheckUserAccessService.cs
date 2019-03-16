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
            using(IGenericRepository db = new EFGenericRepository())
            {
                User user = new User() { Login = login, Password = password };
                User = db.Users.GetItem(user);
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

        public static bool IsNotAdministrator()
        {
            return User.Role != User.UserRole.Administrator;
        }

        public static bool IsNotEmployee()
        {
            return User.Role != User.UserRole.Employee;
        }
        
        public static bool IsNotCustomer()
        {
            return User.Role != User.UserRole.Customer;
        }

        public static bool CanOpenWindow(int window)
        {
            if (IsNotCustomer())
                return true;
            if (IsCustomer())
            {
                switch (window)
                {
                    case OpenWindowCommandFactory.WINDOW_EMPLOYEES:
                        return false;
                    case OpenWindowCommandFactory.WINDOW_INGREDIENTS:
                        return false;
                    case OpenWindowCommandFactory.WINDOW_PRODUCTS:
                        return true;
                    case OpenWindowCommandFactory.WINDOW_ORDERS:
                        return false;
                }
            }
            return false;
        }
    }
}
