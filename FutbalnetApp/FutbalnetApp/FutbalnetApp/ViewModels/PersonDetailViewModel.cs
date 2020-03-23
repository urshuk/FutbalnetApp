using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace FutbalnetApp.ViewModels
{
	public class PersonDetailViewModel : BaseViewModel
	{
		public int PersonId { get; set; }
		public Person person;
		public Person Person
		{
			get => person;
			set => SetProperty(ref person, value);
		}
		public string photo;
		public string Photo
		{
			get => photo;
			set => SetProperty(ref photo, value);
		}
		public int SelectedTabIndex { get; set; }
		public Command LoadPersonCommand { get; set; }
		int statsOrderIndex = 0;
		public int StatsOrderIndex
		{
			get => statsOrderIndex;
			set => SetProperty(ref statsOrderIndex, value);
		}
		public Command OrderStatsCommand { get; set; }
		public ObservableCollection<PlayerStatsSeason> OrderedStats { get; set; }
		public ObservableCollection<Transfer> Transfers { get; set; }
		public PlayerStatsSeason PlayerStatsSummary { get; set; }
		public List<PlayerStatsSeason> PlayerStatsSeasons { get; set; }


		public PersonDetailViewModel(int id)
		{
			PersonId = id;
			OrderedStats = new ObservableCollection<PlayerStatsSeason>();
			Transfers = new ObservableCollection<Transfer>();
			LoadPersonCommand = new Command(async () => await ExecuteLoadPersonCommand());
			OrderStatsCommand = new Command<string>((parameter) => ExecuteOrderStatsCommand(parameter));
		}

		private void ExecuteOrderStatsCommand(string by)
		{
			switch (by)
			{
				case "Goals":
					var goals = PlayerStatsSeasons.OrderByDescending(x => x.G);
					OrderedStats.Clear();
					OrderedStats.Add(PlayerStatsSummary);
					foreach (var item in goals)
					{
						OrderedStats.Add(item);
					};
					StatsOrderIndex = 0;
					break;
				case "Mins":
					var mins = PlayerStatsSeasons.OrderByDescending(x => x.Min);
					OrderedStats.Clear();
					OrderedStats.Add(PlayerStatsSummary);
					foreach (var item in mins)
					{
						OrderedStats.Add(item);
					};
					StatsOrderIndex = 1;
					break;
				case "Yellows":
					var yellows = PlayerStatsSeasons.OrderByDescending(x => x.YC);
					OrderedStats.Clear();
					OrderedStats.Add(PlayerStatsSummary);
					foreach (var item in yellows)
					{
						OrderedStats.Add(item);
					};
					StatsOrderIndex = 2;
					break;
				case "Reds":
					var reds = PlayerStatsSeasons.OrderByDescending(x => x.RC);
					OrderedStats.Clear();
					OrderedStats.Add(PlayerStatsSummary);
					foreach (var item in reds)
					{
						OrderedStats.Add(item);
					};
					StatsOrderIndex = 3;
					break;
				case "Seasons":
					var seasons = PlayerStatsSeasons.OrderByDescending(x => x.Season.Name);
					OrderedStats.Clear();
					OrderedStats.Add(PlayerStatsSummary);
					foreach (var item in seasons)
					{
						OrderedStats.Add(item);
					};
					StatsOrderIndex = 4;
					break;
				default:
					break;
			}
		}

		async Task ExecuteLoadPersonCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Person = await SportnetStore.GetPersonAsync(PersonId);

				switch (Person.PhotoUrl)
					{
						case null when Device.RuntimePlatform == Device.iOS:
							Photo = "DefaultPersonLogo.pdf";
							break;
						case null when Device.RuntimePlatform == Device.Android:
							Photo = "ic_avatar.xml";
							break;
						default:
							Photo = $"https://futbalnet.sportnet.online/api/images/{Person.PhotoId}";
							break;
					}

				Title = Person.Fullname;
				PlayerStatsSummary = new PlayerStatsSeason
				{
					Stats = await SportnetStore.GetPlayerStatsSummaryAsync(PersonId)
				};

				var transfers = await SportnetStore.GetPlayerTransfersAsync(PersonId);
				foreach (var transfer in transfers)
				{
					transfer.SourceClub = await SportnetStore.GetClubAsync(transfer.SourceClub.Id);
					transfer.DestinationClub = await SportnetStore.GetClubAsync(transfer.DestinationClub.Id);
					Transfers.Add(transfer);
				}

				PlayerStatsSeasons = new List<PlayerStatsSeason>();
				foreach (var season in App.Seasons)
				{
					var stat = new PlayerStatsSeason
					{
						Season = season,
						Stats = await SportnetStore.GetPlayerStatsBySeasonAsync(PersonId, season)
					};
					PlayerStatsSeasons.Add(stat);
				}
				OrderStatsCommand.Execute("Seasons");

				IsLoaded = true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				var log = new ErrorLog()
				{
					ExceptionType = ex.GetType().ToString(),
					Status = ErrorLog.LogStatus.Unread,
					Message = ex.Message,
					ObjectId = PersonId.ToString(),
					Action = "Loading Person",
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
