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

		//CLUBS AND TEAMS
		public async Task<Club> GetClubAsync(int id)
		{
			if (id > 0 && IsConnected)
			{
				var json = await client.GetStringAsync($"clubs/{id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<Club>(json));
			}

			return null;
		}
		public async Task<IEnumerable<Team>> GetClubTeamsAsync(int clubId)
		{
			if (clubId > 0 && IsConnected)
			{
				var json = await client.GetStringAsync($"clubs/{clubId}/teams");
				return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Team>>(json));
			}

			return null;
		}
		public async Task<Team> GetTeamAsync(int id)
		{
			if (id > 0 && IsConnected)
			{
				var json = await client.GetStringAsync($"teams/{id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<Team>(json));
			}

			return null;
		}

		//COMPETITIONS
		public async Task<IEnumerable<CompetitionPreview>> GetActiveCompetitionsAsync(Season season)
		{
			if (season != null && IsConnected)
			{
				var json = await client.GetStringAsync($"season/{season.Id}/competitions");
				return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<CompetitionPreview>>(json));
			}

			return null;
		}
		public async Task<Competition> GetCompetitionAsync(int id)
		{
			if (id > 0 && IsConnected)
			{
				var json = await client.GetStringAsync($"competitions/{id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<Competition>(json));
			}

			return null;
		}
		public async Task<CompetitionStats> GetCompetitionStatsAsync(int id, int compPartId = 0)
		{
			if (id > 0 && IsConnected)
			{
				if (compPartId > 0)
				{
					var json = await client.GetStringAsync($"competitions/{id}/parts/{compPartId}/statistics");
					return await Task.Run(() => JsonConvert.DeserializeObject<CompetitionStats>(json));
				}
				else
				{
					var json = await client.GetStringAsync($"competitions/{id}/statistics");
					return await Task.Run(() => JsonConvert.DeserializeObject<CompetitionStats>(json));
				}
			}
			return null;
		}
		public async Task<CompetitionTable> GetCompetitionTableAsync(int id, int compPartId = 0)
		{
			if (id > 0 && IsConnected)
			{
				if (compPartId > 0)
				{
					var json = await client.GetStringAsync($"competitions/{id}/parts/{compPartId}/table");
					return await Task.Run(() => JsonConvert.DeserializeObject<CompetitionTable>(json));
				}
				else
				{
					var json = await client.GetStringAsync($"competitions/{id}/table");
					return await Task.Run(() => JsonConvert.DeserializeObject<CompetitionTable>(json));
				}
			}
			return null;
		}
		public async Task<CompetitionRound> GetCompetitionRoundAsync(int competitionId, int partId, int roundId)
		{
			if (competitionId > 0 && partId > 0 && roundId > 0 && IsConnected)
			{
				var json = await client.GetStringAsync($"competitions/{competitionId}/parts/{partId}/rounds/{roundId}");
				return await Task.Run(() => JsonConvert.DeserializeObject<CompetitionRound>(json));
			}

			return null;
		}
		public async Task<IEnumerable<CompetitionSeason>> GetPastCompetitionsAsync(int id)
		{
			if (id > 0 && IsConnected)
			{
				var json = await client.GetStringAsync($"competitions/{id}/seasons");
				return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<CompetitionSeason>>(json));
			}

			return null;
		}

		//SEASON
		public async Task<IEnumerable<Season>> GetSeasonsAsync()
		{
			if (IsConnected)
			{
				var json = await client.GetStringAsync($"season");
				return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Season>>(json));
			}

			return null;
		}

		//UNIONS
		public async Task<Union> GetUnionAsync(int id)
		{
			if (id > 0 && IsConnected)
			{
				var json = await client.GetStringAsync($"unions/{id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<Union>(json));
			}

			return null;
		}
		public async Task<IEnumerable<UnionPreview>> GetUnionsAsync()
		{
			if (IsConnected)
			{
				var json = await client.GetStringAsync($"unions");
				return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<UnionPreview>>(json));
			}

			return null;
		}

		//PERSONS
		public async Task<Person> GetPersonAsync(int id)
		{
			if (id > 0 && IsConnected)
			{
				var json = await client.GetStringAsync($"persons/{id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<Person>(json));
			}

			return null;
		}
		public async Task<Player> GetPlayerAsync(int id)
		{
			if (id > 0 && IsConnected)
			{
				var json = await client.GetStringAsync($"players/{id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<Player>(json));
			}

			return null;
		}
		public async Task<IEnumerable<Stat>> GetPlayerStatsBySeasonAsync(int id, Season season)
		{
			if (id > 0 && season != null && IsConnected)
			{
				var json = await client.GetStringAsync($"players/{id}/statistics/season/{season.Id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Stat>>(json));
			}

			return null;
		}
		public async Task<IEnumerable<Stat>> GetPlayerStatsSummaryAsync(int id)
		{
			if (id > 0 && IsConnected)
			{
				var json = await client.GetStringAsync($"players/{id}/statistics/");
				return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Stat>>(json));
			}

			return null;
		}
		public async Task<Trainer> GetTrainerAsync(int id)
		{
			if (id > 0 && IsConnected)
			{
				var json = await client.GetStringAsync($"trainers/{id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<Trainer>(json));
			}

			return null;
		}

		//MATCHES
		public async Task<Match> GetMatchAsync(int id)
		{
			if (id > 0 && IsConnected)
			{
				var json = await client.GetStringAsync($"matches/{id}");
				return await Task.Run(() => JsonConvert.DeserializeObject<Match>(json));
			}

			return null;
		}
		
		//MISC
		public async Task<IEnumerable<SearchResult>> GetSearchResults(string term)
		{
			if (!string.IsNullOrWhiteSpace(term) && IsConnected)
			{
				var json = await client.GetStringAsync($"autocomplete/?term={term}");
				return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<SearchResult>>(json));
			}

			return null;
		}
		public async Task<Stadium> GetStadiumAsync(int id)
		{
			if (id > 0 && IsConnected)
			{
				var json = await client.GetStringAsync($"stadium/{id}/");
				return await Task.Run(() => JsonConvert.DeserializeObject<Stadium>(json));
			}

			return null;
		}

	}
}
