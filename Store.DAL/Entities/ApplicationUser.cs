using Microsoft.AspNet.Identity.EntityFramework;

namespace Store.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        //public string Name { get; set; }
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
