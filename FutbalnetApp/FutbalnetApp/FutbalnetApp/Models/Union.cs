using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class Union
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("logo.urlpattern")]
		public string LogoUrl { get; set; }
		[JsonProperty("logo.url")]
		public string LogoBase { get; set; }
		public int LogoId
		{
			get
			{
				bool success = int.TryParse(LogoBase.Substring(LogoBase.LastIndexOf("/") + 1), out int result);
				return success ? result : -1;
			}
		}
		[JsonProperty("full_name")]
		public string FullName { get; set; }
		[JsonProperty("parent")]
		public int ParentUnionId { get; set; }
		[JsonProperty("billing_info")]
		public BillingInfo BillingInfo { get; set; }
		[JsonProperty("contact")]
		public Contact Contact { get; set; }

		public override string ToString() => FullName;
	}
}
