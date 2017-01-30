using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeVoce.Frontend.Controllers
{
    [Authorize]
    public class CursosController : BaseController
    {
        public async Task<IActionResult> Index()
        {   
            HttpContext.Session.SetString("", "Rick");
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<List<EscolaDeVoce.Services.ViewModel.CourseViewModel>>>(Helpers.EscolaDeVoceEndpoints.Courses.getCourses);
            return View(response.data);
        }

        public async Task<IActionResult> Detalhes(string id)
        {   
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.CourseViewModel>>(Helpers.EscolaDeVoceEndpoints.Courses.getCourses + "/" + id);
            return View(response.data);
        }

        public async Task<IActionResult> Sala(string id)
        {
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.CourseViewModel>>(Helpers.EscolaDeVoceEndpoints.Courses.getCourses + "/" + id);
            return View(response.data);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
