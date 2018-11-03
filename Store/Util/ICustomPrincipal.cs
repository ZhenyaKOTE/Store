
using System.Security.Principal;

namespace Store.Util
{
    public interface ICustomPrincipal : IPrincipal
    {
        string Name { get; set; }
        string Email { get; set; }
    }
}