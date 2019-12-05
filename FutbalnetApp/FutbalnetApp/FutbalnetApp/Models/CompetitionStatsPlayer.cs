using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class CompetitionStatsPlayer
	{
		[JsonProperty("player.person")]
		public PersonPreview Player { get; set; }
		[JsonProperty("stats")]
		public IEnumerable<Stat> Stats { get; set; }

		public int G => Stats.First(x => x.Id == "G").Value;
		public int Min => Stats.First(x => x.Id == "MIN" || x.Id == "MINS").Value;
		public int YC => Stats.First(x => x.Id == "ZK").Value;
		public int RC => Stats.First(x => x.Id == "CK").Value;

		public override string ToString() => Player.ToString();

	}
}