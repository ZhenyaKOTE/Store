using Store.DAL.Entities;
using System;


namespace Store.DAL.Interfaces
{
    public interface IClientManager: IDisposable
    {
        void Create(ClientProfile item);
    }
}
