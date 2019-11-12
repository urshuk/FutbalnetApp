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
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();
		}

		private void Switch_Toggled(object sender, ToggledEventArgs e)
		{
			if (e.Value == true)
			{
				Application.Current.Resources["BackgroundColor"] = Application.Current.Resources["BackgroundColorDark"];
				Application.Current.Resources["AlternativeColor"] = Application.Current.Resources["AlternativeColorDark"];
				//Application.Current.Resources["DarkAlternativeColor"] = Application.Current.Resources["DarkAlternativeColorDark"];
				Application.Current.Resources["TextColor"] = Application.Current.Resources["TextColorDark"];
			}
			else
			{
				Application.Current.Resources["BackgroundColor"] = Application.Current.Resources["BackgroundColorLight"];
				Application.Current.Resources["AlternativeColor"] = Application.Current.Resources["AlternativeColorLight"];
				//Application.Current.Resources["DarkAlternativeColor"] = Application.Current.Resources["DarkAlternativeColorLight"];
				Application.Current.Resources["TextColor"] = Application.Current.Resources["TextColorLight"];
			}
		}
	}
}