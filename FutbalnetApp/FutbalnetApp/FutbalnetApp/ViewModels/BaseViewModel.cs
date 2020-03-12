using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using FutbalnetApp.Models;
using FutbalnetApp.Services;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace FutbalnetApp.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		public ISportnetDataStore SportnetStore => DependencyService.Get<ISportnetDataStore>();
		public ILocalDataStore LocalDataStore => DependencyService.Get<ILocalDataStore>();

		bool isBusy = false;
		public bool IsBusy
		{
			get => isBusy;
			set => SetProperty(ref isBusy, value);
		}

		bool isLoaded = false;
		public bool IsLoaded
		{
			get => isLoaded;
			set => SetProperty(ref isLoaded, value);
		}

		bool adsEnabled = false;
		public bool AdsEnabled
		{
			get => adsEnabled;
			set => SetProperty(ref adsEnabled, value);
		}

		string title = string.Empty;
		public string Title
		{
			get => title;
			set => SetProperty(ref title, value);
		}

		public async Task LogError(ErrorLog log)
		{
			//var client = new MongoClient("mongodb+srv://mongoAdmin:mongoDraslik1598@freecluster-aqcdc.azure.mongodb.net/test?retryWrites=true&w=majority");
			//var database = client.GetDatabase("Logging");
			//var collection = database.GetCollection<ErrorLog>("errors");
			//await collection.InsertOneAsync(log);
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
