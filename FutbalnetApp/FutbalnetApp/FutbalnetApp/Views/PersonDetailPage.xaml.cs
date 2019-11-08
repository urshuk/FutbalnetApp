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
	public partial class PersonDetailPage : ContentPage
	{
		PersonDetailViewModel viewModel;
		public PersonDetailPage(int id)
		{
			InitializeComponent();
			BindingContext = viewModel = new PersonDetailViewModel(id);
		}
		public PersonDetailPage()
		{
			int id = 1319465;
			InitializeComponent();
			BindingContext = viewModel = new PersonDetailViewModel(id);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (!viewModel.IsLoaded)
				viewModel.LoadPersonCommand.Execute(null);
		}
	}
}