using Microsoft.AspNet.Identity.EntityFramework;

namespace Store.DAL.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }

        public string Description { get; set; }
    }
}
