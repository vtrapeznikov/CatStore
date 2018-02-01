using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CatStore.DAL.Entities;

namespace CatStore.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
                    : base(store)
        { }
    }
}
