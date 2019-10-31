using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class PlayerStats
	{
		public Season Season { get; set; }
		public int RedCards { get; set; }
		public int YellowCards { get; set; }
		public int Goals { get; set; }
		public int Minutes { get; set; }
	}
}
