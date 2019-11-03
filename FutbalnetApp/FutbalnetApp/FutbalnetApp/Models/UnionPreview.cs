using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class UnionPreview
	{
		[JsonProperty("union.id")]
		public int Id { get; set; }
		[JsonProperty("union.name")]
		public string Name { get; set; }
		[JsonProperty("union.logo.urlpattern")]
		public string LogoUrl { get; set; }
		[JsonProperty("union.logo.url")]
		public string LogoBase { get; set; }
		public int LogoId
		{
			get
			{
				bool success = int.TryParse(LogoBase.Substring(LogoBase.LastIndexOf("/") + 1), out int result);
				return success ? result : -1;
			}
		}
		[JsonProperty("union.full_name")]
		public string FullName { get; set; }
		[JsonProperty("union.parent")]
		public int ParentUnionId { get; set; }
		[JsonProperty("subs")]
		public IEnumerable<UnionPreview> SubUnions { get; set; }
	}
}
