using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class Season
	{
		public string Id { get; set; }
		public bool IsActual { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
	}
}
