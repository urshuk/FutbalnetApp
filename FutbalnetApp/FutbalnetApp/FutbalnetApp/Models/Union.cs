	using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class Union
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string LogoUrl { get; set; }
		public string FullName { get; set; }
		public int ParentUnionId { get; set; }
		public IEnumerable<Union> SubUnions { get; set; }
		public BillingInfo BillingInfo { get; set; }
		public Contact Contact { get; set; }
	}
}
