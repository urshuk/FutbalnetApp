using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class Stat
	{
		public int Value { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }

		public override string ToString() => $"{Id} - {Value}";
	}
}
