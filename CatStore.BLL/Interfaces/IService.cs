using System.Collections.Generic;
using CatStore.BLL.DTO;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CatStore.BLL.Infrastructure;
using CatStore.DAL.Entities;

namespace CatStore.BLL.Interfaces
{
    public interface IService : IDisposable
    {
        void MakeOrder(OrderDTO orderDto);
        CatDTO GetCat(int? id);
        IEnumerable<CatDTO> GetCats();
        IEnumerable<OrderDTO> GetOrders();
        void CreateCat(CatDTO item);

        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        UserDTO GetCurrentUser();
    }
}
