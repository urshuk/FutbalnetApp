using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FutbalnetApp.ViewModels
{
	public class MatchEventViewModel : MatchEvent
	{
		public PersonPreview Player { get; set; }
		public PersonPreview SubPlayer { get; set; }
	}
}
