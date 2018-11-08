using Store.DAL.Identity;
using System;
using System.Threading.Tasks;

namespace Store.DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IStoreManager StoreManager { get; }
        Task SaveAsync();
    }
}
