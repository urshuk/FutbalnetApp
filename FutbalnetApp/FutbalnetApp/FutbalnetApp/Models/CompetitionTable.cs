using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class CompetitionTable
	{
		public IEnumerable<CompetitionTableClub> Clubs { get; set; }
	}
}
