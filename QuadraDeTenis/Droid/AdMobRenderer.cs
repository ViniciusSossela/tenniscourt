using System;
using QuadraDeTenis;
using QuadraDeTenis.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobRenderer))]
namespace QuadraDeTenis.Droid
{
	public class AdMobRenderer : ViewRenderer<AdMobView, Android.Gms.Ads.AdView>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				var ad = new Android.Gms.Ads.AdView(Forms.Context);
				ad.AdSize = Android.Gms.Ads.AdSize.Banner;
				ad.AdUnitId = "ca-app-pub-1460185125045371/6114685443";

				var requestbuilder = new Android.Gms.Ads.AdRequest.Builder();
				ad.LoadAd(requestbuilder.Build());

				SetNativeControl(ad);
			}
		}
	}
}
