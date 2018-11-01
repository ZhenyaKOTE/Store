using Microsoft.AspNet.Identity.EntityFramework;
using Store.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.EntityFramework
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(string conectionString) : base(conectionString) { }
        public DbSet<ClientProfile> ClientProfiles { get; set; }

    }
}
