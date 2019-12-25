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
	public partial class ClubDetailPage : ContentPage
	{
		ClubDetailViewModel viewModel;
		public ClubDetailPage(int id)
		{
			InitializeComponent();
			BindingContext = viewModel = new ClubDetailViewModel(id);
		}
		public ClubDetailPage()
		{
			int id = 1319465;
			InitializeComponent();
			BindingContext = viewModel = new ClubDetailViewModel(id);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (!viewModel.IsLoaded)
				viewModel.LoadClubCommand.Execute(null);
		}

		private async void TeamsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!(e.CurrentSelection.FirstOrDefault() is Team team))
				return;

			await Navigation.PushAsync(new TeamDetailPage(team.Id, viewModel.Club));

			TeamsList.SelectedItem = null;
		}

		private async void Button_Clicked(object sender, EventArgs e)
		{
			await DisplayAlert("Kontakt", $"Kontaktná osoba: {viewModel.Club.Contact.PersonName}\n" +
				$"Email: {viewModel.Club.Contact.Email.FirstOrDefault()}\n" +
				$"Mobil: {viewModel.Club.Contact.Phone.FirstOrDefault()}\n" +
				$"Fax: {viewModel.Club.Contact.Fax.FirstOrDefault()}", "OK");
		}
	}
}