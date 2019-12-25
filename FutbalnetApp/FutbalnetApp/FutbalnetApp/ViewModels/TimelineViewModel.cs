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
			CrossLocalNotifications.Current.Show("title", "body", 100, DateTime.Now.AddSeconds(10));
		}

		private void SetMatchNotification(MatchPreview match)
		{
			if (!LocalDataStore.GetNotificationsSettings())
				return;

			var minutesAhead = LocalDataStore.GetNotificationsMinutes();
			CrossLocalNotifications.Current.Show(match.ToString(), "Začiatok zápasu", match.Id, match.Datetime.AddMinutes(-minutesAhead));
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
						foreach (var round in part.Rounds.Where(x => (x.Datetime - DateTime.Today).Days < 15 && (x.Datetime.Date >= DateTime.Today)))
						{
							var fullRound = await SportnetStore.GetCompetitionRoundAsync(competition.Id, part.Id, round.Id);
							var match = fullRound.Matches.FirstOrDefault(x => x.Teams != null && x.Teams.FirstOrDefault(y => y.Id == team.Id) != null); ;
							if (match != null && match.Status == "VYGENEROVANY")
							{
								matches.Add(match);
								SetMatchNotification(match);
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

				MatchGroups = matches.GroupBy(x => x.Datetime.Date).ToList();
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
