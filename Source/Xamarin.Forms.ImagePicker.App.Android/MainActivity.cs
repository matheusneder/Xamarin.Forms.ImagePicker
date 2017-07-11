using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace Xamarin.Forms.ImagePicker.App.Android
{
    [Activity(Label = "Image Picker", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

