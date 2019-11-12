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
	public partial class ActivityIndicatorView : ContentView
	{
		public ActivityIndicatorView()
		{
			InitializeComponent();
		}

		public static readonly BindableProperty IsRunningProperty = BindableProperty.Create("IsRunning", typeof(bool), typeof(ActivityIndicatorView));
		public bool IsRunning
		{
			get => (bool)GetValue(IsRunningProperty);
			set => SetValue(IsRunningProperty, value);
		}
	}
}