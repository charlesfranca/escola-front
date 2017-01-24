using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeVoce.Frontend.Controllers
{
    [Authorize]
    public class EmbaixadorasController : Controller
    {
        public IActionResult Index()
        {   
            return View();
        }

        public IActionResult Detail(string id)
        {   
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
