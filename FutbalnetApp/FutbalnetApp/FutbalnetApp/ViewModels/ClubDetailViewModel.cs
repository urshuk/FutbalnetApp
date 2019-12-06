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
		public ObservableCollection<Team> Teams { get; set; }

		public ClubDetailViewModel(int id)
		{
			ClubId = id;
			Teams = new ObservableCollection<Team>();
			LoadClubCommand = new Command(async () => await ExecuteLoadClubCommand());
		}

		async Task ExecuteLoadClubCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Club = await SportnetStore.GetClubAsync(ClubId);
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
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
