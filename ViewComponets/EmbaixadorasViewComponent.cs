using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeVoce.Frontend.ViewComponents
{
    public class EmbaixadorasViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {   
            return View();
        }

    }
}
