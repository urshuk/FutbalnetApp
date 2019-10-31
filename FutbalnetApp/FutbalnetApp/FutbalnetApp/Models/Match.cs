using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class Match
	{
		public int Id { get; set; }
		public int CompetitionId { get; set; }
		public int PartId { get; set; }
		public int RoundId { get; set; }
		public DateTime Datetime { get; set; }
		public string MatchType { get; set; }
		public string Status { get; set; }
		public int[] Playtime { get; set; }
		public int[] PlaytimeAdded { get; set; }
		public int[] Extratime { get; set; }
		public int[] ExtratimeAdded { get; set; }
		public string SurfaceType { get; set; }
		public DateTime LastUpdated { get; set; }
		public int[] Score { get; set; }
		public int StadiumId { get; set; }
		public int[][] PartialScore { get; set; }
		public int Audience { get; set; }
		public IEnumerable<MatchOfficial> Officials { get; set; }
		public IEnumerable<MatchOfficial>[] CoachingStaff { get; set; }
		public IEnumerable<MatchPlayers>[] Players { get; set; }
	}
}
