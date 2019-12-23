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
	public class TeamDetailViewModel : BaseViewModel
	{
		public int TeamId { get; set; }
		public Club Club { get; set; }
		public Competition competition;
		public Competition Competition
		{
			get => competition;
			set => SetProperty(ref competition, value);
		}
		public CompetitionPart Part { get; set; }
		public Team team;
		public Team Team
		{
			get => team;
			set => SetProperty(ref team, value);
		}
		public int SelectedTabIndex { get; set; }
		public ObservableCollection<MatchPreview> Matches { get; set; }
		public Command LoadTeamCommand { get; set; }
		public TableViewModel table;
		public TableViewModel Table
		{
			get => table;
			set => SetProperty(ref table, value);
		}
		public Command ToggleFavouriteCommand { get; set; }
		public bool isFavourite;
		public bool IsFavourite
		{
			get => isFavourite;
			set => SetProperty(ref isFavourite, value);
		}


		public TeamDetailViewModel(int id, Club club)
		{
			TeamId = id;
			Club = club;
			Matches = new ObservableCollection<MatchPreview>();
			LoadTeamCommand = new Command(async () => await ExecuteLoadTeamCommand());
			ToggleFavouriteCommand = new Command(() => ExecuteToggleFavouriteCommand());
			IsFavourite = LocalDataStore.TeamExists(TeamId);
		}
		public TeamDetailViewModel(Team team)
		{
			TeamId = team.Id;
			Matches = new ObservableCollection<MatchPreview>();
			LoadTeamCommand = new Command(async () => await ExecuteLoadTeamCommand());
			ToggleFavouriteCommand = new Command(() => ExecuteToggleFavouriteCommand());
			IsFavourite = LocalDataStore.TeamExists(TeamId);
		}

		void ExecuteToggleFavouriteCommand()
		{
			if (IsFavourite)
			{
				LocalDataStore.DeleteTeam(TeamId);
				IsFavourite = false;
			}
			else
			{
				LocalDataStore.SaveTeam(Team);
				IsFavourite = true;
			}
		}

		async Task LoadTeamMatchesAsync()
		{
			var comps = await SportnetStore.GetClubCompetitionsAsync(Club.Id, Team.Season);
			comps = comps.OrderBy(x => x.Level);
			Matches.Clear();
			foreach (var comp in comps)
			{
				var teams = await SportnetStore.GetCompetitionTeamsAsync(comp.Id);
				if (teams.FirstOrDefault(x => x.Id == Team.Id) != null)
				{
					Competition = await SportnetStore.GetCompetitionAsync(comp.Id);
					foreach (var part in Competition.Parts)
					{
						foreach (var round in part.Rounds)
						{
							var fullRound = await SportnetStore.GetCompetitionRoundAsync(Competition.Id, part.Id, round.Id);
							var match = fullRound.Matches.FirstOrDefault(x => x.Teams.FirstOrDefault(y => y.Id == Team.Id) != null);
							if (match != null)
							{
								Matches.Add(match);
								Part = part;
							}
						}
						if (Part != null)
						{
							var table = await SportnetStore.GetCompetitionTableAsync(Competition.Id, Part.Id);
							Table = new TableViewModel(table.Clubs);
							Table.OrderTableCommand.Execute("Points");
						}
					}
					break;
				}
			}
		}

		async Task ExecuteLoadTeamCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				if (Team == null)
					Team = await SportnetStore.GetTeamAsync(TeamId);

				if (Club == null)
					Club = await SportnetStore.GetClubAsync(Team.Club.Id);

				await LoadTeamMatchesAsync();

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
