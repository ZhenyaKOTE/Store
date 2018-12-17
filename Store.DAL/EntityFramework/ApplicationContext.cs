using DAL.Migrations.Views.Filters;
using Microsoft.AspNet.Identity.EntityFramework;
using Store.DAL.Entities;
using Store.DAL.Entities.StoreEntitiesWithFilters;
using Store.DAL.EntityFramework.DAL.Entities;
//using Store.DAL.Entities.StoreEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.EntityFramework
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        //public ApplicationContext(string conectionString) : base(conectionString)
        //{
        //    //Database.SetInitializer<ApplicationContext>(new ContextInitializer());
        //}

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
