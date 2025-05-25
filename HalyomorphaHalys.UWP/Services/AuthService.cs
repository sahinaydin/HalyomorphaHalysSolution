using HalyomorphaHalys.UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HalyomorphaHalys.UWP.Services
{
    public static class AuthService
    {
        public static async Task<UserModel> LoginAsync(string username, string password)
        {
            var url = $"https://hazelnutbugauthentication.intalalab.com/api/authentication/{username}/{password}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<UserModel>(json);
                    return user;
                }
            }

            return null; // Giriş başarısız
        }
    }
}
