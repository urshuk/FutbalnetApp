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
	public partial class CompetitionDetailPage : ContentPage
	{
		CompetitionDetailViewModel viewModel;
		public CompetitionDetailPage(int id)
		{
			InitializeComponent();
			BindingContext = viewModel = new CompetitionDetailViewModel(id);
		}
		public CompetitionDetailPage()
		{
			int id = 2984;
			InitializeComponent();
			BindingContext = viewModel = new CompetitionDetailViewModel(id);
		}

		private void OnSeasonChanged(object sender, EventArgs e)
		{
			var season = SeasonPicker.SelectedItem as CompetitionSeason;
			if (season == null || season.Id == viewModel.CompetitionId)
				return;

			BindingContext = viewModel = new CompetitionDetailViewModel(season.Id);
			viewModel.LoadCompetitionCommand.Execute(null);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (!viewModel.IsLoaded)
				viewModel.LoadCompetitionCommand.Execute(null);
		}
	}
}