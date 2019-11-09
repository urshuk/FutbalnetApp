using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			get { return (IEnumerable)GetValue(ItemsSourceProperty); }
			set
			{
				SetValue(ItemsSourceProperty, value);
			}
		}

		private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}
	}
}