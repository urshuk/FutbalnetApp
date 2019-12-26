using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FutbalnetApp.Services;
using FutbalnetApp.Views;
using System.Collections.Generic;
using FutbalnetApp.Models;
using System.Linq;

namespace FutbalnetApp
{
	public partial class App : Application
	{
		//TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
		//To debug on Android emulators run the web backend against .NET Core not IIS
		//If using other emulators besides stock Google images you may need to adjust the IP address
		public static IEnumerable<Season> Seasons { get; set; }
		public static string AzureBackendUrl =
			DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
		public static string SportnetApiUrl = "https://futbalnet.sportnet.online/api";

		//roadmap
		//club players
		//timeline performance
		//notifications improve
		//loading
		//green lines

		public App()
		{
			InitializeComponent();

			DependencyService.Register<SportnetDataStore>();
			DependencyService.Register<LocalDataStore>();
			MainPage = new AppShell();
		}

		protected override void OnStart()
		{
			base.OnStart();
			ISportnetDataStore SportnetStore = DependencyService.Get<ISportnetDataStore>();
			Seasons = SportnetStore.GetSeasons();

			Theme theme = DependencyService.Get<IEnvironment>().GetOperatingSystemTheme();

			SetTheme(theme);
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			base.OnResume();

			Theme theme = DependencyService.Get<IEnvironment>().GetOperatingSystemTheme();

			SetTheme(theme);
		}

		void SetTheme(Theme theme)
		{
			if (theme == Theme.Dark)
			{
				Application.Current.Resources["BackgroundColor"] = Application.Current.Resources["BackgroundColorDark"];
				Application.Current.Resources["AlternativeColor"] = Application.Current.Resources["AlternativeColorDark"];
				//Application.Current.Resources["DarkAlternativeColor"] = Application.Current.Resources["DarkAlternativeColorDark"];
				Application.Current.Resources["TextColor"] = Application.Current.Resources["TextColorDark"];
			}
			else
			{
				Application.Current.Resources["BackgroundColor"] = Application.Current.Resources["BackgroundColorLight"];
				Application.Current.Resources["AlternativeColor"] = Application.Current.Resources["AlternativeColorLight"];
				//Application.Current.Resources["DarkAlternativeColor"] = Application.Current.Resources["DarkAlternativeColorLight"];
				Application.Current.Resources["TextColor"] = Application.Current.Resources["TextColorLight"];
			}
		}
	}
}
