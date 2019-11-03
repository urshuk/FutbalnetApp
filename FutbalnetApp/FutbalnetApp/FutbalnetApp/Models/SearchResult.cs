using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class SearchResult
	{
		public int Id { get; set; }
		[JsonProperty("label")]
		public string Name { get; set; }
		public string Type { get; set; }
	}
}
