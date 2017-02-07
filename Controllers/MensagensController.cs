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
    public class MensagensController : BaseController
    {
        public async Task<IActionResult> Index()
        {   
            ViewBag.UserId = getClaimValue("Id");
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<List<EscolaDeVoce.Services.ViewModel.UserViewModel>>>(Helpers.EscolaDeVoceEndpoints.User.create);
            return View(response.data);
        }

        public IActionResult Detalhe(string id)
        {   
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getMessages(string from){
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<List<Services.ViewModel.MessageViewModel>>>(Helpers.EscolaDeVoceEndpoints.Message.getusermessages + "/" + from + "/" + getClaimValue("Id").ToString());
            var retorno = new EscolaDeVoce.Frontend.UserMessages();
            retorno.mensagens = response.data;
            retorno.userid = Guid.Parse(getClaimValue("Id"));

            return PartialView("_ChatMessages", retorno);
        }

        [HttpPost]
        public async Task<IActionResult> sendMessage(string message, string to)
        {   
            var model = new EscolaDeVoce.Services.ViewModel.MessageViewModel();
            model.fromId = Guid.Parse(getClaimValue("Id"));
            model.message = message;
            model.toId = Guid.Parse(to);
            model.title = message;

            Infrastructure.ApiResponse<bool> mensagemresponse = null;
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            mensagemresponse = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<bool>>(
                Helpers.EscolaDeVoceEndpoints.Message.get,
                method,
                JsonConvert.SerializeObject(model)
            );

            if(mensagemresponse != null){
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
