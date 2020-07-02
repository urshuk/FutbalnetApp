using FutbalnetApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FutbalnetApp.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		public bool NotificationsSet { get; set; }
		private bool darkModeAuto;
		public bool DarkModeAuto
		{
			get => darkModeAuto;
			set
			{
				SetProperty(ref darkModeAuto, value);
				SetDarkModeSettings();
			}
		}
		private bool darkMode;
		public bool DarkMode
		{
			get => darkMode;
			set
			{
				SetProperty(ref darkMode, value);
				SetDarkModeSettings();
			}
		}
		public bool AdsSet { get; set; }
		public bool IsDarkModeInitial { get; set; }
		public string AppVersion => VersionTracking.CurrentVersion;
		public int NotificationMinutesAhead { get; set; }
		public List<int> MinuteOptions { get; set; }
		public Command SaveSettingsCommand { get; set; }
		public Command LoadSettingsCommand { get; set; }

		public SettingsViewModel()
		{
			ExecuteLoadSettingsCommand();
			SaveSettingsCommand = new Command(() => ExecuteSaveSettingsCommand());
			LoadSettingsCommand = new Command(() => ExecuteLoadSettingsCommand());
			PopulateMinutesList();
		}

		void PopulateMinutesList()
		{
			MinuteOptions = new List<int>();
			int i = 0;
			while (i <= 120)
			{
				MinuteOptions.Add(i);
				i += 5;
			}
		}

		void SetDarkModeSettings()
		{
			if (DarkModeAuto)
			{
				LocalDataStore.SetDarkModeSettings(OSAppTheme.Unspecified);
				Application.Current.UserAppTheme = OSAppTheme.Unspecified;
			}
			else if (DarkMode)
			{
				LocalDataStore.SetDarkModeSettings(OSAppTheme.Dark);
				Application.Current.UserAppTheme = OSAppTheme.Dark;
			}
			else
			{
				LocalDataStore.SetDarkModeSettings(OSAppTheme.Light);
				Application.Current.UserAppTheme = OSAppTheme.Light;
				IsDarkModeInitial = false;
			}
		}
		void LoadDarkModeSettings()
		{
			OSAppTheme theme = LocalDataStore.GetDarkModeSettings();
			switch (theme)
			{
				case OSAppTheme.Unspecified:
					DarkModeAuto = true;
					break;
				case OSAppTheme.Light:
					DarkModeAuto = false;
					DarkMode = false;
					break;
				case OSAppTheme.Dark:
					DarkModeAuto = false;
					DarkMode = true;
					IsDarkModeInitial = true;
					break;
				default:
					break;
			}
		}
		void ExecuteSaveSettingsCommand()
		{
			LocalDataStore.SetNotificationsMinutes(NotificationMinutesAhead);
			LocalDataStore.SetNotificationsSettings(NotificationsSet);
			LocalDataStore.SetAdsSettings(AdsSet);
			MessagingCenter.Send(this, "SettingsUpdated");

		}
		void ExecuteLoadSettingsCommand()
		{
			bool isNotificationEnabled = DependencyService.Get<IDeviceService>().GetApplicationNotificationSettings();
			NotificationsSet = isNotificationEnabled && LocalDataStore.GetNotificationsSettings();
			NotificationMinutesAhead = LocalDataStore.GetNotificationsMinutes();
			AdsSet = LocalDataStore.GetAdsSettings();
			LoadDarkModeSettings();
		}
	}
}
