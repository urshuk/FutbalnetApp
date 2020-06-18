using FutbalnetApp.Models;
using FutbalnetApp.Services;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutbalnetApp.ViewModels
{
	public class TimelineViewModel : BaseViewModel
	{
		public Command LoadTimelineCommand { get; set; }
		public List<IGrouping<DateTime, MatchPreview>> matchGroups;
		public List<IGrouping<DateTime, MatchPreview>> MatchGroups
		{
			get => matchGroups;
			set => SetProperty(ref matchGroups, value);
		}

		public TimelineViewModel()
		{
			LoadTimelineCommand = new Command(async () => await ExecuteLoadTimelineCommandAsync());
			MessagingCenter.Subscribe<TeamDetailViewModel>(this, "FavouritesUpdated", (sender) => IsLoaded = false);

		}

		private void AddMatchNotification(MatchPreview match)
		{
			var not = new NotificationMatch
			{
				MatchId = match.Id,
				MatchName = match.ToString(),
				DateTime = match.Datetime
			};
			LocalDataStore.SaveNotificationMatch(not);
		}
		private async Task<IEnumerable<MatchPreview>> GetTeamMatchesAsync(Team team, IEnumerable<CompetitionPreview> comps = null)
		{
			var matches = new List<MatchPreview>();

			if (comps == null)
				comps = await SportnetStore.GetClubCompetitionsAsync(team.Club.Id, team.Season);

			foreach (var comp in comps)
			{
				var compTeams = await SportnetStore.GetCompetitionTeamsAsync(comp.Id);
				if (compTeams.FirstOrDefault(x => x.Id == team.Id) != null)
				{
					var competition = await SportnetStore.GetCompetitionAsync(comp.Id);
					foreach (var part in competition.Parts)
					{
						//for matches from other rounds i guess
						/*foreach (var round in part.Rounds.Where(x => (x.Datetime - DateTime.Today).Days < 15 && (x.Datetime.Date >= DateTime.Today)))
						{
							roundcouunt++;
							var fullRound = await SportnetStore.GetCompetitionRoundAsync(competition.Id, part.Id, round.Id);
							var match = fullRound.Matches.FirstOrDefault(x => x.Teams != null && x.Teams.FirstOrDefault(y => y.Id == team.Id) != null); ;
							if (match != null && match.Status == "VYGENEROVANY" && match.Datetime != null)
							{
								matches.Add(match);
								AddMatchNotification(match);
							}
						}*/
						var currentRoundNumber = part.Rounds.FirstOrDefault(x => (x.Datetime.Date >= DateTime.Today.AddDays(-1)))?.Number;
						foreach (var round in part.Rounds.Where(x => ((x.Datetime - DateTime.Today).Days < 17 && (x.Datetime.DayOfYear >= DateTime.Today.DayOfYear)) || (x.Number >= currentRoundNumber.GetValueOrDefault() - 1 && x.Number < currentRoundNumber.GetValueOrDefault() + 1)))
						{
							var fullRound = await SportnetStore.GetCompetitionRoundAsync(competition.Id, part.Id, round.Id);
							var match = fullRound.Matches.FirstOrDefault(x => x.Teams != null && x.Teams.FirstOrDefault(y => y.Id == team.Id) != null); ;
							if (match != null && match.Status == "VYGENEROVANY" && match.Datetime != null)
							{
								if ((match.Datetime.Value.Date - DateTime.Today).Days < 15 && (match.Datetime.Value.DayOfYear >= DateTime.Today.DayOfYear))
								{
									matches.Add(match);
									AddMatchNotification(match);
								}
							}
						}
					}
					break;
				}
			}
			return matches;
		}

		async Task ExecuteLoadTimelineCommandAsync()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var clubs = LocalDataStore.GetClubs();
				var teams = LocalDataStore.GetTeams();

				var matches = new List<MatchPreview>();

				foreach (var team in teams)
				{
					matches.AddRange(await GetTeamMatchesAsync(team));
				}

				/*foreach (var club in clubs)
				{
					var clubTeams = await SportnetStore.GetClubTeamsAsync(club.Id);
					var comps = await SportnetStore.GetClubCompetitionsAsync(club.Id, App.Seasons.First(x => x.IsActual));

					foreach (var team in clubTeams)
					{
						matches.AddRange(await GetTeamMatchesAsync(team, comps));
					}
				}*/
				MatchGroups = matches.OrderBy(x => x.Datetime).GroupBy(x => x.Datetime.GetValueOrDefault().Date).ToList();
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
					Action = "Loading timeline",
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
