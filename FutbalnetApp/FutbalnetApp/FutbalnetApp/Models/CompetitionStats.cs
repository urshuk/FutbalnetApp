using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class CompetitionStats
	{
		public string Name { get; set; }
		public IEnumerable<CompetitionStatsPlayer> Players { get; set; }
	}
}
