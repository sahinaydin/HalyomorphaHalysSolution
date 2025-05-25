using HalyomorphaHalys.UWP.Pages;
using HalyomorphaHalys.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HalyomorphaHalys.UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainViewModel _viewModel;

        public MainPage()
        {
            this.InitializeComponent();
            _viewModel = new MainViewModel(ContentFrame);
            this.DataContext = _viewModel;

            // 👇 Logout event'ine abone ol
            _viewModel.LogoutRequested += ViewModel_LogoutRequested;

            // Varsayılan sayfa
            _viewModel.NavigateCommand.Execute("home");
        }


        private void ViewModel_LogoutRequested(object sender, EventArgs e)
        {
            Frame.Navigate(typeof(Pages.LoginPage));
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            // 1. Settings tıklanmışsa özel kontrol
            if (args.IsSettingsSelected)
            {
                _viewModel.NavigateCommand.Execute("settings");
                return;
            }

            // 2. Diğer menü öğeleri
            if (args.SelectedItem is NavigationViewItem item && item.Tag != null)
            {
                string tag = item.Tag.ToString();
                _viewModel.NavigateCommand.Execute(tag);
            }
        }

        private void NavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            _viewModel.NavigateCommand.Execute("logout");

        }
    }
}
