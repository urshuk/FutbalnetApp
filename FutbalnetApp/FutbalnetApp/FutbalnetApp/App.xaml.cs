using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FutbalnetApp.Services;
using FutbalnetApp.Views;
using System.Collections.Generic;
using FutbalnetApp.Models;
using System.Linq;
using System.Threading.Tasks;
using FutbalnetApp.ViewModels;
using Xamarin.Forms.PlatformConfiguration;

namespace FutbalnetApp
{
	public partial class App : Application
	{
		//TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
		//To debug on Android emulators run the web backend against .NET Core not IIS
		//If using other emulators besides stock Google images you may need to adjust the IP address
		public static IEnumerable<Season> Seasons { get; set; }

		public static string SportnetApiUrl = "https://futbalnet.sportnet.online/api";
		public static string FutbalVilleApiUrl = "https://api.futbalville.sk/";
		bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

		//roadmap
		//club players
		//timeline performance
		//notifications improve
		//UI design
		//movements
		//green lines

		public App()
		{
			InitializeComponent();

			DependencyService.Register<SportnetDataStore>();
			DependencyService.Register<DatabaseStore>();
			DependencyService.Register<LocalDataStore>();
			if (Device.RuntimePlatform == Device.iOS)
			{
				MainPage = new AppShell();
			}
		}

		protected override async void OnStart()
		{
			base.OnStart();

			await CheckConnectionAsync();

			MainPage = new AppShell();

			SetDarkMode();

			ISportnetDataStore SportnetStore = DependencyService.Get<ISportnetDataStore>();
			Seasons = SportnetStore.GetSeasons();

			SetNotifications();
			MessagingCenter.Subscribe<SettingsViewModel>(this, "SettingsUpdated", (sender) => SetNotifications());
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override async void OnResume()
		{
			base.OnResume();
			SetDarkMode();
			await CheckConnectionAsync();
		}

		void SetNotifications()
		{
			ILocalDataStore LocalDataStore = DependencyService.Get<ILocalDataStore>();

			if (!LocalDataStore.GetNotificationsSettings())
				return;

			var minutesAhead = LocalDataStore.GetNotificationsMinutes();
			var message = minutesAhead > 0 ? $"Začiatok zápasu o {minutesAhead} min." : "Začiatok zápasu.";
			var notifications = LocalDataStore.GetNotificationMatches();

			foreach (var not in notifications)
			{
				Plugin.LocalNotifications.CrossLocalNotifications.Current.Cancel(not.MatchId);
			}
			foreach (var not in notifications)
			{
				if (not.DateTime.HasValue)
					Plugin.LocalNotifications.CrossLocalNotifications.Current.Show(not.MatchName, message, not.MatchId, not.DateTime.Value.AddMinutes(-minutesAhead));
			}
		}
		async Task CheckConnectionAsync()
		{
			if (!IsConnected)
			{
				await MainPage.DisplayAlert("Žiadny prístup k internetu", "Skontrolujte vaše internetové pripojenie a skúste znova.", "OK");
				await CheckConnectionAsync();
			}
		}
		void SetDarkMode()
		{
			ILocalDataStore LocalStore = DependencyService.Get<ILocalDataStore>();
			OSAppTheme theme = LocalStore.GetDarkModeSettings();
			switch (theme)
			{
				case OSAppTheme.Unspecified:
					Application.Current.UserAppTheme = OSAppTheme.Unspecified;
					break;
				case OSAppTheme.Light:
					Application.Current.UserAppTheme = OSAppTheme.Light;
					break;
				case OSAppTheme.Dark:
					Application.Current.UserAppTheme = OSAppTheme.Dark;
					break;
				default:
					break;
			}
		}
	}
}
