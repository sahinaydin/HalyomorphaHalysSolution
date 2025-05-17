using HalyomorphaHalys.MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HalyomorphaHalys.MobileApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }


        private async Task<UserViewModel> GetUserAsync(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                var url = "https://hazelnutbugauthentication.intalalab.com/";
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(url)
                };
                var httpResponseMessage = await httpClient.GetAsync($"/api/authentication/{username}/{password}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string content = await httpResponseMessage.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<UserViewModel>(content);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }


        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Username.Text) || string.IsNullOrEmpty(Password.Text))
            {
                await DisplayAlert("Warning", "Please, fill the form!", "Cancel");
            }
            else
            {
                var user = await GetUserAsync(Username.Text, Password.Text);
                if (user != null)
                {
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                }
                else
                {
                    await DisplayAlert("Warning", "User does not exist!", "Cancel");
                }
            }

        }
    }
}