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
	public class ClubDetailViewModel : BaseViewModel
	{
		public int ClubId { get; set; }
		public Club club;
		public Club Club
		{
			get => club;
			set => SetProperty(ref club, value);
		}
		public Command LoadClubCommand { get; set; }
		public Command ToggleFavouriteCommand { get; set; }
		public ObservableCollection<Team> Teams { get; set; }
		public bool isFavourite;
		public bool IsFavourite
		{
			get => isFavourite;
			set => SetProperty(ref isFavourite, value);
		}

		public ClubDetailViewModel(int id)
		{
			ClubId = id;
			Teams = new ObservableCollection<Team>();
			LoadClubCommand = new Command(async () => await ExecuteLoadClubCommand());
			ToggleFavouriteCommand = new Command(() => ExecuteToggleFavouriteCommand());
			IsFavourite = LocalDataStore.ClubExists(ClubId);
		}

		void ExecuteToggleFavouriteCommand()
		{
			if (IsFavourite)
			{
				LocalDataStore.DeleteClub(ClubId);
				IsFavourite = false;
			}
			else
			{
				LocalDataStore.SaveClub(Club);
				IsFavourite = true;
			}
		}
		async Task ExecuteLoadClubCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Club = await SportnetStore.GetClubAsync(ClubId);
				Title = Club.Name;
				var teams = await SportnetStore.GetClubTeamsAsync(ClubId);
				Teams.Clear();
				foreach (var item in teams)
				{
					if (item.Season.IsActual)
					{
						Teams.Add(item);
					}
				}

				IsLoaded = true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				var log = new ErrorLog()
				{
					Exception = ex,
					Object = Club,
					ObjectId = ClubId,
					Action = "Loading Club",
					Datetime = DateTime.Now,
				};
				await LogError(log);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
