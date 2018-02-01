using Microsoft.AspNet.Identity.EntityFramework;

namespace CatStore.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
