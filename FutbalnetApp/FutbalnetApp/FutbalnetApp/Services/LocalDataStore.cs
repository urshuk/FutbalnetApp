using FutbalnetApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Essentials;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;

namespace FutbalnetApp.Services
{
	public class LocalDataStore : ILocalDataStore
	{
		public Club GetClub(int id)
		{
			var clubs = GetClubs();
			var club = clubs.FirstOrDefault(x => x.Id == id);
			return club;
		}
		public void SaveClub(Club value)
		{
			var clubs = GetClubs();
			clubs.Add(value);
			var xs = new XmlSerializer(typeof(List<Club>));
			string xml;
			using (var textWriter = new StringWriter())
			{
				xs.Serialize(textWriter, clubs);
				xml = textWriter.ToString();
			}
			Preferences.Set("clubs", xml);
		}
		public void DeleteClub(int id)
		{
			var clubs = GetClubs();
			clubs.RemoveAll(x => x.Id == id);
			var xs = new XmlSerializer(typeof(List<Club>));
			string xml;
			using (var textWriter = new StringWriter())
			{
				xs.Serialize(textWriter, clubs);
				xml = textWriter.ToString();
			}
			Preferences.Set("clubs", xml);
		}
		public List<Club> GetClubs()
		{
			var xml = Preferences.Get("clubs", null);
			var clubs = new List<Club>();
			if (xml != null)
			{
				var xs = new XmlSerializer(typeof(List<Club>));
				using (var reader = new StringReader(xml))
				{
					clubs = xs.Deserialize(reader) as List<Club>;
				}
			}
			return clubs;
		}
		public bool ClubExists(int id)
		{
			var clubs = GetClubs();
			var club = clubs.FirstOrDefault(x => x.Id == id);
			return club != null ? true : false;
		}

		public Team GetTeam(int id)
		{
			var teams = GetTeams();
			var team = teams.FirstOrDefault(x => x.Id == id);
			return team;
		}
		public void DeleteTeam(int id)
		{
			var teams = GetTeams();
			teams.RemoveAll(x => x.Id == id);
			var xs = new XmlSerializer(typeof(List<Team>));
			string xml;
			using (var textWriter = new StringWriter())
			{
				xs.Serialize(textWriter, teams);
				xml = textWriter.ToString();
			}
			Preferences.Set("teams", xml);
		}
		public List<Team> GetTeams()
		{
			var xml = Preferences.Get("teams", null);
			var teams = new List<Team>();
			if (xml != null)
			{
				var xs = new XmlSerializer(typeof(List<Team>));
				using (var reader = new StringReader(xml))
				{
					teams = xs.Deserialize(reader) as List<Team>;
				}
			}
			return teams;
		}
		public void SaveTeam(Team value)
		{
			var teams = GetTeams();
			teams.Add(value);
			var xs = new XmlSerializer(typeof(List<Team>));
			string xml;
			using (var textWriter = new StringWriter())
			{
				xs.Serialize(textWriter, teams);
				xml = textWriter.ToString();
			}
			Preferences.Set("teams", xml);
		}
		public bool TeamExists(int id)
		{
			var teams = GetTeams();
			var team = teams.FirstOrDefault(x => x.Id == id);
			return team != null ? true : false;
		}

		public Competition GetCompetition(int id)
		{
			var comps = GetCompetitions();
			var comp = comps.FirstOrDefault(x => x.Id == id);
			return comp;
		}
		public void DeleteCompetition(int id)
		{
			var comps = GetCompetitions();
			comps.RemoveAll(x => x.Id == id);
			var xs = new XmlSerializer(typeof(List<Competition>));
			string xml;
			using (var textWriter = new StringWriter())
			{
				xs.Serialize(textWriter, comps);
				xml = textWriter.ToString();
			}
			Preferences.Set("competitions", xml);
		}
		public List<Competition> GetCompetitions()
		{
			var xml = Preferences.Get("competitions", null);
			var comps = new List<Competition>();
			if (xml != null)
			{
				var xs = new XmlSerializer(typeof(List<Competition>));
				using (var reader = new StringReader(xml))
				{
					comps = xs.Deserialize(reader) as List<Competition>;
				}
			}
			return comps;
		}
		public void SaveCompetition(Competition value)
		{
			var comps = GetCompetitions();
			comps.Add(value);
			var xs = new XmlSerializer(typeof(List<Competition>));
			string xml;
			using (var textWriter = new StringWriter())
			{
				xs.Serialize(textWriter, comps);
				xml = textWriter.ToString();
			}
			Preferences.Set("competitions", xml);
		}
		public bool CompetitionExists(int id)
		{
			var comps = GetCompetitions();
			var comp = comps.FirstOrDefault(x => x.Id == id);
			return comp != null ? true : false;
		}

		public bool GetNotificationsSettings() => Preferences.Get("notificationsSet", true);
		public void SetNotificationsSettings(bool value) => Preferences.Set("notificationsSet", value);
		public int GetNotificationsMinutes() => Preferences.Get("notificationsMinutes", 0);
		public void SetNotificationsMinutes(int value) => Preferences.Set("notificationsMinutes", value);
	}
}
