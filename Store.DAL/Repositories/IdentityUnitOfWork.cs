using Microsoft.AspNet.Identity.EntityFramework;
using Store.DAL.Entities;
using Store.DAL.EntityFramework;
using Store.DAL.Identity;
using Store.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext DBContext;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;

        public UnitOfWork(string connectionString)
        {
            DBContext = new ApplicationContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(DBContext));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(DBContext));
            clientManager = new ClientManager(DBContext);
        }

        public ApplicationUserManager UserManager => userManager;

        public IClientManager ClientManager => clientManager;

        public ApplicationRoleManager RoleManager => roleManager;

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                }
                this.disposed = true;
            }
        }
        #endregion

        public async Task SaveAsync()
        {
            await DBContext.SaveChangesAsync();
        }
    }
}
