using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string PhotoUrl { get; set; }
		public DateTime Birthdate { get; set; }
		public string Sex { get; set; }
		public string Country { get; set; }
		public DateTime MembershipFeeFrom { get; set; }
		public DateTime MembershipFeeTo { get; set; }
		public DateTime CardValidFrom { get; set; }
		public DateTime CardValidTo { get; set; }
		public string CardNumber { get; set; }
	}
}
