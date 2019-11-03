using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class Club : ClubPreview
	{
		[JsonProperty("status")]
		public string Status { get; set; }
		[JsonProperty("union.id")]
		public int UnionId { get; set; }
		[JsonProperty("stadium.id")]
		public int StadiumId { get; set; }
		[JsonProperty("billing_info")]
		public BillingInfo BillingInfo { get; set; }
		[JsonProperty("contact")]
		public Contact Contact { get; set; }
	}
}
