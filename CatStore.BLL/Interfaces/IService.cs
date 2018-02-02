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
        OperationDetails MakeOrder(OrderDTO orderDto);
        IEnumerable<OrderDTO> GetOrders();
        OperationDetails DeleteOrder(int id);

        CatDTO GetCat(int? id);
        IEnumerable<CatDTO> GetCats();
        OperationDetails CreateCat(CatDTO item);
        OperationDetails DeleteCat(int id);
        OperationDetails EditCat(CatDTO item);

        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        UserDTO GetCurrentUser();
    }
}
