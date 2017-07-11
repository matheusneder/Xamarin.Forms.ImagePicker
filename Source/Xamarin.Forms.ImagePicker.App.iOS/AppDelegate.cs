using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace Xamarin.Forms.ImagePicker.App.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Forms.Init();

            // Xamarin.Forms.DependencyService didnt work for DependencyAttribute registration.
            DependencyService.Register<ImagePicker.iOS.ImagePickerService>();

            LoadApplication(new App());
            return base.FinishedLaunching(application, launchOptions);
        }
    }
}


