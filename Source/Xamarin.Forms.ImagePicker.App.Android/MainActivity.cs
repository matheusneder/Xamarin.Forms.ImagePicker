using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace Xamarin.Forms.ImagePicker.App.Android
{
    [Activity(Label = "Xamarin.Forms.ImagePicker.App.Android", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Base.Theme.AppCompat")]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

