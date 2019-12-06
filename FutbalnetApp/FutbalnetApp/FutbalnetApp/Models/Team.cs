using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class Team : TeamPreview
	{
		[JsonProperty("age")]
		public string Age { get; set; }
		[JsonProperty("sex")]
		public string Sex { get; set; }
		[JsonProperty("season")]
		public Season Season { get; set; }
		[JsonProperty("season.txtid")]
		public string SeasonId { get; set; }

		public string Fullname => $"{Name} ({Age})";

		public override string ToString() => Fullname;
	}
}
