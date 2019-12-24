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
	public partial class TimelinePage : ContentPage
	{
		TimelineViewModel viewModel;
		public TimelinePage()
		{
			InitializeComponent();
			BindingContext = viewModel = new TimelineViewModel();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			viewModel.LoadTimelineCommand.Execute(null);
		}

		private async void TimelineList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!(e.CurrentSelection.FirstOrDefault() is MatchPreview item))
				return;

			await Navigation.PushAsync(new MatchDetailPage(item.Id));
			TimelineList.SelectedItem = null;
		}
	}
}