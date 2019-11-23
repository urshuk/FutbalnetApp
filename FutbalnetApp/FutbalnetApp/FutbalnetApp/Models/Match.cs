using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class Match : MatchPreview
	{
		[JsonProperty("competition_part.id")]
		public int PartId { get; set; }
		[JsonProperty("competition_part_round.id")]
		public int RoundId { get; set; }
		[JsonProperty("meeting_type")]
		public string MatchType { get; set; }
		[JsonProperty("playtime")]
		public int[] Playtime { get; set; }
		[JsonProperty("playtime_add")]
		public int[] PlaytimeAdded { get; set; }
		[JsonProperty("extratime")]
		public int[] Extratime { get; set; }
		[JsonProperty("extratime_add")]
		public int[] ExtratimeAdded { get; set; }
		[JsonProperty("surface_type.name")]
		public string SurfaceType { get; set; }
		[JsonProperty("last_update")]
		public DateTime LastUpdated { get; set; }
		[JsonProperty("stadium.id")]
		public int StadiumId { get; set; }
		[JsonProperty("partial_score")]
		public int[][] PartialScore { get; set; }
		[JsonProperty("audience")]
		public int Audience { get; set; }
		[JsonProperty("persons")]
		public MatchOfficials Officials { get; set; }
		[JsonProperty("officials")]
		public IEnumerable<MatchCoachingStaff> CoachingStaff { get; set; }
		[JsonProperty("players")]
		public IEnumerable<MatchPlayers> Players { get; set; }
		[JsonProperty("events")]
		public IEnumerable<MatchEvent> Events { get; set; }

		public string PartialScoreString => (PartialScore != null) ? $"({PartialScore[0][0]} : {PartialScore[0][1]})" : null;
	}
}
