using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeVoce.Frontend.ViewComponents
{
    public class EspecialistaViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(bool showEmbaixadorasButton)
        {   
            ViewBag.showEmbaixadorasButton = showEmbaixadorasButton;
            try
            {
                var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<List<EscolaDeVoce.Services.ViewModel.EspecialistViewModel>>>(Helpers.EscolaDeVoceEndpoints.Especialist.getEspecialists);
                return View(response.data);   
            }
            catch (System.Exception)
            {
            }
            return View(new List<EscolaDeVoce.Services.ViewModel.EspecialistViewModel>());
        }
    }
}
