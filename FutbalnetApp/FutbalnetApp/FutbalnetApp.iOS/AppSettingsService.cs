using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using FutbalnetApp.iOS;
using FutbalnetApp.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppSettingsService))]
namespace FutbalnetApp.iOS
{
	public class AppSettingsService : IAppSettingsService
	{
		public void OpenAppSettings()
		{
			var url = new NSUrl($"app-settings:");
			UIApplication.SharedApplication.OpenUrl(url);
		}
	}
}