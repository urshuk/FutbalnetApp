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
	public partial class TeamDetailPage : ContentPage
	{
		TeamDetailViewModel viewModel;
		public TeamDetailPage(int id, Club club)
		{
			InitializeComponent();
			BindingContext = viewModel = new TeamDetailViewModel(id, club);
		}
		public TeamDetailPage(Team team)
		{
			InitializeComponent();
			BindingContext = viewModel = new TeamDetailViewModel(team);
		}
		public TeamDetailPage()
		{
			int id = 1319465;
			InitializeComponent();
			BindingContext = viewModel = new TeamDetailViewModel(id, null);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (!viewModel.IsLoaded)
				viewModel.LoadTeamCommand.Execute(null);
		}

		private async void MatchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			if (!(e.CurrentSelection.FirstOrDefault() is MatchPreview match))
				return;

			await Navigation.PushAsync(new MatchDetailPage(match.Id));

			// Manually deselect item.
			MatchList.SelectedItem = null;
		}

		private async void Competition_Tapped(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new CompetitionDetailPage(viewModel.Competition.Id));
		}
	}
}