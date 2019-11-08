using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using FutbalnetApp.Models;
using FutbalnetApp.Views;

namespace FutbalnetApp.ViewModels
{
	public class ItemsViewModel : BaseViewModel
	{
		public ObservableCollection<Item> Items { get; set; }
		public Command LoadItemsCommand { get; set; }

		public ItemsViewModel()
		{
			Title = "Browse";
			Items = new ObservableCollection<Item>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
			{
				var newItem = item as Item;
				Items.Add(newItem);
				await DataStore.AddItemAsync(newItem);
			});
		}

		async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				//var test = await SportnetStore.GetActiveCompetitionsAsync(new Season { Id = "2019-2020" });
				//var test = await SportnetStore.GetClubAsync(1413);
				//var test = await SportnetStore.GetClubTeamsAsync(1872);
				//var test = await SportnetStore.GetCompetitionAsync(2984);
				//var test = await SportnetStore.GetCompetitionStatsAsync(2984);
				//var test = await SportnetStore.GetCompetitionTableAsync(2984);
				//var test = await SportnetStore.GetMatchAsync(444151);
				//var test = await SportnetStore.GetMatchAsync(444243);
				//var test = await SportnetStore.GetCompetitionRoundAsync(2984,4902,87900);
				//var test = await SportnetStore.GetPastCompetitionsAsync(2984);
				//var test = await SportnetStore.GetPersonAsync(1319465);
				//var test = await SportnetStore.GetPlayerAsync(1319465);
				//var test = await SportnetStore.GetPlayerStatsBySeasonAsync(1319465, new Season { Id = "2019-2020" });
				//var test = await SportnetStore.GetPlayerStatsSummaryAsync(1319465);
				//var test = await SportnetStore.GetSearchResults("vit");
				//var test = await SportnetStore.GetSeasonsAsync();
				//var test = await SportnetStore.GetStadiumAsync(657);
				//var test = await SportnetStore.GetTeamAsync(36812);
				//var test = await SportnetStore.GetTrainerAsync(1136680);
				//var test = await SportnetStore.GetUnionAsync(38);
				//var test = await SportnetStore.GetUnionsAsync();
				Items.Clear();
				var items = await DataStore.GetItemsAsync(true);
				foreach (var item in items)
				{
					Items.Add(item);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}