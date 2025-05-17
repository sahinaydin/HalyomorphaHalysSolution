using HalyomorphaHalys.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalyomorphaHalys.WebApp.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User ValidateUser(string username, string password)
        {
            return _userRepository.GetUser(username, password);
        }
    }
}