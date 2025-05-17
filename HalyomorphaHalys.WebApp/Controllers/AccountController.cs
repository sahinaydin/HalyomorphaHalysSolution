using HalyomorphaHalys.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using HalyomorphaHalys.WebApp.Business;


namespace HalyomorphaHalys.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController()
        {
            _userService = new UserService(new UserRepository());
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(User model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var url = "https://hazelnutbugauthentication.intalalab.com/";
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(url)
                };
                var username = model.Username;
                var password = model.UserPassword;
                var httpResponseMessage = await httpClient.GetAsync($"/api/authentication/{username}/{password}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string content = await httpResponseMessage.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(content);
                    if (user != null)
                    {
                        Session["APIUSER"] = user;

                        return RedirectToAction("Index", "Home");

                    }
                }
                else
                {
                    return null;
                }


            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}