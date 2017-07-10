# Xamarin.Forms.ImagePicker

Simple ImagePicker (camera and gallery) with cropper for Xamarin.Forms.

### Usage

Inject `IImagePickerService` using your favorite container. Service implamentation register is already done by Xamarin.Forms.DependencyAttribute, I tested using Unity container and it works fine with constructor parameter injection. You may also resolve with `Xamarin.Forms.DependencyService.Get<IImagePickerService>()`.

Example:

```cs
class ViewModel : INotifyPropertyChanged
{
  public event PropertyChangedEventHandler PropertyChanged;
  private Xamarin.Forms.ImageSource _imageSource;

  public Xamarin.Forms.ImageSource ImageSource 
  { 
    get { return _imageSource; }
    set
    {
      _imageSource = value;
      PropertyChanged?.Invoke(nameof(ImageSource));
    }
  }
  
  async void PickImage() 
  {
    // Take service implementation (you may use constructor parameter aproach)
    IImagePickerService imagePickerService = Xamarin.Forms.DependencyService.Get<IImagePickerService>();
    
    // Pick the image
    ImageSource = await imagePickerService.PickAsync();
  }
}
```

On View:

```xml
<Image Source="{Binding ImageSource}" />
```

Get JPEG stream in order to save on filesystem or send over network:

```cs
Stream stream = imagePickerService.ImageSourceUtility.ToJpegStreamAsync(imageSource);
```
