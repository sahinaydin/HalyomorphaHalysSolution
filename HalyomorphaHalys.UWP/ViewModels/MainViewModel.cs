using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace HalyomorphaHalys.UWP.ViewModels
{
    public class MainViewModel:BaseViewModel
    {
        private Frame _navigationFrame;

        public MainViewModel(Frame frame)
        {
            _navigationFrame = frame;
            NavigateCommand = new RelayCommand(OnNavigate);
            Title = "Welcome to INTALA LAB";
        }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ICommand NavigateCommand { get; }
        private void OnNavigate(object tag)
        {
            string page = tag?.ToString();
            switch (page)
            {
                case "home":
                    _navigationFrame.Navigate(typeof(Pages.HomePage));
                    Title = "Home";
                    break;
                case "classifier":
                    _navigationFrame.Navigate(typeof(Pages.ClassifierPage));
                    Title = "Classifier";
                    break;
                case "about":
                    _navigationFrame.Navigate(typeof(Pages.AboutPage));
                    Title = "About";
                    break;
                case "logout":
                    HandleLogout();
                    break;
                case "settings":
                    _navigationFrame.Navigate(typeof(Pages.SettingsPage));
                    Title = "Settings";
                    break;
            }
        }

        private void HandleLogout()
        {
            ApplicationData.Current.LocalSettings.Values.Clear();

            // View'a sinyal gönder → LoginPage'e geçmesi için
            LogoutRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler LogoutRequested;

    }
}
