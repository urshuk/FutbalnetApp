using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutbalnetApp.ViewModels
{
	public class CompetitionDetailViewModel : BaseViewModel
	{
		public int CompetitionId { get; set; }
		public Competition competition;
		public Competition Competition
		{
			get => competition;
			set => SetProperty(ref competition, value);
		}
		public CompetitionPart part;
		public CompetitionPart Part
		{
			get => part;
			set => SetProperty(ref part, value);
		}
		public CompetitionRound round;
		public CompetitionRound Round
		{
			get => round;
			set => SetProperty(ref round, value);
		}
		public ObservableCollection<MatchPreview> Matches { get; set; }
		public IEnumerable<CompetitionTableClub> TableClubs { get; set; }
		public IEnumerable<CompetitionSeason> pastSeasons;
		public IEnumerable<CompetitionSeason> PastSeasons
		{
			get => pastSeasons;
			set => SetProperty(ref pastSeasons, value);
		}
		public CompetitionSeason currentSeason;
		public CompetitionSeason CurrentSeason
		{
			get => currentSeason;
			set => SetProperty(ref currentSeason, value);
		}
		public ObservableCollection<CompetitionTableClub> OrderedTableClubs { get; set; }
		public IEnumerable<CompetitionStatsPlayer> StatsPlayers { get; set; }
		public ObservableCollection<CompetitionStatsPlayer> OrderedStatsPlayers { get; set; }
		public Command LoadCompetitionCommand { get; set; }
		public Command OrderStatsCommand { get; set; }
		public Command OrderTableCommand { get; set; }
		public Command LoadRoundCommand { get; set; }
		public int SelectedTabIndex { get; set; }
		int statsOrderIndex = 0;
		public int StatsOrderIndex
		{
			get => statsOrderIndex;
			set => SetProperty(ref statsOrderIndex, value);
		}
		int tableOrderIndex = 0;
		public int TableOrderIndex
		{
			get => tableOrderIndex;
			set => SetProperty(ref tableOrderIndex, value);
		}

		public CompetitionDetailViewModel(int id)
		{
			CompetitionId = id;
			Matches = new ObservableCollection<MatchPreview>();
			OrderedTableClubs = new ObservableCollection<CompetitionTableClub>();
			OrderedStatsPlayers = new ObservableCollection<CompetitionStatsPlayer>();
			OrderStatsCommand = new Command<string>((pareameter) => ExecuteOrderStatsCommand(pareameter));
			OrderTableCommand = new Command<string>((pareameter) => ExecuteOrderTableCommand(pareameter));
			LoadRoundCommand = new Command(async () => await ExecuteLoadRoundCommand());
			LoadCompetitionCommand = new Command(async () => await ExecuteLoadCompetitionCommand());
		}

		async Task ExecuteLoadRoundCommand()
		{
			await LoadRoundAsync();
		}
		private void ExecuteOrderTableCommand(string by)
		{
			switch (by)
			{
				case "Matches":
					var matches = TableClubs.OrderByDescending(x => x.MP);
					OrderedTableClubs.Clear();
					foreach (var item in matches)
					{
						OrderedTableClubs.Add(item);
					};
					TableOrderIndex = 0;
					break;
				case "Wins":
					var wins = TableClubs.OrderByDescending(x => x.W);
					OrderedTableClubs.Clear();
					foreach (var item in wins)
					{
						OrderedTableClubs.Add(item);
					};
					TableOrderIndex = 1;
					break;
				case "Draws":
					var draws = TableClubs.OrderByDescending(x => x.D);
					OrderedTableClubs.Clear();
					foreach (var item in draws)
					{
						OrderedTableClubs.Add(item);
					};
					TableOrderIndex = 2;
					break;
				case "Losses":
					var losses = TableClubs.OrderByDescending(x => x.L);
					OrderedTableClubs.Clear();
					foreach (var item in losses)
					{
						OrderedTableClubs.Add(item);
					};
					TableOrderIndex = 3;
					break;
				case "Score":
					var score = TableClubs.OrderByDescending(x => x.ScoreDifference);
					OrderedTableClubs.Clear();
					foreach (var item in score)
					{
						OrderedTableClubs.Add(item);
					};
					TableOrderIndex = 4;
					break;
				case "Points":
					var points = TableClubs.OrderBy(x => x.Order);
					OrderedTableClubs.Clear();
					foreach (var item in points)
					{
						OrderedTableClubs.Add(item);
					};
					TableOrderIndex = 5;
					break;
				default:
					break;
			}
		}
		private void ExecuteOrderStatsCommand(string by)
		{
			switch (by)
			{
				case "Goals":
					var goals = StatsPlayers.OrderByDescending(x => x.G).Take(20);
					OrderedStatsPlayers.Clear();
					foreach (var item in goals)
					{
						OrderedStatsPlayers.Add(item);
					};
					StatsOrderIndex = 0;
					break;
				case "Mins":
					var mins = StatsPlayers.OrderByDescending(x => x.Min).Take(20);
					OrderedStatsPlayers.Clear();
					foreach (var item in mins)
					{
						OrderedStatsPlayers.Add(item);
					};
					StatsOrderIndex = 1;
					break;
				case "Yellows":
					var yellows = StatsPlayers.OrderByDescending(x => x.YC).Take(20);
					OrderedStatsPlayers.Clear();
					foreach (var item in yellows)
					{
						OrderedStatsPlayers.Add(item);
					};
					StatsOrderIndex = 2;
					break;
				case "Reds":
					var reds = StatsPlayers.OrderByDescending(x => x.RC).Take(20);
					OrderedStatsPlayers.Clear();
					foreach (var item in reds)
					{
						OrderedStatsPlayers.Add(item);
					};
					StatsOrderIndex = 3;
					break;
				default:
					break;
			}
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
			var stats = await SportnetStore.GetCompetitionStatsAsync(Competition.Id, Part.Id);
			StatsPlayers = stats.Players;
			OrderStatsCommand.Execute("Goals");
		}
		async Task LoadTableAsync()
		{
			var table = await SportnetStore.GetCompetitionTableAsync(Competition.Id, Part.Id);
			TableClubs = table.Clubs;
			OrderTableCommand.Execute("Points");
		}
		async Task LoadPastSeasonsAsync()
		{
			var past = await SportnetStore.GetPastCompetitionsAsync(Competition.Id);
			PastSeasons = past;
		}
		async Task ExecuteLoadCompetitionCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Competition = await SportnetStore.GetCompetitionAsync(CompetitionId);
				Part = Competition.Parts.FirstOrDefault();
				SetClosestRound();

				await LoadRoundAsync();
				await LoadStatsAsync();
				await LoadTableAsync();
				await LoadPastSeasonsAsync();

				CurrentSeason = PastSeasons.FirstOrDefault(x => x.Id == CompetitionId);

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
		private void SetClosestRound()
		{
			//get the match no more than 3 days from today
			Round = Part.Rounds.FirstOrDefault(x => Math.Abs((x.Datetime - DateTime.Now).Days) < 4);
			//if not found (break), get the upcoming match
			if (Round == null)
				Round = Part.Rounds.FirstOrDefault(x => x.Datetime > DateTime.Now);

			if (round == null)
				round = part.Rounds.LastOrDefault();
		}
	}
}
