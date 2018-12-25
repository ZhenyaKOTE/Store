using DAL.Migrations.Views.Filters;
using Microsoft.AspNet.Identity.EntityFramework;
using Store.DAL.Entities;
using Store.DAL.Entities.StoreEntitiesWithFilters;
using System.Data.Entity;

namespace Store.DAL.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string conectionString) : base(conectionString)
        {
            Database.SetInitializer<ApplicationContext>(new DatabaseInitializerIsExist());

        }


        public ApplicationContext()
            : base("name=StoreContext")
        {
            //Database.SetInitializer<ApplicationContext>(
            //  new DatabaseInitializerIsExist()
            //  );
        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }

        //public DbSet<Product> Products { get; set; }
        //public DbSet<Characteristic> Characteristics { get; set; }
        //public DbSet<Category> Categories { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<FilterName> FilterNames { get; set; }
        public DbSet<FilterValue> FilterValues { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<FilterNameGroup> FilterNameGroups { get; set; }
        public DbSet<VFilterNameGroup> VFilterNameGroups { get; set; }
    }
}
