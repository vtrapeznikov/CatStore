using CatStore.DAL.Interfaces;
using CatStore.DAL.EF;
using CatStore.DAL.Entities;
using CatStore.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace CatStore.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private CatContext Db;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;
        private CatRepository catRepository;
        private OrderRepository orderRepository;

        public UnitOfWork(string connectionString)
        {
            Db = new CatContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(Db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(Db));
            clientManager = new ClientManager(Db);
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public IRepository<Cat> Cats
        {
            get
            {
                if (catRepository == null)
                    catRepository = new CatRepository(Db);
                return catRepository;
            }
        }

        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(Db);
                return orderRepository;
            }
        }

        public async Task SaveAsync()
        {
            await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
