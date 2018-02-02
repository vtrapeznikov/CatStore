using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CatStore.BLL.Interfaces;
using CatStore.BLL.DTO;
using CatStore.WEB.Models;
using AutoMapper;
using CatStore.BLL.Infrastructure;

namespace CatStore.WEB.Controllers
{
    public class HomeController : Controller
    {
        IService service;

        public HomeController(IService serv)
        {
            service = serv;
            //for (int i = 0; i < 5; i++)
            //{
            //    service.CreateCat(new CatDTO
            //    {
            //        Cost = 12.2,
            //        Description = "маленький кот",
            //        Name = "кот обыкновенный",
            //        PhotoUrl = "https://pm1.narvii.com/6330/5ead4c177a2ee1bbf377a1f79244f7b26bb9ad74_128.jpg"
            //    });
            //}
        }

        public ActionResult Index()
        {
            IEnumerable<CatDTO> catDtos = service.GetCats();
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<CatDTO, CatViewModel>());
            var cats = Mapper.Map<IEnumerable<CatDTO>, List<CatViewModel>>(catDtos);
            return View(cats);
        }
    }
}