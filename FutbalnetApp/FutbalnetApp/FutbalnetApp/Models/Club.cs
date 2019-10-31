using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class Club
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public string LogoUrl { get; set; }
		public int UnionId { get; set; }
		public int StadiumId { get; set; }
		public DateTime Founded { get; set; }
		public BillingInfo BillingInfo { get; set; }
		public Contact Contact { get; set; }
	}
}
