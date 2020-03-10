using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FutbalnetApp.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		private int notificationMinutesAhead;

		public bool NotificationsSet { get; set; }
		public bool AdsSet { get; set; }
		public string AppVersion => VersionTracking.CurrentVersion;
		public int NotificationMinutesAhead
		{
			get => notificationMinutesAhead;
			set
			{
				notificationMinutesAhead = value;
				ExecuteSaveSettingsCommand();
			}
		}
		public Command SaveSettingsCommand { get; set; }
		public Command LoadSettingsCommand { get; set; }

		public SettingsViewModel()
		{
			ExecuteLoadSettingsCommand();
			SaveSettingsCommand = new Command(() => ExecuteSaveSettingsCommand());
			LoadSettingsCommand = new Command(() => ExecuteLoadSettingsCommand());
		}

		void ExecuteSaveSettingsCommand()
		{
			LocalDataStore.SetNotificationsMinutes(NotificationMinutesAhead);
			LocalDataStore.SetNotificationsSettings(NotificationsSet);
			LocalDataStore.SetAdsSettings(AdsSet);
		}
		void ExecuteLoadSettingsCommand()
		{
			NotificationsSet = LocalDataStore.GetNotificationsSettings();
			NotificationMinutesAhead = LocalDataStore.GetNotificationsMinutes();
			AdsSet = LocalDataStore.GetAdsSettings();
		}
	}
}
