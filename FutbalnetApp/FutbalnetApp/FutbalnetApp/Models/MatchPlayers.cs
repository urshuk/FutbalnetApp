using System.Collections.Generic;

namespace FutbalnetApp.Models
{
	public class MatchPlayers
	{
		public IEnumerable<MatchPlayer> Starters { get; set; }
		public IEnumerable<MatchPlayer> Substitutes { get; set; }
	}
}