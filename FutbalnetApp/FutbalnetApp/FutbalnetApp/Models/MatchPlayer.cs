using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class MatchPlayer
	{
		public PersonPreview Person { get; set; }
		public string Post { get; set; }
		public int Number { get; set; }
		[JsonProperty("captain")]
		public bool IsCaptain { get; set; }
	}
}
