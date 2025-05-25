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
    public static class BugDensityService
    {
        private const string ApiUrl = "https://hazelnutdensitymap.intalalab.com/api/BugDensityNotifications";

        public static async Task<List<BugDensityNotification>> GetDensityDataAsync()
        {
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(ApiUrl);
                // Tek bir kayıt dönerse:
                try
                {
                    var single = JsonConvert.DeserializeObject<BugDensityNotification>(json);
                    return new List<BugDensityNotification> { single };
                }
                catch
                {
                    // Dizi gelirse
                    return JsonConvert.DeserializeObject<List<BugDensityNotification>>(json);
                }
            }
        }
    }
}
