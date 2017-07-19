## Xamarin.Forms.ImagePicker

Simple ImagePicker (camera and gallery) with cropper for Xamarin.Forms (Android and iOS). It bindings native components [ALCameraViewController](https://github.com/AlexLittlejohn/ALCameraViewController) (iOS) and [Android Image Cropper](https://github.com/ArthurHub/Android-Image-Cropper) (Android).

### Installation

Install [Xamarin.Forms.ImagePicker](https://www.nuget.org/packages/Xamarin.Forms.ImagePicker/) NuGet package on Portable/Shared, Android and iOS project (same package for both Xamarin.Forms and platform projects).

#### Android project

Edit `Properties/AssemblyInfo.cs` by adding permissions for camera and storage:

```cs
// Add permissions
[assembly: UsesPermission(Android.Manifest.Permission.Camera)]
[assembly: UsesPermission(Android.Manifest.Permission.ReadExternalStorage)]
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
    <!-- Set MinimumOSVersion to >= 8.0 -->
    <key>MinimumOSVersion</key>
    <string>8.0</string> 
  </dict>
</plist> 
```

Add this assembly decoration to `Properties/AssemblyInfo.cs`:

```cs
// This is necessary to tell the linker to don't discard iOS ImagePickerService implementation
[assembly: Preserve(typeof(Xamarin.Forms.ImagePicker.iOS.ImagePickerService), AllMembers = true)]
```

### Usage

Inject `IImagePickerService` using your favorite container. Service registration is already done by Xamarin.Forms.DependencyAttribute, I tested using Unity container and it works fine with constructor parameter injection. You may also resolve with `Xamarin.Forms.DependencyService.Get<IImagePickerService>()`.

#### Example using codebehind

Code:

```cs
public partial class MainPage : ContentPage
{
    IImagePickerService _imagePickerService;

    public MainPage()
    {
        _imagePickerService = DependencyService.Get<IImagePickerService>();
        InitializeComponent();
    }

    async void Button_Clicked(object sender, System.EventArgs e)
    {
        var imageSource = await _imagePickerService.PickImageAsync();

        if (imageSource != null) // it will be null when user cancel
        {
            image.Source = imageSource;
        }
    }
}
```

View:

```xml
<Image x:Name="image" />
<Button Clicked="Button_Clicked" Text="Pick Image!" />
```

#### Example using ViewModel (MVVM)

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

View:

```xml
<Image Source="{Binding ImageSource}" />
```

#### Handling image data

Get JPEG stream to save on filesystem or send over network: 

```cs
using(System.IO.Stream stream = await imagePickerService.ImageSourceUtility.ToJpegStreamAsync(imageSource))
{
    // ...
}
```

### Contribute

#### Build

Just clone the source and buid on Visual Studio (I used VS 15.2). The role of this project is to provide an uniform Xamarin.Forms API to access native libraries for iOS and Android ([ALCameraViewController](https://github.com/AlexLittlejohn/ALCameraViewController) and [Android Image Cropper](https://github.com/ArthurHub/Android-Image-Cropper)). Each native library need a bindings as a xamarin library in order to be acessible from Xamarin. I did this bindings separeted from this projects and it's available at: 
- [Xamarin.iOS.CameraViewController](https://github.com/matheusneder/Xamarin.iOS.CameraViewController)
- [Xamarin.Android.ImageCropper](https://github.com/matheusneder/Xamarin.Android.ImageCropper)
