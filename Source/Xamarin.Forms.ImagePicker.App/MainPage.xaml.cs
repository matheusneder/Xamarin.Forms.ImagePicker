using System;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.ImagePicker.App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private IImagePickerService _imagePickerService;

        public MainPage()
        {
            _imagePickerService = DependencyService.Get<IImagePickerService>();

            if(_imagePickerService == null)
            {
                throw new InvalidOperationException(
                    "Could not resolve IImagePickerService dependecy! " +
                    "On iOS there is scenarios that DependencyAttribute registration didn't work. "+
                    "To fix you may do a manual registration "+
                    "by calling Xamarin.Forms.DependencyService.Register<Xamarin.Forms.ImagePicker.iOS.ImagePickerService>(); " +
                    "on AppDelegate between Forms.Init(); and LoadApplication(...) calls.");
            }

            InitializeComponent();
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            var imageSource = await _imagePickerService.PickImageAsync();

            if (imageSource != null) // it will be null when user cancel
            {
                image.Source = imageSource;
            }
        }
    }
}