using System;
using System.IO;
using System.Threading.Tasks;

namespace Xamarin.Forms.ImagePicker.Android
{
    public class ImageSourceUtility : IImageSourceUtility
    {
        public Task<Stream> ToJpegStreamAsync(ImageSource imageSource)
        {
            if (imageSource == null)
            {
                throw new ArgumentNullException(nameof(imageSource));
            }

            if (imageSource is FileImageSource)
            {
                return Task.Run(() => File.Open((imageSource as FileImageSource).File, FileMode.Open) as Stream);
            }
            else
            {
                throw new InvalidOperationException($"The type of the given imageSource is '{imageSource.GetType().Name}', but 'FileImageSource' were expected.");
            }
        }
    }
}