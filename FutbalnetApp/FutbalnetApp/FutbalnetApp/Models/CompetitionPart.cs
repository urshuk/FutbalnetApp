using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class CompetitionPart
	{
		public int Id { get; set; }
		[JsonProperty("part_type")]
		public string Type { get; set; }
		public string Name { get; set; }
		public IEnumerable<CompetitionRound> Rounds { get; set; }

	}
}
