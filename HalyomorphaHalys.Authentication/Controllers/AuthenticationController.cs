using HalyomorphaHalys.Authentication.Models;
using HalyomorphaHalys.Authentication.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HalyomorphaHalys.Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly HazelnutBugDbContext _context;

        public AuthenticationController(HazelnutBugDbContext context)
        {
            _context = context;
        }


        [HttpGet("{username}/{password}")]
        public UserViewModel GetUser(string username, string password)
        {
            var user = _context.Users.Where(u => u.Username == username && u.UserPassword == password && u.IsActive == true).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            else
            {
                var userModel = new UserViewModel();
                userModel.UserId = user.UserId;
                userModel.Username = username;
                userModel.UserPassword = password;
                userModel.FirstName = user.FirstName;
                userModel.LastName = user.LastName;
                userModel.Email = user.Email;
                userModel.IsActive = user.IsActive;

                return userModel;
            }
        }
    }
}
