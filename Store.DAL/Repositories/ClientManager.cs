using Microsoft.AspNet.Identity.EntityFramework;
using Store.DAL.Entities;
using Store.DAL.Context;
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

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
