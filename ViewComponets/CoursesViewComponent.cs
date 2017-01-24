using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeVoce.Frontend.ViewComponents
{
    public class CoursesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string windowName, string buttonName)
        {   
            ViewBag.windowName = windowName;
            ViewBag.buttonName = buttonName;
            
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<List<EscolaDeVoce.Services.ViewModel.CourseViewModel>>>(Helpers.EscolaDeVoceEndpoints.Courses.getCourses);
            return View(response.data);
        }
    }
}
