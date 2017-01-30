using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeVoce.Frontend.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public BaseController()
        {
            
        }
        
        protected string getClaimValue(string name){
            var identity = (ClaimsIdentity) User.Identity;
            var value = identity.Claims.Where(c => c.Type == name)
                    .Select(c => c.Value).SingleOrDefault();

            return value;
        }

        protected async void updateClaimValue(string name, string value){
            var identity = (ClaimsIdentity) User.Identity;
            var claim = identity.Claims.Where(c => c.Type == name).SingleOrDefault();

            if(claim != null){
                identity.RemoveClaim(claim);
            }

            identity.AddClaim(new Claim(name, value));
            
            var userPrincipal = new ClaimsPrincipal(identity);
            
            await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                        IsPersistent = true,
                        AllowRefresh = false
                    });
        }
    }
}