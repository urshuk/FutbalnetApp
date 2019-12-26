using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutbalnetApp.ViewModels
{
	public class FavouritesViewModel : BaseViewModel
	{
		public Command LoadFavouritesCommand { get; set; }
		public List<GenericGroup> favourites;
		public List<GenericGroup> Favourites
		{
			get => favourites;
			set => SetProperty(ref favourites, value);
		}

		public FavouritesViewModel()
		{
			LoadFavouritesCommand = new Command(() => ExecuteLoadFavouritesCommand());
		}

		void ExecuteLoadFavouritesCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var clubs = LocalDataStore.GetClubs();
				var teams = LocalDataStore.GetTeams();
				var comps = LocalDataStore.GetCompetitions();

				Favourites = new List<GenericGroup>
				{
					new GenericGroup("Ligy", comps),
					new GenericGroup("Kluby", clubs),
					new GenericGroup("Tímy", teams),
				};

				IsLoaded = true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				var log = new ErrorLog()
				{
					Exception = ex,
					Object = null,
					ObjectId = 0,
					Action = "Loading Favourites",
					Datetime = DateTime.Now,
				};
				LogError(log);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
