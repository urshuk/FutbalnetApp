using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class MatchEvent
	{
		public int ClubIndex { get; set; }
		public int Minute { get; set; }
		public string Type { get; set; }
		public int PlayerNumber { get; set; }
		public string GoalType { get; set; }
		public string CardType { get; set; }
		public string CardReason { get; set; }
		public int SubIndex { get; set; }
		public int SubPlayerNumber { get; set; }
	}
}
