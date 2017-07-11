using System.Threading;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.iOS.CameraViewController;

[assembly: Dependency(typeof(Xamarin.Forms.ImagePicker.iOS.ImagePickerService))]
namespace Xamarin.Forms.ImagePicker.iOS
{
    public class ImagePickerService : IImagePickerService
    {
        public IImageSourceUtility ImageSourceUtility => new ImageSourceUtility();

        private UIViewController GetTheMostPresentedViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var theMostPresentedViewController = window.RootViewController;

            while (theMostPresentedViewController.PresentedViewController != null)
            {
                theMostPresentedViewController = theMostPresentedViewController.PresentedViewController;
            }

            return theMostPresentedViewController;
        }

        private ImageSource _pickAsyncResult;
        private EventWaitHandle _waitHandle = new AutoResetEvent(false);

        public Task<ImageSource> PickImageAsync()
        {
            _pickAsyncResult = null;
            var viewController = GetTheMostPresentedViewController();

            var cameraViewController = new CameraViewController(true, true,
                (image, asset) =>
                {
                    if (image != null)
                    {
                        _pickAsyncResult = ImageSource.FromStream(() => image.AsPNG().AsStream());
                    }

                    viewController.DismissViewController(true, null);
                    _waitHandle.Set();
                });

            viewController.PresentViewController(cameraViewController, true, null);

            return Task.Run(() =>
            {
                _waitHandle.WaitOne();
                var result = _pickAsyncResult;
                _pickAsyncResult = null;

                return result;
            });
        }
    }
}