using System;
using System.Collections.Generic;

namespace FutbalnetApp.Models
{
	public class CompetitionRound
	{
		public int Id { get; set; }
		public int Number { get; set; }
		public DateTime DateTime { get; set; }
		public IEnumerable<Match> Matches { get; set; }
	}
}