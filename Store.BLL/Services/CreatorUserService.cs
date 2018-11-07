using Store.BLL.Interfaces;
using Store.DAL.Repositories;
using System.Diagnostics;

namespace Store.BLL.Services
{
    public class ServiceCreator : Creator
    {
        public IService CreateUserService(string connection)
        {
            //Debug.Write(connection + "\n\n\n\n\n\n\n\n");
            return new UserService(new UnitOfWork(connection));
        }

        
    }
}
