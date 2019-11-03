using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class MatchOfficials
	{
		[JsonProperty("ar1")]
		public PersonPreview AssistantReferee1 { get; set; }
		[JsonProperty("ar2")]
		public PersonPreview AssistantReferee2 { get; set; }
		[JsonProperty("r")]
		public PersonPreview Referee { get; set; }
		[JsonProperty("videotech")]
		public PersonPreview Videotech { get; set; }
		[JsonProperty("ds")]
		public PersonPreview Delegate { get; set; }
		[JsonProperty("hu")]
		public PersonPreview HeadOrganizer { get; set; }
	}
}
