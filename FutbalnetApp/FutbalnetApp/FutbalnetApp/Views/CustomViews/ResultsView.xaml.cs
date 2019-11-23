using FutbalnetApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FutbalnetApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultsView : ContentView
	{
		public ResultsView()
		{
			InitializeComponent();
		}

		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(ResultsView));
		public IEnumerable ItemsSource
		{
			get => (IEnumerable)GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}
		public static readonly BindableProperty PartProperty = BindableProperty.Create("Part", typeof(CompetitionPart), typeof(ResultsView));
		public CompetitionPart Part
		{
			get => (CompetitionPart)GetValue(PartProperty);
			set => SetValue(PartProperty, value);
		}
		public static readonly BindableProperty RoundProperty = BindableProperty.Create("Round", typeof(CompetitionRound), typeof(ResultsView));
		public CompetitionRound Round
		{
			get => (CompetitionRound)GetValue(RoundProperty);
			set => SetValue(RoundProperty, value);
		}
		public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(CompetitionStatsView));
		public ICommand Command
		{
			get => (ICommand)GetValue(CommandProperty);
			set => SetValue(CommandProperty, value);
		}

		async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var match = e.CurrentSelection.FirstOrDefault() as MatchPreview;

			if (match == null)
				return;

			await Navigation.PushAsync(new MatchDetailPage(match.Id));

			// Manually deselect item.
			MatchList.SelectedItem = null;
		}

		private void OnRoundChanged(object sender, EventArgs e)
		{
			Command.Execute(null);
		}
	}
}