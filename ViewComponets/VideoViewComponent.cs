using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeVoce.Frontend.ViewComponents
{
    public class VideoViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Guid categoryid)
        {   
            try
            {
                var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<List<EscolaDeVoce.Services.ViewModel.VideoViewModel>>>(Helpers.EscolaDeVoceEndpoints.Videos.getByCategory + categoryid.ToString());
                return View(response.data);
            }
            catch (System.Exception)
            {
            }

            return View(new List<EscolaDeVoce.Services.ViewModel.VideoViewModel>());
        }

    }
}
