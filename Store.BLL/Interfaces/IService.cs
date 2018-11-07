using Store.BLL.DTO;
using Store.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Interfaces
{
    public interface IService: IDisposable
    {

        Task<OperationDetails> CreateAsync(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);       
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        string GetUserNameByEmail(string Email);
        Task<string[]> GetRoles(string id);


    }
}
