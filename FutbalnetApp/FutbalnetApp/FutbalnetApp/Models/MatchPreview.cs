using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class MatchPreview
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("teams")]
		public IEnumerable<TeamPreview> Teams { get; set; }
		[JsonProperty("score")]
		public int[] Score { get; set; }
		[JsonProperty("competition.id")]
		public int CompetitionId { get; set; }
		[JsonProperty("datetime")]
		public DateTime Datetime { get; set; }
		[JsonProperty("match_status")]
		public string Status { get; set; }

		public string ScoreString => (Score != null) ? $"{Score[0]} : {Score[1]}" : null;
		public bool IsPlaying => (Status == "VYGENEROVANY" && Score != null) ? true : false;
		public bool NotStartedYet => (Status == "VYGENEROVANY" && IsPlaying == false);


		public override string ToString() => $"{Teams.FirstOrDefault()?.Name} : {Teams.LastOrDefault()?.Name}";
	}
}
