using HalyomorphaHalys.MobileApp.ViewModels;
using HalyomorphaHalys.MobileApp.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace HalyomorphaHalys.MobileApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
           Routing.RegisterRoute(nameof(BugClassifierPage), typeof(BugClassifierPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
