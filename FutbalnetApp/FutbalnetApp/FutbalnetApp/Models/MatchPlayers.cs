using Newtonsoft.Json;
using System.Collections.Generic;

namespace FutbalnetApp.Models
{
	public class MatchPlayers
	{
		[JsonProperty("players")]
		public IEnumerable<MatchPlayer> Starters { get; set; }
		[JsonProperty("alternates")]
		public IEnumerable<MatchPlayer> Substitutes { get; set; }
	}
}