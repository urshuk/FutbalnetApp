using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FutbalnetApp.Services
{
	public interface ISportnetDataStore
	{
		//CLUBS AND TEAMS
		Task<Club> GetClubAsync(int id);
		Task<Team> GetTeamAsync(int id);
		Task<IEnumerable<Team>> GetClubTeamsAsync(int clubId);

		//COMPETITIONS
		Task<Competition> GetCompetitionAsync(int id);
		Task<IEnumerable<Competition>> GetActiveCompetitionsAsync(Season season);
		Task<IEnumerable<Competition>> GetPastCompetitionsAsync(int id);
		Task<CompetitionTable> GetCompetitionTableAsync(int id, int compPartId = 0);
		Task<CompetitionStats> GetCompetitionStatsAsync(int id, int compPartId = 0);

		//SEASON
		Task<IEnumerable<Season>> GetSeasonsAsync();
		
		//UNIONS
		Task<IEnumerable<Union>> GetUnionsAsync();
		Task<Union> GetUnionAsync(int id);
				
		//PERSONS
		Task<Person> GetPersonAsync(int id);
		Task<Player> GetPlayerAsync(int id);
		Task<Trainer> GetTrainerAsync(int id);
		Task<PlayerStats> GetPlayerStatsSummaryAsync(int id);
		Task<PlayerStats> GetPlayerStatsBySeasonAsync(int id, Season season);


		//MATCHES
		Task<IEnumerable<Match>> GetMatchesByRoundAsync(int competitionId, int partId, int roundId);
		Task<Match> GetMatchAsync(int id);

		//MISC
		Task<Stadium> GetStadiumAsync(int id);
		Task<IEnumerable<SearchResult>> GetSearchResults(string term);
	}
}
