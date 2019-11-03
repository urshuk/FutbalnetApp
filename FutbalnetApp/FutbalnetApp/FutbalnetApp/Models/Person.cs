using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class Person : PersonPreview
	{
		[JsonProperty("birthdate")]
		public DateTime Birthdate { get; set; }
		[JsonProperty("sex")]
		public string Sex { get; set; }
		[JsonProperty("country")]
		public string Country { get; set; }
		[JsonProperty("datumClenskehoOd")]
		public DateTime MembershipFeeFrom { get; set; }
		[JsonProperty("datumClenskehoDo")]
		public DateTime MembershipFeeTo { get; set; }
		[JsonProperty("card.ordered")]
		public DateTime CardValidFrom { get; set; }
		[JsonProperty("card.validity")]
		public DateTime CardValidTo { get; set; }
		[JsonProperty("card.number")]
		public string CardNumber { get; set; }
		[JsonProperty("profiles")]
		public IEnumerable<Profile> Profiles { get; set; }
	}
}
