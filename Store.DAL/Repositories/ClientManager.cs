using Microsoft.AspNet.Identity.EntityFramework;
using Store.DAL.Entities;
using Store.DAL.EntityFramework;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        private readonly ApplicationContext DbContext;
        public ClientManager(ApplicationContext _IdentityDbContext)
        {
            DbContext = _IdentityDbContext;
        }
        public void Create(ClientProfile item)
        {
            DbContext.ClientProfiles.Add(item);
            DbContext.SaveChanges();
        }

        public string GetUserNameByEmail(string Email)
        {
             var User = DbContext.ClientProfiles.FirstOrDefault(x => x.ApplicationUser.Email == Email);
             return User.Name;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
