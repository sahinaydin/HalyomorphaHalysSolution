using HalyomorphaHalys.UWP.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace HalyomorphaHalys.UWP.ViewModels
{
    public class ClassifierViewModel : BaseViewModel
    {
        public ICommand CapturePhotoCommand { get; }
        public ICommand ClassifyCommand { get; }

        private BitmapImage _capturedImage;
        public BitmapImage CapturedImage
        {
            get => _capturedImage;
            set => SetProperty(ref _capturedImage, value);
        }

        private bool _isPhotoCaptured;
        public bool IsPhotoCaptured
        {
            get => _isPhotoCaptured;
            set => SetProperty(ref _isPhotoCaptured, value);
        }

        private double _classificationProgress;
        public double ClassificationProgress
        {
            get => _classificationProgress;
            set => SetProperty(ref _classificationProgress, value);
        }

        private string _classificationResult;
        public string ClassificationResult
        {
            get => _classificationResult;
            set => SetProperty(ref _classificationResult, value);
        }

        private Brush _resultBackground;
        public Brush ResultBackground
        {
            get => _resultBackground;
            set => SetProperty(ref _resultBackground, value);
        }

        public ICommand PickImageCommand { get; }

        private StorageFile _photoFile;

        public ClassifierViewModel()
        {
            CapturePhotoCommand = new RelayCommand(async _ => await CapturePhotoAsync());
            ClassifyCommand = new RelayCommand(async _ => await ClassifyAsync(), _ => IsPhotoCaptured);
            PickImageCommand = new RelayCommand(async _ => await PickImageAsync());
        }

        private async Task CapturePhotoAsync()
        {
            var picker = new Windows.Media.Capture.CameraCaptureUI();
            picker.PhotoSettings.Format = Windows.Media.Capture.CameraCaptureUIPhotoFormat.Jpeg;
            picker.PhotoSettings.CroppedSizeInPixels = new Windows.Foundation.Size(300, 300);

            _photoFile = await picker.CaptureFileAsync(Windows.Media.Capture.CameraCaptureUIMode.Photo);

            if (_photoFile != null)
            {
                var image = new BitmapImage();
                using (var stream = await _photoFile.OpenAsync(FileAccessMode.Read))
                {
                    await image.SetSourceAsync(stream);
                }
                CapturedImage = image;
                IsPhotoCaptured = true;
                (ClassifyCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        private async Task ClassifyAsync()
        {
            ClassificationProgress = 0;
            ClassificationResult = "Sınıflandırma başlatılıyor...";
            ResultBackground = null;

            for (int i = 1; i <= 5; i++)
            {
                await Task.Delay(100);
                ClassificationProgress = i * 10;
            }

            var result = await Services.ClassifierService.ClassifyImageAsync(_photoFile);

            ClassificationProgress = 100;

            if (result != null)
            {
                ClassificationResult = $"🪲 Tahmin Edilen Tür: {result.Label}";

                if (result.Label?.ToLower().Contains("halys") == true)
                {
                    ResultBackground = (Brush)Application.Current.Resources["ResultRedBrush"];
                }
                else
                {
                    ResultBackground = (Brush)Application.Current.Resources["ResultGreenBrush"];
                }
            }
            else
            {
                ClassificationResult = "❌ Sınıflandırma başarısız oldu.";
                ResultBackground = (Brush)Application.Current.Resources["ResultYellowBrush"];
            }
        }

        public async Task PickImageAsync()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");

            var file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                _photoFile = file;
                var image = new BitmapImage();
                using (var stream = await file.OpenAsync(FileAccessMode.Read))
                {
                    await image.SetSourceAsync(stream);
                }
                CapturedImage = image;
                IsPhotoCaptured = true;
                (ClassifyCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

    }
}
