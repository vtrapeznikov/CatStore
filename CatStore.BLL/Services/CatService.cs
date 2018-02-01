using CatStore.BLL.Interfaces;
using CatStore.DAL.Repositories;

namespace CatStore.BLL.Services
{
    public class CatService
    {
        public IService CreateService(string connection)
        {
            return new Service(new UnitOfWork(connection));
        }
    }
}
