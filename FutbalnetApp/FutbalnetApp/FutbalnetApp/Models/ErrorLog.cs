using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class ErrorLog
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public Exception Exception { get; set; }
		public string Message { get; set; }
		public string Action { get; set; }
		public int ObjectId { get; set; }
		public object Object { get; set; }
		public DateTime Datetime { get; set; }
	}
}
