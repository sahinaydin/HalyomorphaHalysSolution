using HalyomorphaHalys.DesktopApp.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http.Headers;
using System.Text;

namespace HalyomorphaHalys.DesktopApp
{
    public partial class HalyomorphaHalysClassifier : Form
    {
        public HalyomorphaHalysClassifier()
        {
            InitializeComponent();
        }

        private string filePath;


        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            txtClassificationResult.Clear();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.JPEG;*.JPG;*.PNG)|*.JPEG;*.JPG;*.PNG|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    pbBugImage.ImageLocation = filePath;
                    txtImagePath.Text = new FileInfo(filePath).Name;
                }
            }
        }

        private async void btnPredict_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Select valid bug image!", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var url = "http://localhost:5009";
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(url)
            };

            using var form = new MultipartFormDataContent();

            using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(filePath));
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
            form.Add(fileContent, "file", Path.GetFileName(filePath));
            var response = await httpClient.PostAsync($"/api/HalyomorphaHalysClassifier", form);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var objectModel = JsonConvert.DeserializeObject<PredictModel>(responseContent.ToString());
            if (objectModel != null)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Predicted Bug Type : " + objectModel.Label);
                txtClassificationResult.Text = stringBuilder.ToString();
            }



            //string url = "https://hazelnutclassifier.intalalab.net/api/HazelnutClassification";
            //string localUrl = "https://apigateway.intalalab.net/gateway/classify";
            //IRestResponse restResponse = await this.UploadAsync(filePath, localUrl);
            //var obj = JsonConvert.DeserializeObject<PredictModel>(restResponse.Content);
            //if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    MessageBox.Show("You have successfully uploaded the file", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtClassificationResult.Text = obj.Label;
            //}
        }

        private async Task<RestSharp.IRestResponse> UploadAsync(string fileName, string server)
        {
            using (var fileStream = File.Open(fileName, FileMode.Open))
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
