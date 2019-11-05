using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class Transfer
	{
		[JsonProperty("status")]
		public string Status { get; set; }
		[JsonProperty("date.from")]
		public DateTime? From { get; set; }
		[JsonProperty("date.to")]
		public DateTime? To { get; set; }
		[JsonProperty("move_type")]
		public string Type { get; set; }
		[JsonProperty("club_source")]
		public ClubPreview SourceClub { get; set; }
		[JsonProperty("club_destination")]
		public ClubPreview DestinationClub { get; set; }
	}
}
