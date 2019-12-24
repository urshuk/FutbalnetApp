using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutbalnetApp.ViewModels
{
	public class SearchViewModel : BaseViewModel
	{
		public ObservableCollection<SearchResult> Clubs { get; set; }
		public ObservableCollection<SearchResult> Persons { get; set; }
		public ObservableCollection<SearchResult> Competitions { get; set; }
		public ObservableCollection<SearchResult> Unions { get; set; }
		public Command LoadSearchResultsCommand { get; set; }
		public int SelectedIndex { get; set; }



		public SearchViewModel()
		{
			LoadSearchResultsCommand = new Command<string>(async (term) => await ExecuteLoadSearchResultsCommand(term));
			Clubs = new ObservableCollection<SearchResult>();
			Persons = new ObservableCollection<SearchResult>();
			Competitions = new ObservableCollection<SearchResult>();
			Unions = new ObservableCollection<SearchResult>();
		}

		async Task ExecuteLoadSearchResultsCommand(string term)
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				var results = await SportnetStore.GetSearchResults(term);
				
				Clubs.Clear();
				Persons.Clear();
				Competitions.Clear();
				Unions.Clear();
				
				foreach (var item in results)
				{
					switch (item.Type)
					{
						case "person":
							Persons.Add(item);
							break;
						case "club":
							Clubs.Add(item);
							break;
						case "competition":
							Competitions.Add(item);
							break;
						case "union":
							Unions.Add(item);
							break;
						default:
							break;
					}
				}

				IsLoaded = true;
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
