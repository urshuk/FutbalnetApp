using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class CompetitionTableClub
	{
		[JsonProperty("club")]
		public ClubPreview Club { get; set; }
		[JsonProperty("team.id")]
		public int TeamId { get; set; }
		[JsonProperty("order")]
		public int Order { get; set; }
		[JsonProperty("resignation")]
		public bool IsResignated { get; set; }
		[JsonProperty("stats")]
		public IEnumerable<Stat> Stats { get; set; }
	}
}