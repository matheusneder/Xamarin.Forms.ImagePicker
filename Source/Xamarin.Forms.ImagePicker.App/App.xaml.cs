using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.ImagePicker.App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }
    }
}