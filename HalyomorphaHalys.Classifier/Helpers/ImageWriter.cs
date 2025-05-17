using HalyomorphaHalys.Classifier.Models;

namespace HalyomorphaHalys.Classifier.Helpers
{
    public class ImageWriter
    {
        public async Task<PredictModel> UploadImage(IFormFile file)
        {
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file);
            }

            return new PredictModel { Message = "Invalid image file" };
        }

        /// <summary>
        /// Method to check if file is image file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return WriterHelper.GetImageFormat(fileBytes) != WriterHelper.ImageFormat.unknown;
        }

        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<PredictModel> WriteFile(IFormFile file)
        {
            //try
            //{
            string fileName;
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            fileName = Guid.NewGuid().ToString() + extension; //Create a new Name for the file due to security reasons.
            var path = Path.Combine(Directory.GetCurrentDirectory(), "BugImages", fileName);
            using (var bits = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(bits);
            }


            // Create single instance of sample data from first line of dataset for model input
            var imageBytes = File.ReadAllBytes(path);
            HalyomorphaHalysMLModel.ModelInput sampleData = new HalyomorphaHalysMLModel.ModelInput()
            {
                ImageSource = imageBytes,
            };

            var predictionResult = HalyomorphaHalysMLModel.Predict(sampleData);
            var message = $"\n\nPredicted Label value: {predictionResult.PredictedLabel} \nPredicted Label scores: [{String.Join(",", predictionResult.Score)}]\n\n";

            return new PredictModel { FileName = fileName, Label = predictionResult.PredictedLabel, Message = message };
            //}
            //catch (Exception e)
            //{
            //    return new PredictModel { Message = e.InnerException.Message };
            //}
        }
    }
}
