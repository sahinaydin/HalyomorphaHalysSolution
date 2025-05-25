using HalyomorphaHalys.UWP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace HalyomorphaHalys.UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        private async Task<Geopoint> GetUserLocationAsync()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            if (accessStatus == GeolocationAccessStatus.Allowed)
            {
                var geolocator = new Geolocator { DesiredAccuracy = PositionAccuracy.Default };
                var position = await geolocator.GetGeopositionAsync();
                return position.Coordinate.Point;
            }

            // Erişim verilmediyse varsayılan konum (Ankara)
            return new Geopoint(new BasicGeoposition
            {
                Latitude = 39.9208,
                Longitude = 32.8541
            });
        }


        private async void DrawDensityHeatMap()
        {
            var data = await BugDensityService.GetDensityDataAsync();
            MapOverlayCanvas.Children.Clear();

            foreach (var item in data)
            {
                var geoPoint = new Geopoint(new BasicGeoposition
                {
                    Latitude = (double)item.NotificationLatitude,
                    Longitude = (double)item.NotificationLongitude
                });

                Point pointOnMap;
                MyMap.GetOffsetFromLocation(geoPoint, out pointOnMap);

                int count = (int)item.NotificationCount;

                // Renk seçimi
                SolidColorBrush brush;
                if (count >= 8)
                    brush = new SolidColorBrush(Colors.Red);
                else if (count >= 4)
                    brush = new SolidColorBrush(Colors.Orange);
                else
                    brush = new SolidColorBrush(Colors.LightGreen);

                // Boyut (min 20, max 80)
                double size = Math.Min(80, 20 + count * 10);

                var ellipse = new Ellipse
                {
                    Width = size,
                    Height = size,
                    Fill = brush,
                    Opacity = 0.5,
                    Stroke = new SolidColorBrush(Colors.Red),
                    StrokeThickness = 1
                };

                // Konumlandır
                Canvas.SetLeft(ellipse, pointOnMap.X - size / 2);
                Canvas.SetTop(ellipse, pointOnMap.Y - size / 2);

                MapOverlayCanvas.Children.Add(ellipse);
            }
        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var userLocation = await GetUserLocationAsync();
            MyMap.Center = userLocation;
            MyMap.ZoomLevel = 10;

            MyMap.Loaded += async (s, ev) =>
            {
                await Task.Delay(300); // küçük bekleme ile çizim garantilenir
                DrawDensityHeatMap();
            };

            MyMap.ActualCameraChanged += MyMap_ActualCameraChanged;
        }

        private void MyMap_ActualCameraChanged(MapControl sender, MapActualCameraChangedEventArgs args)
        {
            _ = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                DrawDensityHeatMap();
            });
        }

        private async void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(300); // harita tam render olsun
            DrawDensityHeatMap();
        }
    }
}
