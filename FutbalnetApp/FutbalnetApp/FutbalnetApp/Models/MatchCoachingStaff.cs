using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class MatchCoachingStaff
	{
		[JsonProperty("function")]
		public string Function { get; set; }
		[JsonProperty("person_ids")]
		public IEnumerable<PersonPreview> Persons { get; set; }
	}
}