using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class Profile
	{
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("profileId")]
		public int Id { get; set; }
	}
}
