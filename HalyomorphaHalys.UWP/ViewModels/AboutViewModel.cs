using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;

namespace HalyomorphaHalys.UWP.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            OpenWebCommand = new RelayCommand(_ => OpenWebsite());
        }

        public ICommand OpenWebCommand { get; }

        private async void OpenWebsite()
        {
            var uri = new Uri("https://intalalab.com/");
            await Launcher.LaunchUriAsync(uri);
        }
    }
}
