using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class Player
	{
		public int Id { get; set; }
		public int ActualClubId { get; set; }
		public string ActualClubName { get; set; }
		public string Status { get; set; }
		public DateTime RegistrationDate { get; set; }
		public int OriginClubId { get; set; }
		public string OriginClubName { get; set; }
		public string Type { get; set; }
		public PlayerStats StatsSummary { get; set; }
		public IEnumerable<PlayerStats> SeasonStats { get; set; }
	}
}
