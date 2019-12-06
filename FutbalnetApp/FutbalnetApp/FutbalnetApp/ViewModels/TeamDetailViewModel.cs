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
		public Competition Competition { get; set; }
		public Team team;
		public Team Team
		{
			get => team;
			set => SetProperty(ref team, value);
		}
		public int SelectedTabIndex { get; set; }
		public ObservableCollection<MatchPreview> Matches { get; set; }
		public Command LoadTeamCommand { get; set; }

		public TeamDetailViewModel(int id, Club club)
		{
			TeamId = id;
			Club = club;
			Matches = new ObservableCollection<MatchPreview>();
			LoadTeamCommand = new Command(async () => await ExecuteLoadTeamCommand());
		}

		async Task ExecuteLoadTeamCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Team = await SportnetStore.GetTeamAsync(TeamId);

				var comps = await SportnetStore.GetActiveCompetitionsAsync(Team.Season);
				comps = comps.Where(x => x.UnionId == Club.UnionId);
				Matches.Clear();
				foreach (var comp in comps)
				{
					var teams = await SportnetStore.GetCompetitionTeamsAsync(comp.Id);
					if (teams.FirstOrDefault(x=>x.Id == Team.Id) != null)
					{
						Competition = await SportnetStore.GetCompetitionAsync(comp.Id);
						foreach (var part in Competition.Parts)
						{
							foreach (var round in part.Rounds)
							{
								var fullRound = await SportnetStore.GetCompetitionRoundAsync(Competition.Id, part.Id, round.Id);
								var match = fullRound.Matches.FirstOrDefault(x => x.Teams.FirstOrDefault(y => y.Id == Team.Id) != null);
								if (match != null)
									Matches.Add(match);
							}
						}
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
