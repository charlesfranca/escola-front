using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeVoce.Frontend.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        protected string getClaimValue(string name){
            var identity = (ClaimsIdentity) User.Identity;
            var value = identity.Claims.Where(c => c.Type == name)
                    .Select(c => c.Value).SingleOrDefault();

            return value;
        }

        protected void setClaimValue(string name, string value){
            var identity = (ClaimsIdentity) User.Identity;
            var claim = identity.Claims.Where(c => c.Type == name).SingleOrDefault();

            if(claim != null){
                identity.RemoveClaim(claim);
            }

            identity.AddClaim(new Claim(name, value));

            var userPrincipal = new ClaimsPrincipal(identity);
        }

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
