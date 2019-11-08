using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutbalnetApp.ViewModels
{
	public class CompetitionDetailViewModel : BaseViewModel
	{
		public int CompetitionId { get; set; }
		public Competition Competition { get; set; }
		public CompetitionPart Part { get; set; }
		public CompetitionRound Round { get; set; }
		public ObservableCollection<MatchPreview> Matches { get; set; }
		public ObservableCollection<CompetitionTableClub> TableClubs { get; set; }
		public ObservableCollection<CompetitionStatsPlayer> StatsPlayers { get; set; }
		public Command LoadCompetitionCommand { get; set; }

		public CompetitionDetailViewModel(int id)
		{
			//Title = Competition?.Name;
			CompetitionId = id;
			Matches = new ObservableCollection<MatchPreview>();
			TableClubs = new ObservableCollection<CompetitionTableClub>();
			StatsPlayers = new ObservableCollection<CompetitionStatsPlayer>();
			LoadCompetitionCommand = new Command(async () => await ExecuteLoadCompetitionCommand());
		}

		async Task LoadRoundAsync()
		{
			Matches.Clear();
			var round = await SportnetStore.GetCompetitionRoundAsync(Competition.Id, Part.Id, Round.Id);
			foreach (var match in round.Matches)
			{
				Matches.Add(match);
			}
		}
		async Task LoadStatsAsync()
		{
			StatsPlayers.Clear();
			var stats = await SportnetStore.GetCompetitionStatsAsync(Competition.Id, Part.Id);
			foreach (var player in stats.Players)
			{
				StatsPlayers.Add(player);
			}
		}
		async Task LoadTableAsync()
		{
			TableClubs.Clear();
			var table = await SportnetStore.GetCompetitionTableAsync(Competition.Id, Part.Id);
			foreach (var club in table.Clubs)
			{
				TableClubs.Add(club);
			}
		}
		async Task ExecuteLoadCompetitionCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var test = await SportnetStore.GetPlayerTransfersAsync(1319465);
				Competition = await SportnetStore.GetCompetitionAsync(CompetitionId);
				Part = Competition.Parts.FirstOrDefault();
				SetClosestRound();

				await LoadRoundAsync();
				await LoadStatsAsync();
				await LoadTableAsync();
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
		private void SetClosestRound()
		{
			//get the match no more than 3 days from today
			Round = Part.Rounds.First(x => Math.Abs((x.DateTime - DateTime.Now).Days) < 4);
			//if not found (break), get the upcoming match
			if (Round == null)
				Round = Part.Rounds.First(x => x.DateTime > DateTime.Now);
		}
	}
}
