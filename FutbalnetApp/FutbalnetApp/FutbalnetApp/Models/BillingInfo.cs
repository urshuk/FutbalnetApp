using FutbalnetApp.Converters;
using Newtonsoft.Json;

namespace FutbalnetApp.Models

{
	[JsonConverter(typeof(JsonPathConverter))]
	public class BillingInfo
	{
		[JsonProperty("company")]
		public string Company { get; set; }
		[JsonProperty("dic")]
		public string DIC { get; set; }
		[JsonProperty("icdph")]
		public string ICDPH { get; set; }
		[JsonProperty("ico")]
		public string ICO { get; set; }
		[JsonProperty("bank.iban")]
		public string IBAN { get; set; }
		[JsonProperty("bank.swift")]
		public string SWIFT { get; set; }
	}
}