using HalyomorphaHalys.UWP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HalyomorphaHalys.UWP.Services
{
    public static class ClassifierService
    {
        private const string ApiUrl = "https://bugclassifier.intalalab.com/api/HalyomorphaHalysClassifier";

        public static async Task<PredictModel> ClassifyImageAsync(StorageFile photoFile)
        {
            if (photoFile == null) return null;

            using (var httpClient = new HttpClient())
            using (var content = new MultipartFormDataContent())
            {
                var stream = await photoFile.OpenStreamForReadAsync();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                content.Add(fileContent, "file", photoFile.Name);

                var response = await httpClient.PostAsync(ApiUrl, content);
                if (!response.IsSuccessStatusCode)
                    return new PredictModel { Label = "Hata", Message = $"Kod: {response.StatusCode}" };

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PredictModel>(json);
            }
        }
    }
}
