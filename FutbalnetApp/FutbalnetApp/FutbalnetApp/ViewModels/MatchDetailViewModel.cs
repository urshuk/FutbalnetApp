using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;

namespace FutbalnetApp.ViewModels
{
	public class MatchDetailViewModel : BaseViewModel
	{
		public int MatchId { get; set; }
		public Match match;
		public Match Match
		{
			get => match;
			set => SetProperty(ref match, value);
		}
		public Command LoadMatchCommand { get; set; }
		public int SelectedTabIndex { get; set; }
		public List<EventGroup> events;
		public List<EventGroup> Events
		{
			get => events;
			set => SetProperty(ref events, value);
		}
		public List<LineupGroup> lineups;
		public List<LineupGroup> Lineups
		{
			get => lineups;
			set => SetProperty(ref lineups, value);
		}
		public ObservableCollection<MatchCoachingStaff> CoachingStaff { get; set; }
		public int lineupsHeight;
		public int LineupsHeight
		{
			get => lineupsHeight;
			set => SetProperty(ref lineupsHeight, value);
		}

		public MatchDetailViewModel(int id)
		{
			MatchId = id;
			CoachingStaff = new ObservableCollection<MatchCoachingStaff>();
			LoadMatchCommand = new Command(async () => await ExecuteLoadMatchCommand());
		}

		private void SetLineups()
		{
			var starters = new List<LineupViewModel>();
			var subs = new List<LineupViewModel>();

			if (Match.Players.FirstOrDefault().Starters.Count() > 0)
			{
				for (int i = 0; i < 11; i++)
				{
					var starter = new LineupViewModel
					{
						HomePlayer = Match.Players.FirstOrDefault().Starters.ElementAtOrDefault(i),
						AwayPlayer = Match.Players.LastOrDefault().Starters.ElementAtOrDefault(i),
					};
					starters.Add(starter);
				}

				int homeSubsCount = Match.Players.FirstOrDefault().Substitutes.Count();
				int awaySubsCount = Match.Players.LastOrDefault().Substitutes.Count();
				int subsCount = homeSubsCount >= awaySubsCount ? homeSubsCount : awaySubsCount;

				for (int i = 0; i < subsCount; i++)
				{
					var sub = new LineupViewModel
					{
						HomePlayer = Match.Players.FirstOrDefault().Substitutes.ElementAtOrDefault(i),
						AwayPlayer = Match.Players.LastOrDefault().Substitutes.ElementAtOrDefault(i),
					};
					subs.Add(sub);
				}
			}

			if (Device.RuntimePlatform == Device.iOS)
			{
				Lineups = new List<LineupGroup>
				{
					new LineupGroup("BugFix", new List<LineupViewModel>()),
					new LineupGroup("Základná zostava", starters),
					new LineupGroup("Náhradníci", subs),
				};
			}
			else
			{
				Lineups = new List<LineupGroup>
				{
					new LineupGroup("Základná zostava", starters),
					new LineupGroup("Náhradníci", subs),
				};
			}


			LineupsHeight = ((starters.Count + subs.Count) * 25) + 67;

			foreach (var item in Match.CoachingStaff)
			{
				if (item.Persons != null && (item.Persons.FirstOrDefault().Id != 0 || item.Persons.LastOrDefault().Id != 0))
				{
					switch (item.Function)
					{
						case "TRENER":
							item.Function = "Tréner";
							break;
						case "VEDUCI":
							item.Function = "Vedúci mužstva";
							break;
						case "MASER":
							item.Function = "Masér";
							break;
						case "LEKAR":
							item.Function = "Lekár";
							break;
						case "ASISTENT_TR1":
							item.Function = "1. asistent trénera";
							break;
						case "ASISTENT_TR2":
							item.Function = "2. asistent trénera";
							break;
						case "TRENER_BRANKAROV":
							item.Function = "Tréner brankárov";
							break;
						case "ZASTUPCA_KLUBU":
							item.Function = "Zástupca klubu";
							break;
						default:
							break;
					}
					CoachingStaff.Add(item);
				}
			}
		}
		private List<MatchEventViewModel> ExtendEvents(List<MatchEvent> events)
		{
			var matchEvents = new List<MatchEventViewModel>();

			foreach (var item in events)
			{
				var matchEvent = new MatchEventViewModel
				{
					PlayerNumber = item.PlayerNumber,
					Minute = item.Minute,
					ClubIndex = item.ClubIndex,
					Type = item.Type,
					CardType = item.CardType,
					CardReason = item.CardReason,
					GoalType = item.GoalType,
					SubIndex = item.SubIndex,
					SubPlayerNumber = item.SubPlayerNumber,
				};
				foreach (var starter in Match.Players.ElementAt(item.ClubIndex).Starters)
				{
					if (starter.Number == item.PlayerNumber)
						matchEvent.Player = starter.Person;

					if (starter.Number == item.SubPlayerNumber)
						matchEvent.SubPlayer = starter.Person;
				}
				foreach (var sub in Match.Players.ElementAt(item.ClubIndex).Substitutes)
				{
					if (sub.Number == item.PlayerNumber)
						matchEvent.Player = sub.Person;

					if (sub.Number == item.SubPlayerNumber)
						matchEvent.SubPlayer = sub.Person;
				}
				matchEvents.Add(matchEvent);
			}

			return matchEvents;
		}
		private void SortEvents()
		{
			var first = Match.Events.Where(x => x.Minute < 46).ToList();
			var second = Match.Events.Where(x => x.Minute > 45).ToList();

			Events = new List<EventGroup>
			{
				new EventGroup("1. Polčas", ExtendEvents(first)),
				new EventGroup("2. Polčas", ExtendEvents(second))
			};
		}
		async Task ExecuteLoadMatchCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Match = await SportnetStore.GetMatchAsync(MatchId);
				SortEvents();
				SetLineups();

				IsLoaded = true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				var log = new ErrorLog()
				{
					Exception = ex,
					Object = Match,
					ObjectId = MatchId,
					Action = "Loading Match",
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
