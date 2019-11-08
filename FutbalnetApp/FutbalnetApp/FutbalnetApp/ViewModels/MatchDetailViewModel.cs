using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutbalnetApp.ViewModels
{
	public class MatchDetailViewModel : BaseViewModel
	{
		public int MatchId { get; set; }
		public Match Match { get; set; }
		public Command LoadMatchCommand { get; set; }

		public MatchDetailViewModel(int id)
		{
			MatchId = id;
			LoadMatchCommand = new Command(async () => await ExecuteLoadMatchCommand());
		}

		async Task ExecuteLoadMatchCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				Match = await SportnetStore.GetMatchAsync(MatchId);
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
