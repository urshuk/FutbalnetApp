﻿using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class Contact
	{
		public string[] Phone { get; set; }
		[JsonProperty("person_name")]
		public string PersonName { get; set; }
		public string[] Fax { get; set; }
		public string[] Email { get; set; }
	}
}
