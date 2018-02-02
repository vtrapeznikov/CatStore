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
    public class CatController : Controller
    {
        IService service;
        public CatController(IService serv)
        {
            service = serv;
        }

        public ActionResult Details(int id)
        {
            var cat = service.GetCats().FirstOrDefault(c => c.Id == id);
            if (cat != null)
                return View(cat);
            return View();
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
                    result = service.DeleteCat(id.Value);
                    Loggin.CreateLog(result.Message);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var cur_user = service.GetCurrentUser();
            if (cur_user != null)
            {
                if (id != null)
                {
                    var c = service.GetCat(id.Value);
                    Mapper.Reset();
                    Mapper.Initialize(cfg => cfg.CreateMap<CatDTO, CatViewModel>());
                    var cat = Mapper.Map<CatDTO, CatViewModel>(c);
                    if (cat != null)
                    {
                        return View(cat);
                    }
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(CatViewModel c)
        {
            var cur_user = service.GetCurrentUser();
            OperationDetails result;
            if (cur_user != null)
            {
                if (ModelState.IsValid)
                {
                    Mapper.Reset();
                    Mapper.Initialize(cfg => cfg.CreateMap<CatViewModel, CatDTO>());
                    var cat = Mapper.Map<CatViewModel, CatDTO>(c);
                    result = service.EditCat(cat);
                    Loggin.CreateLog(result.Message);
                    return RedirectToAction("Index", "Home");
                }
                else
                    return View(c);
            }
            else
                return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            var cur_user = service.GetCurrentUser();
            if (cur_user != null)
            {
                return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(CatViewModel c)
        {
            var cur_user = service.GetCurrentUser();
            if (cur_user != null)
            {
                if (ModelState.IsValid)
                {
                    Mapper.Reset();
                    Mapper.Initialize(cfg => cfg.CreateMap<CatViewModel, CatDTO>());
                    var cat = Mapper.Map<CatViewModel, CatDTO>(c);
                    var result = service.CreateCat(cat);
                    Loggin.CreateLog(result.Message);
                    return RedirectToAction("Index", "Home");
                }
                else
                    return View(c);
            }
            else
                return RedirectToAction("Login", "Account");
        }
    }
}