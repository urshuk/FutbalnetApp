using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

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

		public int MP => Stats.First(x => x.Id == "Z").Value;
		public int W => Stats.First(x => x.Id == "V").Value;
		public int D => Stats.First(x => x.Id == "R").Value;
		public int L => Stats.First(x => x.Id == "P").Value;
		public string Score => $"{Stats.First(x => x.Id == "SG").Value}:{Stats.First(x=>x.Id == "IG").Value}";
		public int P => Stats.First(x => x.Id == "B").Value;
		public int ScoreDifference => Stats.First(x => x.Id == "SG").Value - Stats.First(x => x.Id == "IG").Value;

		public override string ToString() => Club.Name;
	}
}