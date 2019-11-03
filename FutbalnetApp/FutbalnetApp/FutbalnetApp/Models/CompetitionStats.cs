using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class CompetitionStats
	{
		[JsonProperty("groupname")]
		public string Name { get; set; }
		public IEnumerable<CompetitionStatsPlayer> Players { get; set; }
	}
}
