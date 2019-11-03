using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class CompetitionPreview
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("season")]
		public Season Season { get; set; }
		[JsonProperty("category")]
		public string Category { get; set; }
		[JsonProperty("level")]
		public int Level { get; set; }
		[JsonProperty("union.id")]
		public int UnionId { get; set; }	
		[JsonProperty("sex")]
		public string Sex { get; set; }

		public override string ToString() => Name;
	}
}
