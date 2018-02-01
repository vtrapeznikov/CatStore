using CatStore.DAL.Interfaces;
using CatStore.DAL.EF;
using CatStore.DAL.Entities;

namespace CatStore.DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public CatContext Db { get; set; }

        public ClientManager(CatContext db)
        {
            Db = db;
        }

        public void Create(ClientProfile item)
        {
            Db.ClientProfiles.Add(item);
            Db.SaveChanges();
        }

        public ClientProfile Get(string id)
        {
            return Db.ClientProfiles.Find(id);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
