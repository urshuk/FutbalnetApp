using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using FutbalnetApp.iOS;
using FutbalnetApp.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceService))]
namespace FutbalnetApp.iOS
{
	public class DeviceService : IDeviceService
	{
		public bool GetApplicationNotificationSettings()
		{
			var settings = UIApplication.SharedApplication.CurrentUserNotificationSettings.Types;
			return settings != UIUserNotificationType.None;
		}
	}
}