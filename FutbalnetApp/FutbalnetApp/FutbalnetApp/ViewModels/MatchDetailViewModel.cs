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

		public MatchDetailViewModel(int id)
		{
			MatchId = id;
			Events = new List<EventGroup>();
			LoadMatchCommand = new Command(async () => await ExecuteLoadMatchCommand());
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
				foreach (var players in Match.Players)
				{
					foreach (var starter in players.Starters)
					{
						if (starter.Number == item.PlayerNumber)
							matchEvent.Player = starter.Person;

						if (starter.Number == item.SubPlayerNumber)
							matchEvent.SubPlayer = starter.Person;
					}
					foreach (var sub in players.Substitutes)
					{
						if (sub.Number == item.PlayerNumber)
							matchEvent.Player = sub.Person;

						if (sub.Number == item.SubPlayerNumber)
							matchEvent.SubPlayer = sub.Person;
					}
				}
				matchEvents.Add(matchEvent);
			}

			return matchEvents;
		}
		private void SortEvents()
		{
			var first = Match.Events.Where(x => x.Minute < 46).ToList();
			var second = Match.Events.Where(x => x.Minute > 45).ToList();
			
			Events = new List<EventGroup>()
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
