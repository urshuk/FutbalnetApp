using FutbalnetApp.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.Models
{
	public class Competition : CompetitionPreview
	{
		public IEnumerable<CompetitionPart> Parts { get; set; }
	}
}
