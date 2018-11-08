using Store.DAL.Entities.StoreEntities;
using System;

namespace Store.DAL.Interfaces
{
    public interface IStoreManager: IDisposable
    {
        void Create(Product item);
    }
}