using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.ViewModels
{
	public class GenericGroup : List<object>
	{
		public string Name { get; set; }
		public object BindedObject { get; set; }

		public GenericGroup(string name, List<Competition> items) : base(items)
		{
			Name = name;
		}
		public GenericGroup(string name, List<Club> items) : base(items)
		{
			Name = name;
		}
		public GenericGroup(string name, List<Team> items) : base(items)
		{
			Name = name;
		}
		public GenericGroup(string name, List<MatchPreview> items) : base(items)
		{
			Name = name;
		}
	}
}
