using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class CompetitionSeason
	{
		[JsonProperty("competition.id")]
		public int Id { get; set; }
		[JsonProperty("competition.name")]
		public string Name { get; set; }
		[JsonProperty("season")]
		public Season Season { get; set; }

		public override string ToString() => Season.Name;

	}
}
