using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class Season
	{
		[JsonProperty("txtid")]
		public string Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("actual")]
		public bool IsActual { get; set; }
		[JsonProperty("dates.from")]
		public DateTime From { get; set; }
		[JsonProperty("dates.to")]
		public DateTime To { get; set; }

		public override string ToString() => Name;
	}
}
