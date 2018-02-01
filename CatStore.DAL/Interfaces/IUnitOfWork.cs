using System;
using CatStore.DAL.Identity;
using System.Threading.Tasks;
using CatStore.DAL.Entities;

namespace CatStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IRepository<Cat> Cats { get; }
        IRepository<Order> Orders { get; }
        Task SaveAsync();
        void Save();
    }
}
