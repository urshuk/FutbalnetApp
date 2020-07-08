using FutbalnetApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FutbalnetApp.Services
{
	public class DatabaseStore : IDatabaseStore
	{
		HttpClient client;

		public DatabaseStore()
		{
			client = new HttpClient
			{
				BaseAddress = new Uri($"{App.FutbalVilleApiUrl}/")
			};
		}
		bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;


		public async Task PostErrorLog(ErrorLog log)
		{
			if (log != null && IsConnected)
			{
				var json = JsonConvert.SerializeObject(log);
				await client.PostAsync("bugs", new StringContent(json, Encoding.UTF8, "application/json"));
			}
		}
	}
}
