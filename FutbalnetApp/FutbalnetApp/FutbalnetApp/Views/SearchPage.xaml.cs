using FutbalnetApp.Models;
using FutbalnetApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FutbalnetApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
		SearchViewModel viewModel;
		private CancellationTokenSource throttleCts;

		public SearchPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new SearchViewModel();
			throttleCts = new CancellationTokenSource();
		}

		private async void PersonsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!(e.CurrentSelection.FirstOrDefault() is SearchResult item))
				return;

			await Navigation.PushAsync(new PersonDetailPage(item.Id));
			PersonsList.SelectedItem = null;
		}

		private async void ClubsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!(e.CurrentSelection.FirstOrDefault() is SearchResult item))
				return;

			await Navigation.PushAsync(new ClubDetailPage(item.Id));
			ClubsList.SelectedItem = null;
		}

		private async void CompetitionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!(e.CurrentSelection.FirstOrDefault() is SearchResult item))
				return;

			await Navigation.PushAsync(new CompetitionDetailPage(item.Id));
			CompetitionsList.SelectedItem = null;
		}

		private async void UnionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!(e.CurrentSelection.FirstOrDefault() is SearchResult item))
				return;

			//await Navigation.PushAsync(new UnionDetailPage(item.Id));
			UnionsList.SelectedItem = null;
		}

		private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (SearchBar.Text?.Length > 2)
			{
				//viewModel.LoadSearchResultsCommand.Execute(SearchBar.Text);
				Interlocked.Exchange(ref this.throttleCts, new CancellationTokenSource()).Cancel();
				Task.Delay(TimeSpan.FromMilliseconds(500), this.throttleCts.Token) // if no keystroke occurs, carry on after 500ms
					.ContinueWith(
						delegate { viewModel.LoadSearchResultsCommand.Execute(SearchBar.Text); },
						CancellationToken.None,
						TaskContinuationOptions.OnlyOnRanToCompletion,
						TaskScheduler.FromCurrentSynchronizationContext());
			}
			else
			{
				viewModel.ClearResults();
			}
		}
	}
}