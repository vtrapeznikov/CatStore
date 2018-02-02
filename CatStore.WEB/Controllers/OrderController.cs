using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CatStore.BLL.Interfaces;
using CatStore.BLL.DTO;
using CatStore.WEB.Models;
using AutoMapper;
using CatStore.BLL.Services;
using CatStore.BLL.Infrastructure;

namespace CatStore.WEB.Controllers
{
    public class OrderController : Controller
    {
        IService service;
        public OrderController(IService serv)
        {
            service = serv;
        }

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var ordersDtos = service.GetOrders();
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>());
            var orders = Mapper.Map<IEnumerable<OrderDTO>, List<OrderViewModel>>(ordersDtos);
            return View(orders);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            var cur_user = service.GetCurrentUser();
            OperationDetails result;
            if (cur_user != null)
            {
                if (id != null)
                {
                    result = service.DeleteOrder(id.Value);
                    Loggin.CreateLog(result.Message);
                    return Redirect(Request.UrlReferrer.ToString());
                }
                else
                {
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize]
        public ActionResult Buy(int id)
        {
            var cur_user = service.GetCurrentUser();
            var order = new OrderDTO
            {
                CatId = id,
                ClientProfileId = cur_user.Id,
                Date = DateTime.Now,
                Sum = service.GetCats().FirstOrDefault(c => c.Id == id).Cost
            };
            service.MakeOrder(order);
            return RedirectToAction("Index", "Home");
        }
    }
}