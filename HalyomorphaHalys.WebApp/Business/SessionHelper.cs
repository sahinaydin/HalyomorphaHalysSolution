using HalyomorphaHalys.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HalyomorphaHalys.WebApp.Business
{
    public class SessionHelper
    {
        public static User CurrentUser
        {
            get => HttpContext.Current.Session["CurrentUser"] as User;
            set => HttpContext.Current.Session["CurrentUser"] = value;
        }

        public static bool IsAuthenticated => CurrentUser != null;
    }
}