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
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            image.Source = await _imagePickerService.PickAsync();
        }
    }
}