using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class CompetitionStatsPlayer
	{
		[JsonProperty("player.person")]
		public PersonPreview Player { get; set; }
		[JsonProperty("stats")]
		public IEnumerable<Stat> Stats { get; set; }

		public override string ToString() => Player.ToString();

	}
}