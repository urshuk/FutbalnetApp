using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.ViewModels
{
	public class LineupGroup : List<LineupViewModel>
	{
		public string Name { get; set; }

		public LineupGroup(string name, List<LineupViewModel> lineups) : base(lineups)
		{
			Name = name;
		}
	}
}
