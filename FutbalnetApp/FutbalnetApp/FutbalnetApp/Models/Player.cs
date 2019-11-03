using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class Player
	{
		[JsonProperty("profileId")]
		public int Id { get; set; }
		[JsonProperty("club_actual")]
		public ClubPreview ActualClub { get; set; }
		[JsonProperty("profileStatus.status")]
		public string Status { get; set; }
		[JsonProperty("profileStatus.regDate")]
		public DateTime RegistrationDate { get; set; }
		[JsonProperty("person")]
		public PersonPreview Person { get; set; }
		[JsonProperty("club_origin")]
		public ClubPreview OriginClub { get; set; }
	}
}
