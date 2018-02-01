using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using CatStore.DAL.Entities;

namespace CatStore.DAL.EF
{
    public class CatContext : IdentityDbContext<ApplicationUser>
    {
        public CatContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Cat> Cats { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
