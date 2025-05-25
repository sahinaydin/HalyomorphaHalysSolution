using HalyomorphaHalys.UWP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HalyomorphaHalys.UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            var user = await AuthService.LoginAsync(username, password);

            if (user != null && user.IsActive)
            {
                ApplicationData.Current.LocalSettings.Values["isLoggedIn"] = true;
                ApplicationData.Current.LocalSettings.Values["username"] = user.Username;
                ApplicationData.Current.LocalSettings.Values["userId"] = user.UserId;
                // Giriş başarılı → MainPage'e geç
                Frame.Navigate(typeof(MainPage));
            }
            else
            {
                ErrorTextBlock.Text = "Giriş başarısız. Lütfen bilgileri kontrol edin.";
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
