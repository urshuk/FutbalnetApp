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
	public partial class CompetitionStatsView : ContentView
	{
		public CompetitionStatsView()
		{
			InitializeComponent();
		}

		public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create("ItemsSource", typeof(IEnumerable), typeof(CompetitionStatsView));
		public IEnumerable ItemsSource
		{
			get => (IEnumerable)GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}
		public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(CompetitionStatsView));
		public ICommand Command
		{
			get => (ICommand)GetValue(CommandProperty);
			set => SetValue(CommandProperty, value);
		}
		public static readonly BindableProperty OrderIndexProperty = BindableProperty.Create("OrderIndex", typeof(int), typeof(CompetitionStatsView));
		public int OrderIndex
		{
			get => (int)GetValue(OrderIndexProperty);
			set => SetValue(OrderIndexProperty, value);
		}

		private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}