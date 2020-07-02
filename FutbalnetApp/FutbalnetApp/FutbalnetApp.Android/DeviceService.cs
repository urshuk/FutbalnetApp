using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using FutbalnetApp.Droid;
using FutbalnetApp.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(DeviceService))]
namespace FutbalnetApp.Droid
{
	public class DeviceService : IDeviceService
	{
		public bool GetApplicationNotificationSettings()
		{
			var nm = NotificationManagerCompat.From(Android.App.Application.Context);
			bool enabled = nm.AreNotificationsEnabled();
			return enabled;
		}
	}
}