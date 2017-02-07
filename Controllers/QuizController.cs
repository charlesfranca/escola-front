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
    public class QuizController : BaseController
    {
        public QuizController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> getNextQuestion(string from){
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<Services.ViewModel.PersonalityQuestionViewModel>>(Helpers.EscolaDeVoceEndpoints.User.nextQuestion + "/" + getClaimValue("Id").ToString());
            return PartialView("_QuizQuestion", response.data);
        }

        public async Task<IActionResult> AnswerQuestion(string answerId, int score)
        {   
            var model = new EscolaDeVoce.Services.ViewModel.UserAnswerQuestionViewModel();
            model.userId = Guid.Parse(getClaimValue("Id"));
            model.answearId = Guid.Parse(answerId);
            model.score = score;

            Infrastructure.ApiResponse<bool> mensagemresponse = null;
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            mensagemresponse = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<bool>>(
                Helpers.EscolaDeVoceEndpoints.User.answerQuestion,
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
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
