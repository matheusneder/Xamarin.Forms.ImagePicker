using System;
using Android.App;
using Android.Content;
using System.Threading;
using System.Threading.Tasks;
using Java.Lang;
using Xamarin.Android.ImageCropper;
using Xamarin.Forms;

[assembly: Dependency(typeof(Xamarin.Forms.ImagePicker.Android.ImagePickerService))]
namespace Xamarin.Forms.ImagePicker.Android
{
    public class ImagePickerService : IImagePickerService
    {
        public IImageSourceUtility ImageSourceUtility => new ImageSourceUtility();

        private void StartActivity()
        {
            var currentActivity = Forms.Context as Activity;

            if (currentActivity != null)
            {
                var cropImageOptions = new CropImageOptions();
                cropImageOptions.MultiTouchEnabled = true;
                cropImageOptions.Guidelines = CropImageView.Guidelines.On;
                cropImageOptions.AspectRatioX = 1;
                cropImageOptions.AspectRatioY = 1;
                cropImageOptions.FixAspectRatio = true;
                cropImageOptions.Validate();
                var intent = new Intent();
                intent.SetClass(currentActivity, Class.FromType(typeof(ImagePickerOnResultActivity)));
                intent.PutExtra(CropImage.CropImageExtraSource, null as global::Android.Net.Uri); // Image Uri
                intent.PutExtra(CropImage.CropImageExtraOptions, cropImageOptions);
                currentActivity.StartActivity(intent);
            }
            else
            {
                throw new InvalidOperationException("Could not get current activity.");
            }
        }

        public Task<ImageSource> PickAsync()
        {
            StartActivity();

            return Task.Run(() =>
            {
                _waitHandle.WaitOne();
                var result = _pickAsyncResult;
                _pickAsyncResult = null;

                return result;
            });
        }

        private static ImageSource _pickAsyncResult;
        private static EventWaitHandle _waitHandle = new AutoResetEvent(false);

        [Activity(Theme = "@style/Base.Theme.AppCompat")]
        public class ImagePickerOnResultActivity : CropImageActivity
        {
            public override void OnCropImageComplete(CropImageView cropImageView, CropImageView.CropResult cropResult)
            {
                var resultImageUri = new Uri(cropResult.Uri.ToString());
                _pickAsyncResult = ImageSource.FromFile(resultImageUri.LocalPath);
                base.OnCropImageComplete(cropImageView, cropResult);
                _waitHandle.Set();
            }
        }
    }
}