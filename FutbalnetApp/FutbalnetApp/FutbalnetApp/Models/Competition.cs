﻿using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	[JsonConverter(typeof(JsonPathConverter))]
	public class Competition : CompetitionPreview
	{
		[JsonProperty("parts")]
		public IEnumerable<CompetitionPart> Parts { get; set; }
	}
}
