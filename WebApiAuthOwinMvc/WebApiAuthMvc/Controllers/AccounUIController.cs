using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApiAuthMvc.Models;

namespace WebApiAuthMvc.Controllers
{
    public class AccounUIController : Controller
    {
        // GET: AccounUI
        public ActionResult Index()
        {
            return View();
        }

        // GET:   
        public ActionResult Login() 
        {
            return View();
        }

        //
        // POST:  
        [HttpPost] 
        public ActionResult Login(LoginModel model)
        {
            if(model.Password == null || model.EmailAddress ==null)
                return View();
            //var getTokenUrl = string.Format(ApiEndPoints.AuthorisationTokenEndpoint.Post.Token, ConfigurationManager.AppSettings["ApiBaseUri"]);
            var getTokenUrl = "http://localhost:57359/token";

            using (HttpClient httpClient = new HttpClient())
            {
                HttpContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", model.EmailAddress),
                    //new KeyValuePair<string, string>("username", "ElenaElena2"),
                    new KeyValuePair<string, string>("password", model.Password)
                    //new KeyValuePair<string, string>("password", "SuperPass")
                });

                HttpResponseMessage result = httpClient.PostAsync(getTokenUrl, content).Result;

                string resultContent = result.Content.ReadAsStringAsync().Result;

                var token = JsonConvert.DeserializeObject<Token>(resultContent);

                Microsoft.Owin.Security.AuthenticationProperties options = new Microsoft.Owin.Security.AuthenticationProperties();

                options.AllowRefresh = true;
                options.IsPersistent = true;
                options.ExpiresUtc = DateTime.UtcNow.AddSeconds(int.Parse(token.expires_in));

                var claims = new[]
                {
                    //new Claim(ClaimTypes.Name, model.EmailAddress),
                    new Claim(ClaimTypes.Name, "ElenaElena2"),
                    new Claim("AcessToken", string.Format("Bearer {0}", token.access_token)),
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie");

                Request.GetOwinContext().Authentication.SignIn(options, identity);
            }

            return RedirectToAction("Index", "OrderList");
        }

        public ActionResult LogOut()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");

            return RedirectToAction("Login");
        }
    }
}