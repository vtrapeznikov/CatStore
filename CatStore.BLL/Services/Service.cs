using System;
using System.Collections.Generic;
using AutoMapper;
using CatStore.DAL.Repositories;
using CatStore.DAL.Entities;
using CatStore.DAL.Interfaces;
using CatStore.BLL.DTO;
using CatStore.BLL.Infrastructure;
using CatStore.BLL.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace CatStore.BLL.Services
{
    public class Service : IService
    {
        IUnitOfWork db { get; set; }

        public Service(IUnitOfWork db)
        {
            this.db = db;
        }
        public Service(string connectionString)
        {
            db = new UnitOfWork(connectionString);
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await db.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await db.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await db.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                db.ClientManager.Create(clientProfile);
                await db.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await db.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await db.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public UserDTO GetCurrentUser()
        {
            ApplicationUser appUser = db.UserManager.FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            if (appUser == null)
                return null;
            else
            {
                var user = db.ClientManager.Get(appUser.Id);
                return new UserDTO
                {
                    Address = user.Address,
                    Name = user.Name,
                    Email = user.ApplicationUser.Email,
                    UserName = user.ApplicationUser.UserName
                };
            }
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await db.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await db.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void MakeOrder(OrderDTO orderDto)
        {
            var cat = db.Cats.Get(orderDto.CatId);
            var client = db.UserManager.FindById(orderDto.ClientProfileId.ToString());

            if (cat == null)
                throw new ValidationException("Товар не найден", "");
            if (client == null)
                throw new ValidationException("Пользователь не найден", "");
            Order order = new Order
            {
                Date = DateTime.Now,
                CatId = cat.Id,
                Sum = cat.Cost,
                ClientProfileId = client.ClientProfile.Id
            };
            db.Orders.Create(order);
            db.Save();
        }

        public IEnumerable<CatDTO> GetCats()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<Cat, CatDTO>());
            return Mapper.Map<IEnumerable<Cat>, List<CatDTO>>(db.Cats.GetAll());
        }

        public CatDTO GetCat(int? id)
        {
            if (id == null)
                throw new ValidationException("Ид кота не выбран", "");
            var cat = db.Cats.Get(id.Value);
            if (cat == null)
                throw new ValidationException("Товар не найден", "");
            // применяем автомаппер для проекции Cat на CatDTO
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<Cat, CatDTO>());
            return Mapper.Map<Cat, CatDTO>(cat);
        }

        public IEnumerable<OrderDTO> GetOrders()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<Order, OrderDTO>());
            return Mapper.Map<IEnumerable<Order>, List<OrderDTO>>(db.Orders.GetAll());
        }

        public void CreateCat(CatDTO item)
        {
            if (item != null)
            {
                db.Cats.Create(new Cat
                {
                    Cost = item.Cost,
                    Description = item.Description,
                    Name = item.Name,
                    PhotoUrl = item.PhotoUrl
                });
                db.Save();
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
