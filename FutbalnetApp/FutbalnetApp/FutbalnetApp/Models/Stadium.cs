using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class Stadium
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public StadiumCapacity Capacity { get; set; }
		public string Owner { get; set; }
		public Address Address { get; set; }
		public DateTime? Founded { get; set; }
	}
}
