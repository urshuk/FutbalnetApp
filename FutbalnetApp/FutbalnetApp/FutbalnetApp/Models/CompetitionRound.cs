using System;
using System.Collections.Generic;

namespace FutbalnetApp.Models
{
	public class CompetitionRound
	{
		public int Id { get; set; }
		public int Number { get; set; }
		public DateTime Datetime { get; set; }
		public IEnumerable<MatchPreview> Matches { get; set; }

		public override string ToString() => Number.ToString();
	}
}