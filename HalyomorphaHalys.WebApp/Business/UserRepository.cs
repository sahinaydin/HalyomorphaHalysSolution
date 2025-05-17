using HalyomorphaHalys.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalyomorphaHalys.WebApp.Business
{
    public class UserRepository : IUserRepository
    {
        public User GetUser(string username, string password)
        {
            // Fake DB
            //    var users = new List<User>
            //{
            //    new User { Id = 1, Username = "admin", Password = "1234", Role = "Admin" },
            //    new User { Id = 2, Username = "user", Password = "1234", Role = "User" }
            //};

            //    return users.FirstOrDefault(u => u.Username == username && u.Password == password);
            //}
            return null;

        }
    }
}