﻿using FutbalnetApp.Models;
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
	public partial class CompetitionSelectionPage : ContentPage
	{
		CompetitionSelectionViewModel viewModel;
		public CompetitionSelectionPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new CompetitionSelectionViewModel();
		}

		private void UnionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!(e.CurrentSelection.FirstOrDefault() is UnionPreview union))
				return;

			viewModel.Union = union;
			viewModel.ReloadListsCommand.Execute(null);
			competitionSelector.SelectedIndex = 0;
			//viewModel.ParentUnionId = union.Id;

			// Manually deselect item.
			UnionList.SelectedItem = null;
		}

		async void OnCompetitionSelected(object sender, SelectionChangedEventArgs e)
		{
			if (!(e.CurrentSelection.FirstOrDefault() is CompetitionPreview comp))
				return;

			await Navigation.PushAsync(new CompetitionDetailPage(comp.Id));

			// Manually deselect item.
			MCompsList.SelectedItem = null;
			WCompsList.SelectedItem = null;
			YCompsList.SelectedItem = null;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (!viewModel.IsLoaded)
				viewModel.LoadCompetitionsCommand.Execute(null);
		}
	}
}