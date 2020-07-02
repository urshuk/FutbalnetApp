using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using FutbalnetApp.iOS;
using FutbalnetApp.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using static FutbalnetApp.Views.StatusBar;

[assembly: ExportRenderer(typeof(Page), typeof(StatusBarRendrerer))]
namespace FutbalnetApp.iOS
{
	public class StatusBarRendrerer : PageRenderer
	{
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			SetStatusBarStyle(StatusBar.GetStatusBarStyle(Element));
		}

		private void SetStatusBarStyle(StatusBarStyle statusBarStyle)
		{
			switch (statusBarStyle)
			{
				case StatusBarStyle.DarkText:
					UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.DarkContent, true);
					UIApplication.SharedApplication.SetStatusBarHidden(false, true);
					break;
				case StatusBarStyle.WhiteText:
					UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, true);
					UIApplication.SharedApplication.SetStatusBarHidden(false, true);
					break;
				case StatusBarStyle.Hidden:
					UIApplication.SharedApplication.SetStatusBarHidden(true, true);
					break;
				case StatusBarStyle.Default:
				default:
					UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.Default, true);
					UIApplication.SharedApplication.SetStatusBarHidden(false, true);
					break;
			}

			SetNeedsStatusBarAppearanceUpdate();
		}
	}
}