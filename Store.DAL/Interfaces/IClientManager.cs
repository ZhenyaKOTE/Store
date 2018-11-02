using Store.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace Store.DAL.Interfaces
{
    public interface IClientManager: IDisposable
    {
        void Create(ClientProfile item);
        Task<string> GetUserNameByEmail(string Email);
    }
}
