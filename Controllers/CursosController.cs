using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EscolaDeVoce.Frontend.Controllers
{
    [Authorize]
    public class CursosController : BaseController
    {
        public async Task<IActionResult> Index()
        {   
            //HttpContext.Session.SetString("", "Rick");
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<List<EscolaDeVoce.Services.ViewModel.CourseViewModel>>>(Helpers.EscolaDeVoceEndpoints.Courses.getCourses + "/" + getClaimValue("Id"));
            return View(response.data);
        }

        public async Task<IActionResult> Detalhes(string id)
        {   
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.CourseViewModel>>(Helpers.EscolaDeVoceEndpoints.Courses.getdetail + "/" + id + "/" + getClaimValue("Id"));
            return View(response.data);
        }

        public async Task<IActionResult> Detalhe(string id)
        {   
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.CourseViewModel>>(Helpers.EscolaDeVoceEndpoints.Courses.getdetail + "/" + id + "/" + getClaimValue("Id"));
            if(response.data.userStarted){
                var video = response.data.videos.FirstOrDefault();
                return RedirectToAction("Sala", new {id=response.data.Id, videoid=video.Id});
            }else{
                return View(response.data);
            }
        }
        
        public async Task<IActionResult> Sala(string id, string videoid)
        {
            ViewBag.VideoId = Guid.Parse(videoid);
            var response = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.CourseViewModel>>(Helpers.EscolaDeVoceEndpoints.Courses.getdetail + "/" + id);
            var statusresponse = await ApiRequestHelper.Get<Infrastructure.ApiResponse<int>>(Helpers.EscolaDeVoceEndpoints.Videos.getStatus + "/" + getClaimValue("Id") + "/" + videoid);
            ViewBag.status = statusresponse.data;
            ViewBag.name = getClaimValue(ClaimTypes.Name);
            return View(response.data);
        }
        public async Task<IActionResult> AddDictionaryItem([Bind("title,content,dictionaryType")]Services.ViewModel.DictionaryViewModel model){
            Infrastructure.ApiResponse<bool> favoriteresponse = null;
            model.userId = Guid.Parse(getClaimValue("Id"));
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            favoriteresponse = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<bool>>(
                Helpers.EscolaDeVoceEndpoints.Dictionary.get,
                method,
                JsonConvert.SerializeObject(model)
            );
            
            return View();
        }

        public async Task<IActionResult> startCourse(string courseId){
            Infrastructure.ApiResponse<bool> courseResponse = null;
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            courseResponse = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<bool>>(
                Helpers.EscolaDeVoceEndpoints.User.startCourse + "/" + getClaimValue("Id") + "/" + courseId,
                method
            );
            return Json(new { success = true });
        }

        public async Task<IActionResult> updateVideoStatus([Bind("progress,videoId")]Services.ViewModel.UpdateVideoStatusViewModel model){
            Infrastructure.ApiResponse<bool> favoriteresponse = null;
            model.userId = Guid.Parse(getClaimValue("Id"));
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            favoriteresponse = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<bool>>(
                Helpers.EscolaDeVoceEndpoints.Videos.updateStatus,
                method,
                JsonConvert.SerializeObject(model)
            );
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorite(string videoId)
        {   
            var model = new EscolaDeVoce.Services.ViewModel.AddVideoToFavoriteViewModel();
            model.userId = Guid.Parse(getClaimValue("Id"));
            model.videoId = Guid.Parse(videoId);

            Infrastructure.ApiResponse<bool> favoriteresponse = null;
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            favoriteresponse = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<bool>>(
                Helpers.EscolaDeVoceEndpoints.Videos.addToFavorites,
                method,
                JsonConvert.SerializeObject(model)
            );

            if(favoriteresponse != null){
                return Json(new {
                    success = true
                });
            }

            return Json(new {
                success = false
            });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
