using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeVoce.Frontend.ViewComponents
{
    public class MultipleQuizViewComponent : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {   
            try
            {
                var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<List<EscolaDeVoce.Services.ViewModel.PersonalityQuestionViewModel>>>(Helpers.EscolaDeVoceEndpoints.Questions.get);
                return View(response.data);
            }
            catch (System.Exception)
            {
            }
            return View(new EscolaDeVoce.Services.ViewModel.PersonalityQuestionViewModel());
        }
    }
}
