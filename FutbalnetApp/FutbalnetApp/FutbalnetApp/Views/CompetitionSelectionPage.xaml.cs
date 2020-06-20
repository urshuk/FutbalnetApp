using FutbalnetApp.Models;
using FutbalnetApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FutbalnetApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	[QueryProperty("UnionId", "unionId")]
	public partial class CompetitionSelectionPage : ContentPage
	{
		CompetitionSelectionViewModel viewModel;
		public CompetitionSelectionPage()
		{
			InitializeComponent();
			BindingContext = viewModel = new CompetitionSelectionViewModel();
		}

		public string UnionId
		{
			set
			{
				int.TryParse(Uri.UnescapeDataString(value), out int unionId);
				foreach (var union in viewModel.AllUnions)
				{
					if (union.Id == unionId)
					{
						viewModel.Union = union;
						break;
					}
					if (union.SubUnions != null)
					{
						foreach (var subUnion in union.SubUnions)
						{
							if (subUnion.Id == unionId)
							{
								viewModel.Union = subUnion;
								break;
							}
							if (subUnion.SubUnions != null)
							{
								foreach (var lastSubUnion in subUnion.SubUnions)
								{
									if (lastSubUnion.Id == unionId)
									{
										viewModel.Union = lastSubUnion;
										break;
									}
								}
							}
						}
					}
				}
				viewModel.ReloadListsCommand.Execute(null);
			}
		}

		private void UnionList_SelectionChanged(object sender, SelectedItemChangedEventArgs e)
		{
			try
			{
				//if (!(e.CurrentSelection.FirstOrDefault() is UnionPreview union))
				//	return;

				if (!(e.SelectedItem is UnionPreview union))
					return;

				viewModel.Union = union;
				viewModel.ReloadListsCommand.Execute(null);
				//viewModel.ParentUnionId = union.Id;
				UnionList.SelectedItem = null;
			}
			catch (Exception err)
			{
				Debug.WriteLine(err);
				throw;
			}
		}

		async void OnCompetitionSelected(object sender, SelectedItemChangedEventArgs e)
		{
			//if (!(e.CurrentSelection.FirstOrDefault() is CompetitionPreview comp))
			//	return;

			if (!(e.SelectedItem is CompetitionPreview comp))
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