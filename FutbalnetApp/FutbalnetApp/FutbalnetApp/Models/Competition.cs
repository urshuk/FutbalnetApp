using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class Competition : CompetitionPreview
	{
		[XmlIgnore]
		[JsonProperty("parts")]
		public IEnumerable<CompetitionPart> Parts { get; set; }
	}
}
