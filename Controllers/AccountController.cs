using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;

namespace EscolaDeVoce.Frontend.Controllers
{
    public class AccountController : BaseController
    {
        private IHostingEnvironment _env;
        public AccountController(IHostingEnvironment env)
        {
            _env = env;
        }
        
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var Id = Guid.Parse(getClaimValue("Id"));
            var userresponse = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.UserViewModel>>(
                Helpers.EscolaDeVoceEndpoints.User.create + "/" + Id,
                null,
                "token"
            );

            return View(userresponse.data.person);
        }

        [HttpPost]
        public async Task<IActionResult> ProfilePhotoUpload(string image){
            var webRoot = _env.WebRootPath;
            var imagename = Guid.NewGuid().ToString() + ".jpg";
            string filePath = webRoot + "/images/" + imagename;
            var baseImage = image.Replace("data:image/png;base64,", "");
            baseImage = baseImage.Replace("data:image/jpeg;base64,", "");
            baseImage = baseImage.Replace("data:image/jpg;base64,", "");
            baseImage = baseImage.Replace("\"", "");
            System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(baseImage));

            var userimage = new EscolaDeVoce.Services.ViewModel.UpdateUserImageViewModel();
            userimage.Id = Guid.Parse(getClaimValue("Id"));
            userimage.image = imagename;

            Infrastructure.ApiResponse<bool> updateimageresponse = null;
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            updateimageresponse = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<bool>>(
                Helpers.EscolaDeVoceEndpoints.User.changeImage,
                method,
                JsonConvert.SerializeObject(userimage)
            );

            if(updateimageresponse != null){
                updateClaimValue("image", imagename);
                return Json(new {
                    success = true,
                    imageName = imagename,
                    imagepath = Request.Path
                });    
            }

            return Json(new {
                success = false
            });
        }

        [HttpPost]
        public async Task<IActionResult> CoverPhotoUpload(string image){
            var webRoot = _env.WebRootPath;
            var imagename = Guid.NewGuid().ToString() + ".jpg";
            string filePath = webRoot + "/images/" + imagename;
            var baseImage = image.Replace("data:image/png;base64,", "");
            baseImage = baseImage.Replace("data:image/jpeg;base64,", "");
            baseImage = baseImage.Replace("data:image/jpg;base64,", "");
            baseImage = baseImage.Replace("\"", "");
            System.IO.File.WriteAllBytes(filePath, Convert.FromBase64String(baseImage));

            var userimage = new EscolaDeVoce.Services.ViewModel.UpdateUserCoverViewModel();
            userimage.Id = Guid.Parse(getClaimValue("Id"));
            userimage.image = imagename;

            Infrastructure.ApiResponse<bool> updateimageresponse = null;
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            updateimageresponse = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<bool>>(
                Helpers.EscolaDeVoceEndpoints.User.changeCover,
                method,
                JsonConvert.SerializeObject(userimage)
            );

            if(updateimageresponse != null){
                updateClaimValue("cover", imagename);
                return Json(new {
                    success = true,
                    imageName = imagename,
                    imagepath = Request.Path
                });    
            }

            return Json(new {
                success = false
            });
        }

        [Authorize]
        public IActionResult Profile()
        {
            var model = new User {
                name = getClaimValue(ClaimTypes.Name),
                //lastname = getClaimValue(ClaimTypes.Surname),
                email = getClaimValue(ClaimTypes.Email)
            };
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookie");
            return RedirectToActionPermanent("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Signup(string username, string password, string email, string name, string lastname, bool isFacebook)
        {   
            var model = new EscolaDeVoce.Services.ViewModel.CreateUserViewModel();
            model.password = password;
            model.username = email;

            model.person = new EscolaDeVoce.Services.ViewModel.PersonViewModel();
            model.person.name = name;
            model.person.lastname = lastname;

            Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.UserViewModel> createuserresponse = null;
            System.Net.Http.HttpMethod method = System.Net.Http.HttpMethod.Post;
            createuserresponse = await ApiRequestHelper.postPutRequest<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.UserViewModel>>(
                Helpers.EscolaDeVoceEndpoints.User.create,
                method,
                JsonConvert.SerializeObject(model)
            );

            if(createuserresponse != null){
                return await this.Login(username, password, isFacebook);
            }

            return Json(new {
                status = false
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, bool isFacebook)
        {   
            Frontend.AuthenticationModel response = null;
            response = await ApiRequestHelper.postPutEncodedRequest<AuthenticationModel>(
                Helpers.EscolaDeVoceEndpoints.tokenUrl,
                username,
                password,
                isFacebook
            );

            if(response != null){
                if(response.StatusCode != HttpStatusCode.Created){
                    return Json(new {
                        status = false
                    });
                }
                Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.UserViewModel> userresponse = null;

                userresponse = await ApiRequestHelper.Get<Infrastructure.ApiResponse<EscolaDeVoce.Services.ViewModel.UserViewModel>>(
                    Helpers.EscolaDeVoceEndpoints.User.info,
                    null,
                    response.access_token
                );

                const string Issuer = "https://www.escoladevoce.com.br";
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, userresponse.data.person.name, ClaimValueTypes.String, Issuer),
                    new Claim("Id", userresponse.data.Id.ToString(), ClaimValueTypes.String, Issuer),
                    new Claim(ClaimTypes.Email, userresponse.data.username, ClaimValueTypes.String, Issuer),
                    new Claim("TOKEN", response.access_token, ClaimValueTypes.String, Issuer),
                };

                var userIdentity = new ClaimsIdentity(claims, "Passport");

                if (!String.IsNullOrEmpty(userresponse.data.cover)) userIdentity.AddClaim(new Claim("cover", userresponse.data.cover, ClaimValueTypes.String, Issuer));
                if (!String.IsNullOrEmpty(userresponse.data.image)) userIdentity.AddClaim(new Claim("image", userresponse.data.image, ClaimValueTypes.String, Issuer));

                var userPrincipal = new ClaimsPrincipal(userIdentity);

                await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal,
                    new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                        IsPersistent = true,
                        AllowRefresh = false
                    });

                return Json(new {
                    status = true
                });
            }

            return Json(new {
                status = false
            });
        }

        public IActionResult UpdateAccount([Bind("Id,name")]Services.ViewModel.PersonViewModel person){
            return Json(new {});
        }

        public void logUser(){

        }

        public IActionResult Error()
        {
            return View();
        }
    }
}