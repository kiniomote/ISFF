using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ISFF
{
    public class EFDbSetUser : EFDbSet<User>
    {
        public EFDbSetUser(DbSet<User> users) : base(users) { }

        public override User GetItem(User obj)
        {
            return _collectionElement.SingleOrDefault(c => c.Login == obj.Login && c.Password == obj.Password);
        }
    }
}
