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
        /// <returns>Picked image as ImageSource.</returns>
        Task<ImageSource> PickAsync();

        /// <summary>
        /// ImageSource utility.
        /// </summary>
        IImageSourceUtility ImageSourceUtility { get; }
    }
}
