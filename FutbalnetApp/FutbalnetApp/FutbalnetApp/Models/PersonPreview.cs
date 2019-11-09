using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class PersonPreview
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("surname")]
		public string LastName { get; set; }
		[JsonProperty("photo.urlpattern")]
		public string PhotoUrl { get; set; }
		[JsonProperty("photo.url")]
		public string PhotoBase { get; set; }
		public int PhotoId
		{
			get
			{
				bool success = int.TryParse(PhotoBase.Substring(PhotoBase.LastIndexOf("/") + 1), out int result);
				return success ? result : -1;
			}
		}

		public string Fullname => $"{Name} {LastName}";

		public override string ToString() => Fullname;
	}
}
