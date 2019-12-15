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
	public partial class FavouritesPage : ContentPage
	{
		FavouritesViewModel viewModel;

		public FavouritesPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new FavouritesViewModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			viewModel.LoadFavouritesCommand.Execute(null);
		}

		private async void FavouritesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var fav = e.CurrentSelection.FirstOrDefault();
			if (fav is Competition)
			{
				await Navigation.PushAsync(new CompetitionDetailPage((fav as Competition).Id));
			}
			else if (fav is Team)
			{
				await Navigation.PushAsync(new TeamDetailPage((fav as Team)));
			}
			else if (fav is Club)
			{
				await Navigation.PushAsync(new ClubDetailPage((fav as Club).Id));
			}
			else
			{
				return;
			}

			// Manually deselect item.
			FavouritesList.SelectedItem = null;
		}
	}
}