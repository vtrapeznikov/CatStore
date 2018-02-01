using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using CatStore.WEB.Models;
using CatStore.BLL.DTO;
using System.Security.Claims;
using CatStore.BLL.Interfaces;
using CatStore.BLL.Infrastructure;

namespace CatStore.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IService Service
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await Service.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Address = model.Address,
                    Name = model.Name,
                    Role = "user"
                };
                OperationDetails operationDetails = await Service.Create(userDto);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
        private async Task SetInitialDataAsync()
        {
            await Service.SetInitialData(new UserDTO
            {
                Email = "qwe@wqe.by",
                UserName = "qwe@wqe.by",
                Password = "123",
                Name = "Владимир Трапезников",
                Address = "Новополоцк",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }

        public ActionResult GetName()
        {
            var user = Service.GetCurrentUser();
            if (user != null)
            {
                ViewBag.Name = user.Name;
            }
            else
                ViewBag.Name = "Гость";
            return PartialView();
        }

        public ActionResult Index()
        {
            var user = Service.GetCurrentUser();
            return View(user);
        }
    }
}