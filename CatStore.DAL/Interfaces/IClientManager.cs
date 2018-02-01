using System;
using CatStore.DAL.Entities;

namespace CatStore.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
        ClientProfile Get(string id);
    }
}
