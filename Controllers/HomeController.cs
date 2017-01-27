using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeVoce.Frontend.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {
        }
        [AllowAnonymous]
        public IActionResult Index()
        {   
            if(User.Identity.IsAuthenticated){
                return RedirectToAction("Home");
            }
            return View();
        }

        public IActionResult Home()
        {
            var model = new User {
                name = getClaimValue(ClaimTypes.Name),
                email = getClaimValue(ClaimTypes.Email)
            };
            
            var fullname = getClaimValue(ClaimTypes.Name);
            string[] names = fullname.Split(' ');
            string name = names[0];
            

            ViewBag.name = name;

            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
