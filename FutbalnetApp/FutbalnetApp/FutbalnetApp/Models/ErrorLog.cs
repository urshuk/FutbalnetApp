using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class ErrorLog
	{
		public enum LogStatus { Unread, Read, Fixed }

		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public string ExceptionType { get; set; }
		public string Message { get; set; }
		public string Action { get; set; }
		public string ObjectId { get; set; }
		public DateTime Datetime { get; set; }
		public LogStatus Status { get; set; }
	}
}
