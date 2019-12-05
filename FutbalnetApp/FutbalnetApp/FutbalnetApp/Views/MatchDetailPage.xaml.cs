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
			var eventItem = e.CurrentSelection.FirstOrDefault() as MatchEventViewModel;

			if (eventItem == null)
				return;

			await Navigation.PushAsync(new PersonDetailPage(eventItem.Player.Id));

			// Manually deselect item.
			EventList.SelectedItem = null;
		}
	}
}