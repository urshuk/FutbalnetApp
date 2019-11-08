using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutbalnetApp.ViewModels
{
	public class PersonDetailViewModel : BaseViewModel
	{
		public int PersonId { get; set; }
		public Person Person { get; set; }
		public Command LoadPersonCommand { get; set; }

		public PersonDetailViewModel(int id)
		{
			PersonId = id;
			LoadPersonCommand = new Command(async () => await ExecuteLoadPersonCommand());
		}

		async Task ExecuteLoadPersonCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Person = await SportnetStore.GetPersonAsync(PersonId);
				IsLoaded = true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
