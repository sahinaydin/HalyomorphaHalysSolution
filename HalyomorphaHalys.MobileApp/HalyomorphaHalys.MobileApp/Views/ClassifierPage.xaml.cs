using HalyomorphaHalys.MobileApp.Models;
using Newtonsoft.Json;
using Plugin.Media;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HalyomorphaHalys.MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClassifierPage : ContentPage
    {
        public ClassifierPage()
        {
            InitializeComponent();
        }


        protected string PhotoPath { get; set; }

        private async Task<IRestResponse> UploadAsync(string fileName, string url)
        {
            using (var fileStream = File.Open(fileName, FileMode.Open))
            {
                var client = new RestClient(url);
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

        private async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task LoadPhotoAsync(FileResult photo)
        {
            if (photo == null)
            {
                PhotoPath = null;
                return;
            }

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);
            PhotoPath = newFile;
        }



        private void btnOpenCamera_Clicked(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
                if (photo == null)
                    return;
                PhotoPath = photo.Path;
                //await TakePhotoAsync();
                Photo.Source = ImageSource.FromStream(() =>
                {
                    var stream = photo.GetStream();
                    photo.Dispose();
                    return stream;
                });
                btnClassify.IsEnabled = true;
            });
        }

        private async void btnClassify_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PhotoPath))
            {
                await DisplayAlert("Image Selection", "Select valid hazelnut image", "Cancel");
                return;
            }
            loadingSpinner.IsVisible = true;
            loadingSpinner.IsRunning = true;
            string url = "https://apigateway.intalalab.net/gateway/classify";
            IRestResponse restResponse = await this.UploadAsync(PhotoPath, url);
            var obj = JsonConvert.DeserializeObject<PredictModel>(restResponse.Content);
            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Predicted Bug Type: " + obj.Label);
                txtResult.Text = stringBuilder.ToString();
                btnBugDetails.IsEnabled = true;
            }
            loadingSpinner.IsRunning = false;
            loadingSpinner.IsVisible = false;
        }

        private void btnBugDetails_Clicked(object sender, EventArgs e)
        {

        }

        protected override void OnAppearing()
        {
            btnClassify.IsEnabled = false;
            btnBugDetails.IsEnabled = false;
            txtResult.Text = String.Empty;
            loadingSpinner.IsEnabled = false;
            Photo.Source = ImageSource.FromFile("image_placeholder.jpg");
        }
    }
}