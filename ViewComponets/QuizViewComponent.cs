using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeVoce.Frontend.ViewComponents
{
    public class QuizViewComponent : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {   
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.PersonalityQuestionViewModel>>(Helpers.EscolaDeVoceEndpoints.Questions.getNextQuestion + "/" + getClaimValue("Id").ToString());
            return View(response.data);
        }
    }
}
