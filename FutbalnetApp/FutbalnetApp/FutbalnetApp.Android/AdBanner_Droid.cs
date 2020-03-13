using Android.Content;
using Android.Gms.Ads;
using Android.Widget;
using FutbalnetApp.Droid;
using FutbalnetApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdBanner), typeof(AdBanner_Droid))]
namespace FutbalnetApp.Droid
{
	public class AdBanner_Droid : ViewRenderer<AdBanner, AdView>
	{
		Context context;
		public AdBanner_Droid(Context _context) : base(_context)
		{
			context = _context;
		}
		protected override void OnElementChanged(ElementChangedEventArgs<AdBanner> e)
		{
			base.OnElementChanged(e);
			if (Control == null)
			{
				var adView = new AdView(Context);
				switch ((Element as AdBanner).Size)
				{
					case AdBanner.Sizes.Standardbanner:
						adView.AdSize = AdSize.Banner;
						break;
					case AdBanner.Sizes.LargeBanner:
						adView.AdSize = AdSize.LargeBanner;
						break;
					case AdBanner.Sizes.MediumRectangle:
						adView.AdSize = AdSize.MediumRectangle;
						break;
					case AdBanner.Sizes.FullBanner:
						adView.AdSize = AdSize.FullBanner;
						break;
					case AdBanner.Sizes.Leaderboard:
						adView.AdSize = AdSize.Leaderboard;
						break;
					case AdBanner.Sizes.SmartBannerPortrait:
						adView.AdSize = AdSize.SmartBanner;
						break;
					default:
						adView.AdSize = AdSize.Banner;
						break;
				}
				adView.AdUnitId = "ca-app-pub-7533314111096448/5729497657"; //production id
				//adView.AdUnitId = "ca-app-pub-3940256099942544/6300978111"; //test id

				var adParams = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
				adView.LayoutParameters = adParams;

				var requestbuilder = new AdRequest.Builder().AddTestDevice("CBAA70154B5FA91A7E790F550320D04C");
				adView.LoadAd(requestbuilder.Build());
				SetNativeControl(adView);
			}
		}
	}
}