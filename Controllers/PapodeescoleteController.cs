using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            ViewBag.image = getClaimValue("image");
            ViewBag.name = getClaimValue(ClaimTypes.Name);
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.EscoleteTalkViewModel>>(Helpers.EscolaDeVoceEndpoints.EscoleTalk.get+ "/" + id);
            return View(response.data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveComment(string id, string comment)
        {   
            var model = new EscolaDeVoce.Services.ViewModel.CommentEscoleteTalkViewModel();
            model.Id = Guid.Parse(id);
            model.comment = comment;
            model.userId = Guid.Parse(getClaimValue("Id"));

            Infrastructure.ApiResponse<bool> commentresponse = null;
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            commentresponse = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<bool>>(
                Helpers.EscolaDeVoceEndpoints.EscoleTalk.comment,
                method,
                JsonConvert.SerializeObject(model)
            );

            if(commentresponse != null){
                return Json(new {
                    success = true
                });
            }

            return Json(new {
                success = false
            });
            // return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
