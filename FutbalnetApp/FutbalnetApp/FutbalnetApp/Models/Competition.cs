using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class Competition
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Category { get; set; }
		public int Level { get; set; }
		public int UnionId { get; set; }
		public int SeasonId { get; set; }
		public Season Season { get; set; }
		public string Sex { get; set; }
		public IEnumerable<CompetitionPart> Parts { get; set; }
	}
}
