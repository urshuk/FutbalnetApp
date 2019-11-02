using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using FutbalnetApp.Models;

namespace FutbalnetApp.Services
{
	public class SportnetDataStore : ISportnetDataStore
	{
		HttpClient client;

		public SportnetDataStore()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri($"{App.SportnetApiUrl}/");
		}

		bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

		public async Task<IEnumerable<CompetitionPreview>> GetActiveCompetitionsAsync(Season season)
		{
			if (season != null && IsConnected)
			{
				var json = await client.GetStringAsync($"season/{season.Id}/competitions");
				return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<CompetitionPreview>>(json));
			}

			return null;
		}

		public Task<Club> GetClubAsync(int id) => throw new NotImplementedException();
		public Task<IEnumerable<Team>> GetClubTeamsAsync(int clubId) => throw new NotImplementedException();
		public Task<Competition> GetCompetitionAsync(int id) => throw new NotImplementedException();
		public Task<CompetitionStats> GetCompetitionStatsAsync(int id, int compPartId = 0) => throw new NotImplementedException();
		public Task<CompetitionTable> GetCompetitionTableAsync(int id, int compPartId = 0) => throw new NotImplementedException();
		public Task<Match> GetMatchAsync(int id) => throw new NotImplementedException();
		public Task<IEnumerable<Match>> GetMatchesByRoundAsync(int competitionId, int partId, int roundId) => throw new NotImplementedException();
		public Task<IEnumerable<Competition>> GetPastCompetitionsAsync(int id) => throw new NotImplementedException();
		public Task<Person> GetPersonAsync(int id) => throw new NotImplementedException();
		public Task<Player> GetPlayerAsync(int id) => throw new NotImplementedException();
		public Task<PlayerStats> GetPlayerStatsBySeasonAsync(int id, Season season) => throw new NotImplementedException();
		public Task<PlayerStats> GetPlayerStatsSummaryAsync(int id) => throw new NotImplementedException();
		public Task<IEnumerable<SearchResult>> GetSearchResults(string term) => throw new NotImplementedException();
		public Task<IEnumerable<Season>> GetSeasonsAsync() => throw new NotImplementedException();
		public Task<Stadium> GetStadiumAsync(int id) => throw new NotImplementedException();
		public Task<Team> GetTeamAsync(int id) => throw new NotImplementedException();
		public Task<Trainer> GetTrainerAsync(int id) => throw new NotImplementedException();
		public Task<Union> GetUnionAsync(int id) => throw new NotImplementedException();
		public Task<IEnumerable<Union>> GetUnionsAsync() => throw new NotImplementedException();
	}
}
