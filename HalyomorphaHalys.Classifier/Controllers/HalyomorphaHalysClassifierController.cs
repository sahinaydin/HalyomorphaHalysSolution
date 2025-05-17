using HalyomorphaHalys.Classifier.Helpers;
using HalyomorphaHalys.Classifier.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HalyomorphaHalys.Classifier.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HalyomorphaHalysClassifierController : ControllerBase
    {
        private ImageWriter imageWriter = new ImageWriter();

        [HttpPost]
        public Task<PredictModel> ClassifyBug(IFormFile file)
        {
            var predictionModel = imageWriter.WriteFile(file);
            return predictionModel;
        }
    }
}
