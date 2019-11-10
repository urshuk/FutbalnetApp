﻿using FutbalnetApp.Models;
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
		public IEnumerable<CompetitionTableClub> TableClubs { get; set; }
		public ObservableCollection<CompetitionTableClub> OrderedTableClubs { get; set; }
		public IEnumerable<CompetitionStatsPlayer> StatsPlayers { get; set; }
		public ObservableCollection<CompetitionStatsPlayer> OrderedStatsPlayers { get; set; }
		public Command LoadCompetitionCommand { get; set; }
		public Command OrderStatsCommand { get; set; }
		public Command OrderTableCommand { get; set; }
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
			//Title = Competition?.Name;
			CompetitionId = id;
			Matches = new ObservableCollection<MatchPreview>();
			OrderedTableClubs = new ObservableCollection<CompetitionTableClub>();
			OrderedStatsPlayers = new ObservableCollection<CompetitionStatsPlayer>();
			OrderStatsCommand = new Command<string>((pareameter) => ExecuteOrderStatsCommand(pareameter));
			OrderTableCommand = new Command<string>((pareameter) => ExecuteOrderTableCommand(pareameter));
			LoadCompetitionCommand = new Command(async () => await ExecuteLoadCompetitionCommand());
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
			Round = Part.Rounds.First(x => Math.Abs((x.DateTime - DateTime.Now).Days) < 4);
			//if not found (break), get the upcoming match
			if (Round == null)
				Round = Part.Rounds.First(x => x.DateTime > DateTime.Now);
		}
	}
}
