using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FutbalnetApp.Services
{
	public interface ILocalDataStore
	{
		Team GetTeam(int id);
		void DeleteTeam(int id);
		List<Team> GetTeams();
		void SaveTeam(Team value);
		bool TeamExists(int id);

		Competition GetCompetition(int id);
		void DeleteCompetition(int id);
		List<Competition> GetCompetitions();
		void SaveCompetition(Competition value);
		bool CompetitionExists(int id);

		Club GetClub(int id);
		void DeleteClub(int id);
		List<Club> GetClubs();
		void SaveClub(Club value);
		bool ClubExists(int id);

		bool GetNotificationsSettings();
		void SetNotificationsSettings(bool value);
		int GetNotificationsMinutes();
		void SetNotificationsMinutes(int value);

		NotificationMatch GetNotification(int matchId);
		void DeleteNotification(int matchId);
		List<NotificationMatch> GetNotificationMatches();
		void SaveNotificationMatch(NotificationMatch match);
		bool NotificationExists(int matchId);

		bool GetAdsSettings();
		void SetAdsSettings(bool value);

		OSAppTheme GetDarkModeSettings();
		void SetDarkModeSettings(OSAppTheme value);
	}
}
