using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class ErrorLog
	{
		public Exception Exception { get; set; }
		public string Message { get; set; }
		public string Action { get; set; }
		public int ObjectId { get; set; }
		public object Object { get; set; }
		public DateTime Datetime { get; set; }
	}
}
