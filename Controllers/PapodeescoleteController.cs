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
    public class PapodeescoleteController : BaseController
    {
        public IActionResult Index()
        {   
            return View();
        }

        public async Task<IActionResult> Detalhe(string id)
        {   
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.EscoleteTalkViewModel>>(Helpers.EscolaDeVoceEndpoints.EscoleTalk.get+ "/" + id);
            return View(response.data);
            // return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
