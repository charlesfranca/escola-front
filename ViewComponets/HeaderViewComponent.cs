using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeVoce.Frontend.ViewComponents
{
    public class HeaderViewComponent : BaseViewComponent
    {
        public IViewComponentResult Invoke()
        {   
            var model = new User {
                name = getClaimValue(ClaimTypes.Name),
                email = getClaimValue(ClaimTypes.Email),
                cover = getClaimValue("cover"),
                image = getClaimValue("image")
            };
            return View("_LoggedHeader", model);
        }

    }
}
