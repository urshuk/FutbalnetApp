using FutbalnetApp.Models;
using FutbalnetApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FutbalnetApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MatchDetailPage : ContentPage
	{
		MatchDetailViewModel viewModel;
		public MatchDetailPage(int id)
		{
			InitializeComponent();
			BindingContext = viewModel = new MatchDetailViewModel(id);
		}
		public MatchDetailPage()
		{
			int id = 444242;
			InitializeComponent();
			BindingContext = viewModel = new MatchDetailViewModel(id);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (!viewModel.IsLoaded)
				viewModel.LoadMatchCommand.Execute(null);
		}

		private async void OnPlayerSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!(e.CurrentSelection.FirstOrDefault() is MatchEventViewModel eventItem))
				return;

			await Navigation.PushAsync(new PersonDetailPage(eventItem.Player.Id));

			// Manually deselect item.
			EventList.SelectedItem = null;
		}

		private async void LineupHomePlayerTapped(object sender, EventArgs e)
		{
			var lineup = (sender as StackLayout).BindingContext as LineupViewModel;
			await Navigation.PushAsync(new PersonDetailPage(lineup.HomePlayer.Person.Id));
		}
		private async void LineupAwayPlayerTapped(object sender, EventArgs e)
		{
			var lineup = (sender as StackLayout).BindingContext as LineupViewModel;
			await Navigation.PushAsync(new PersonDetailPage(lineup.AwayPlayer.Person.Id));
		}

		private async void HomeTeamTapped(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ClubDetailPage(viewModel.Match.Teams.First().Club.Id));
		}
		private async void AwayTeamTapped(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ClubDetailPage(viewModel.Match.Teams.Last().Club.Id));
		}
	}
}