namespace FutbalnetApp.Models
{
	public class CompetitionTableClub
	{
		public int ClubId { get; set; }
		public int TeamId { get; set; }
		public int Order { get; set; }
		public string Name { get; set; }
		public string LogoUrl { get; set; }
		public bool Resignation { get; set; }
		public CompetitionTableStats Stats { get; set; }
	}
}