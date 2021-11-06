using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ZXing.Net.Mobile.Android;
using Color = Android.Graphics.Color;
using Platform = Xamarin.Essentials.Platform;

namespace Bitad2021.Android
{
    [Activity(Label = "Bitad2021", Theme = "@style/MainTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Platform.Init(Application);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            Forms.Init(this, savedInstanceState);
            //TODO: Delete statusbar bg on ios and andriod / set it to black or whatever main theme is
            //here is the code to change color
            Window?.SetStatusBarColor(Color.Argb(0, 255, 255, 255));

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            Permission[] grantResults)
        {
            PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}