## Xamarin.Forms.ImagePicker

Simple ImagePicker (camera and gallery) with cropper for Xamarin.Forms.

### Installation

Install [Xamarin.Forms.ImagePicker](https://www.nuget.org/packages/Xamarin.Forms.ImagePicker/) NuGet package on Portable/Shared project, Android project and iOS project (same package for both forms and platform projects).

#### Android project

Edit `Properties/AndroidManifest.xml` by adding permissions for camera and storage:

```xml
<manifest ... >
    <!-- Add permissions -->
    <uses-permission android:name="android.permission.READ_EXTERNAL_STORAGE" />
    <uses-permission android:name="android.permission.CAMERA" />
    ...
</manifest>
```

#### iOS project

Edit `Info.plist`:

```xml
<plist version="1.0">
  <dict>
    ... add this entries to dict element
    <key>NSCameraUsageDescription</key>
    <string>A custom message (will be shown to the user when iOS ask him for permission to access camera).</string>
    <key>NSPhotoLibraryUsageDescription</key>
    <string>A custom message (will be shown to the user when iOS ask him for permission to access photo library).</string>
    ...
  </dict>
</plist> 
```

Add this assembly decoration to `Properties/AssemblyInfo.cs`:

```cs
// This is necessary in order to the linker don't discard iOS ImagePickerService implementation
[assembly: Preserve(typeof(Xamarin.Forms.ImagePicker.iOS.ImagePickerService), AllMembers = true)]
```

### Usage

Inject `IImagePickerService` using your favorite container. Service implementation register is already done by Xamarin.Forms.DependencyAttribute, I tested using Unity container and it works fine with constructor parameter injection. You may also resolve with `Xamarin.Forms.DependencyService.Get<IImagePickerService>()`.

Example:

```cs
class ViewModel : System.ComponentModel.INotifyPropertyChanged
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
    // Get service (recommended to use constructor parameter aproach instead)
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

Get JPEG stream to save on filesystem or send over network: 

```cs
using(System.IO.Stream stream = await imagePickerService.ImageSourceUtility.ToJpegStreamAsync(imageSource))
{
    // ...
}
```
