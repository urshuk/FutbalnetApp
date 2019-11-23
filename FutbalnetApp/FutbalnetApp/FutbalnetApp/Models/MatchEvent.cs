using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class MatchEvent
	{
		[JsonProperty("club_index")]
		public int ClubIndex { get; set; }
		public int Minute { get; set; }
		public string Type { get; set; }
		[JsonProperty("player_number")]
		public int PlayerNumber { get; set; }
		[JsonProperty("goal_type")]
		public string GoalType { get; set; }
		[JsonProperty("card_type")]
		public string CardType { get; set; }
		[JsonProperty("card_reason")]
		public string CardReason { get; set; }
		[JsonProperty("subst_index")]
		public int? SubIndex { get; set; }
		[JsonProperty("subst_player_number")]
		public int? SubPlayerNumber { get; set; }
	}
}
