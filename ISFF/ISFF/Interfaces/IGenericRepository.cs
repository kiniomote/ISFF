using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISFF
{
    public interface IGenericRepository : IDisposable
    {
        IDbSet<Employee> Employees { get; set; }
        IDbSet<Ingredient> Ingredients { get; set; }
        IDbSet<Product> Products { get; set; }
        IDbSet<Order> Orders { get; set; }
        IDbSet<User> Users { get; set; }
        IDbSet<DoseIngredient> DoseIngredients { get; set; }
        IDbSet<DoseProduct> DoseProducts { get; set; }
        void SaveChanges();
    }
}
