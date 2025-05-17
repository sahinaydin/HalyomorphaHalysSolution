using HalyomorphaHalys.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalyomorphaHalys.WebApp.Business
{
    public interface IUserRepository
    {
        User GetUser(string username, string password);
    }
}
