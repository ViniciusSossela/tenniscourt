using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarinos.AdMob.Forms.Android;
using Xamarinos.AdMob.Forms;

namespace QuadraDeTenis.Droid
{
	[Activity(Label = "Quadras de Tenis", Icon = "@drawable/tenniscourticon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();


			//AdBannerRenderer.Init();
			//CrossAdmobManager.Init("ca-app-pub-1460185125045371/5695883046");
			//CrossAdmobManager.Init("ca-app-pub-1460185125045371/6114685443");




			LoadApplication(new App());


		}
	}
}
