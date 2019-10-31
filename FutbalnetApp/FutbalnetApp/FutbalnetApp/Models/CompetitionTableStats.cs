namespace FutbalnetApp.Models
{
	public class CompetitionTableStats
	{
		public int HomePoints { get; set; }
		public int AwayPoints { get; set; }
		public int Points { get; set; }
		public int Goals { get; set; }
		public int GoalsConceded { get; set; }
		public int PlusMinus { get; set; }
		public int Loses { get; set; }
		public int Draws { get; set; }
		public int Wins { get; set; }
		public int HomePlayed { get; set; }
		public int AwayPlayed { get; set; }
		public int Played { get; set; }
		public int YellowCards { get; set; }
		public int SecondYellowCards { get; set; }
		public int RedCards { get; set; }
		public int PointsAfterChange { get; set; }
		public int Coefficient { get; set; }
		public int CoefficientOrder { get; set; }
		public double FP { get; set; }
		public double FPAVG { get; set; }
	}
}