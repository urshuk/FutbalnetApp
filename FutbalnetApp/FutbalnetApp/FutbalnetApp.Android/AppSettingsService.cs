using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FutbalnetApp.Droid;
using FutbalnetApp.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppSettingsService))]
namespace FutbalnetApp.Droid
{
	public class AppSettingsService : IAppSettingsService
	{
		public void OpenAppSettings()
		{
			var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
			intent.AddFlags(ActivityFlags.NewTask);
			string package_name = "com.vitvasak.FutbalVille";
			var uri = Android.Net.Uri.FromParts("package", package_name, null);
			intent.SetData(uri);
			Android.App.Application.Context.StartActivity(intent);
		}
	}
}