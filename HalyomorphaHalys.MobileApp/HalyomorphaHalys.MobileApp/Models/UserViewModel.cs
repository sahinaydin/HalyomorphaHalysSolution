using System;
using System.Collections.Generic;
using System.Text;

namespace HalyomorphaHalys.MobileApp.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        public string Username { get; set; } 

        public string UserPassword { get; set; }

        public string FirstName { get; set; } 

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }
    }
}
