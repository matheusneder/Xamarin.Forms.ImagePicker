using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.iOS;

namespace Xamarin.Forms.ImagePicker.iOS
{
    public class ImageSourceUtility : IImageSourceUtility
    {
        public async Task<Stream> ToJpegStreamAsync(ImageSource imageSource)
        {
            if (imageSource == null)
            {
                throw new ArgumentNullException(nameof(imageSource));
            }

            if (imageSource is StreamImageSource)
            {
                var imageStream = new StreamImagesourceHandler();
                var image = await imageStream.LoadImageAsync(imageSource);
                return image.AsJPEG().AsStream();
            }
            else
            {
                throw new InvalidOperationException($"The type of the given imageSource is '{imageSource.GetType().Name}', but 'StreamImageSource' were expected.");
            }
        }
    }
}