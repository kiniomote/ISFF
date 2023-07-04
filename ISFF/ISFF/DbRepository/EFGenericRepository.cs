using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ISFF
{
    public class EFGenericRepository : IGenericRepository
    {
        private ConnectionDB db;

        public IDbSet<Employee> Employees { get; set ; }
        public IDbSet<Ingredient> Ingredients { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<DoseIngredient> DoseIngredients { get; set; }
        public IDbSet<DoseProduct> DoseProducts { get; set; }

        public EFGenericRepository()
        {
            db = new ConnectionDB();
            db.Employees.Load();
            db.Ingredients.Load();
            db.Products.Load();
            db.Orders.Load();
            db.Users.Load();
            db.DoseIngredients.Load();
            db.DoseProducts.Load();

            Employees = new EFDbSet<Employee>(db.Employees);
            Ingredients = new EFDbSet<Ingredient>(db.Ingredients);
            Products = new EFDbSet<Product>(db.Products);
            Orders = new EFDbSet<Order>(db.Orders);
            DoseIngredients = new EFDbSet<DoseIngredient>(db.DoseIngredients);
            DoseProducts = new EFDbSet<DoseProduct>(db.DoseProducts);
            Users = new EFDbSetUser(db.Users);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
