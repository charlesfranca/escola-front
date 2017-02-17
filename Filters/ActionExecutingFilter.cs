using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EscolaDeVoce.Frontend.Filter
{
    public class ActionExecutingFilter : IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.HttpContext.User.Identity.IsAuthenticated){
                
            }
        }
    }
}
