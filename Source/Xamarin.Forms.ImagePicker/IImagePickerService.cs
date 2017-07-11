using System.Threading.Tasks;

namespace Xamarin.Forms.ImagePicker
{
    /// <summary>
    /// Represents Image Picker component
    /// </summary>
    public interface IImagePickerService
    {
        /// <summary>
        /// Pick a image from camera or gallery.
        /// </summary>
        /// <returns>Picked image as ImageSource or null when user cancel.</returns>
        Task<ImageSource> PickImageAsync();

        /// <summary>
        /// ImageSource utility.
        /// </summary>
        IImageSourceUtility ImageSourceUtility { get; }
    }
}
