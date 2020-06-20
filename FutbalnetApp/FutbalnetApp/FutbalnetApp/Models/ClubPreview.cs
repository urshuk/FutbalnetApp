using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class ClubPreview
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("logo.urlpattern")]
		public string LogoUrl { get; set; }
		[JsonProperty("logo.url")]
		public string LogoBase { get; set; }
		public int LogoId => int.TryParse(LogoBase.Substring(LogoBase.LastIndexOf("/") + 1), out int result) ? result : -1;
		public string Logo => LogoUrl != null ? $"https://futbalnet.sportnet.online/api/images/{LogoId}" : "none";

		public override string ToString() => Name;
	}
}
