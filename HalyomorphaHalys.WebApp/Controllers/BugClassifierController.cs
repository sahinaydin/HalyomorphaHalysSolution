using HalyomorphaHalys.WebApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HalyomorphaHalys.WebApp.Controllers
{
    public class BugClassifierController : Controller
    {
        HazelnutBugDbEntities db = new HazelnutBugDbEntities();

        public ActionResult Index()
        {
            if (Session["APIUSER"] != null)
            {
                return View(db.TestImages.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        public async Task<ActionResult> Classifier(int? id)
        {
            if (Session["APIUSER"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("NotFound");
            }
            var imageModel = db.TestImages.FirstOrDefault(m => m.ImageId == id);
            var imagePath = Path.Combine(Server.MapPath("~/Content/TestImages"), imageModel.ImageName);
            string url = "https://hazelnutbugclassifier.intalalab.com/api/HalyomorphaHalysClassifier";
            IRestResponse restResponse = await this.UploadAsync(imagePath, url);
            var obj = JsonConvert.DeserializeObject<PredictModel>(restResponse.Content);
            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ViewBag.Result = obj.Label;
            }
            return View(imageModel);
        }

        //public async Task<ActionResult> CultivarDetails(string cultivarname)
        //{
        //    var url = "https://apigateway.intalalab.net/";
        //    var httpClient = new HttpClient()
        //    {
        //        BaseAddress = new Uri(url)
        //    };

        //    var httpResponse = httpClient.GetAsync($"/gateway/rdfdataprovider/{cultivarname}");
        //    string content = await httpResponse.Result.Content.ReadAsStringAsync();
        //    var hazelnutCultivar = JsonConvert.DeserializeObject<HazelnutCultivar>(content);
        //    if (hazelnutCultivar != null)
        //    {
        //        return View(hazelnutCultivar);
        //    }
        //    else
        //    {
        //        return RedirectToAction("NotFound");
        //    }
        //}

        public ActionResult UploadImage()
        {
            if (Session["APIUSER"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }


        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    string path = Path.Combine(Server.MapPath("~/Content/TestImages"), Path.GetFileName(photo.FileName));
                    photo.SaveAs(path);

                    var imageModel = new TestImage();
                    imageModel.UserId = ((User)Session["APIUSER"]).UserId;
                    imageModel.ImageName = photo.FileName;
                    imageModel.ImageTitle = photo.FileName;
                    imageModel.ImageFile = System.IO.File.ReadAllBytes(path);
                    db.TestImages.Add(imageModel);
                    db.SaveChanges();
                    int imageId = imageModel.ImageId;
                    return RedirectToAction("Classifier", new { id = imageId });
                }
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (Session["APIUSER"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return RedirectToAction("NotFound");
            }

            var imageModel = db.TestImages.FirstOrDefault(m => m.ImageId == id);
            if (imageModel == null)
            {
                return RedirectToAction("NotFound");
            }

            return View(imageModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var imageModel = db.TestImages.Find(id);
            db.TestImages.Remove(imageModel);
            db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ImageModelExists(int id)
        {
            return db.TestImages.Any(e => e.ImageId == id);
        }



        private async Task<IRestResponse> UploadAsync(string fileName, string server)
        {
            using (var fileStream = System.IO.File.Open(fileName, FileMode.Open))
            {
                var client = new RestClient(server);
                var request = new RestRequest(Method.POST);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await fileStream.CopyToAsync(memoryStream);
                    request.AddFile("file", memoryStream.ToArray(), fileName);
                    request.AlwaysMultipartFormData = true;
                    var response = await client.ExecuteAsync(request);
                    return response;
                }
            }
        }
    }
}