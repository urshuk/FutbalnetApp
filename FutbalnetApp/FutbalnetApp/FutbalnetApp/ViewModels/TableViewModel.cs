using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FutbalnetApp.ViewModels
{
	public class TableViewModel : BaseViewModel
	{
		public IEnumerable<CompetitionTableClub> TableClubs { get; set; }
		public ObservableCollection<CompetitionTableClub> OrderedTableClubs { get; set; }
		public Command OrderTableCommand { get; set; }
		int tableOrderIndex = 0;
		public int TableOrderIndex
		{
			get => tableOrderIndex;
			set => SetProperty(ref tableOrderIndex, value);
		}

		public TableViewModel(IEnumerable<CompetitionTableClub> tableClubs)
		{
			OrderedTableClubs = new ObservableCollection<CompetitionTableClub>();
			OrderTableCommand = new Command<string>((parameter) => ExecuteOrderTableCommand(parameter));
			TableClubs = tableClubs;
		}

		private void ExecuteOrderTableCommand(string by)
		{
			switch (by)
			{
				case "Matches":
					var matches = TableClubs.OrderByDescending(x => x.MP);
					OrderedTableClubs.Clear();
					foreach (var item in matches)
					{
						OrderedTableClubs.Add(item);
					};
					TableOrderIndex = 0;
					break;
				case "Wins":
					var wins = TableClubs.OrderByDescending(x => x.W);
					OrderedTableClubs.Clear();
					foreach (var item in wins)
					{
						OrderedTableClubs.Add(item);
					};
					TableOrderIndex = 1;
					break;
				case "Draws":
					var draws = TableClubs.OrderByDescending(x => x.D);
					OrderedTableClubs.Clear();
					foreach (var item in draws)
					{
						OrderedTableClubs.Add(item);
					};
					TableOrderIndex = 2;
					break;
				case "Losses":
					var losses = TableClubs.OrderByDescending(x => x.L);
					OrderedTableClubs.Clear();
					foreach (var item in losses)
					{
						OrderedTableClubs.Add(item);
					};
					TableOrderIndex = 3;
					break;
				case "Score":
					var score = TableClubs.OrderByDescending(x => x.ScoreDifference);
					OrderedTableClubs.Clear();
					foreach (var item in score)
					{
						OrderedTableClubs.Add(item);
					};
					TableOrderIndex = 4;
					break;
				case "Points":
					var points = TableClubs.OrderBy(x => x.Order);
					OrderedTableClubs.Clear();
					foreach (var item in points)
					{
						OrderedTableClubs.Add(item);
					};
					TableOrderIndex = 5;
					break;
				default:
					break;
			}
		}

	}
}
