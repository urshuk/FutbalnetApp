using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using FutbalnetApp.Models;
using FutbalnetApp.Services;

namespace FutbalnetApp.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public ISportnetDataStore SportnetStore => DependencyService.Get<ISportnetDataStore>();
		public ILocalDataStore LocalDataStore => DependencyService.Get<ILocalDataStore>();

		bool isBusy = false;
		public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}

		bool isLoaded = false;
		public bool IsLoaded
		{
			get { return isLoaded; }
			set { SetProperty(ref isLoaded, value); }
		}

		string title = string.Empty;
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

		public void LogError(ErrorLog log)
		{

		}

		protected bool SetProperty<T>(ref T backingStore, T value,
			[CallerMemberName]string propertyName = "",
			Action onChanged = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return false;

			backingStore = value;
			onChanged?.Invoke();
			OnPropertyChanged(propertyName);
			return true;
		}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			var changed = PropertyChanged;
			if (changed == null)
				return;

			changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
