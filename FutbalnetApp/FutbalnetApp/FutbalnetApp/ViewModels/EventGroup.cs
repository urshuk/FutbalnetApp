using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.ViewModels
{
	public class EventGroup : List<MatchEventViewModel>
	{
		public string Name { get; set; }

		public EventGroup(string name, List<MatchEventViewModel> events) : base(events)
		{
			Name = name;
		}
	}
}
