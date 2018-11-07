using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Interfaces
{
    public interface Creator
    {
        IService CreateUserService(string connection);
        //IRoleService CreateRoleService(string connection);
    }
}
