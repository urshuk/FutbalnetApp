using FutbalnetApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FutbalnetApp.ViewModels
{
	public class CompetitionSelectionViewModel : BaseViewModel
	{
		public ObservableCollection<CompetitionPreview> MenCompetitions { get; set; }
		public ObservableCollection<CompetitionPreview> WomenCompetitions { get; set; }
		public ObservableCollection<CompetitionPreview> YoungCompetitions { get; set; }
		public ObservableCollection<UnionPreview> SubUnions { get; set; }
		public IEnumerable<CompetitionPreview> AllCompetitions { get; set; }
		public IEnumerable<UnionPreview> AllUnions { get; set; }
		int selectedCompetitionIndex = 0;
		public int SelectedCompetitionIndex
		{
			get => selectedCompetitionIndex;
			set => SetProperty(ref selectedCompetitionIndex, value);
		}
		public int SelectedCategoryIndex { get; set; }
		public Command LoadCompetitionsCommand { get; set; }
		public Command ReloadListsCommand { get; set; }
		private UnionPreview union;
		public UnionPreview Union
		{
			get => union;
			set => SetProperty(ref union, value);
		}

		public CompetitionSelectionViewModel()
		{
			SubUnions = new ObservableCollection<UnionPreview>();
			MenCompetitions = new ObservableCollection<CompetitionPreview>();
			WomenCompetitions = new ObservableCollection<CompetitionPreview>();
			YoungCompetitions = new ObservableCollection<CompetitionPreview>();
			LoadCompetitionsCommand = new Command(async () => await ExecuteLoadCompetitionsCommand());
			ReloadListsCommand = new Command(() => ReloadLists());
		}

		private void ReloadLists()
		{
			MenCompetitions.Clear();
			WomenCompetitions.Clear();
			YoungCompetitions.Clear();
			SubUnions.Clear();
			foreach (var comp in AllCompetitions)
			{
				if (comp.UnionId == Union.Id)
				{
					if (comp.Category == "ADULTS")
					{
						if (comp.Sex == "M")
							MenCompetitions.Add(comp);
						else
							WomenCompetitions.Add(comp);
					}
					else
						YoungCompetitions.Add(comp);
				}
			}
			var parentUnion = GetParentUnion(Union);
			if (parentUnion != null)
				SubUnions.Add(parentUnion);

			if (Union.SubUnions != null)
			{
				foreach (var sub in Union.SubUnions)
				{
					SubUnions.Add(sub);
				}
			}
			SelectedCompetitionIndex = 0;
		}
		private UnionPreview GetParentUnion(UnionPreview union)
		{
			foreach (var item in AllUnions.FirstOrDefault().SubUnions)
			{
				if (item.ParentUnionId == union.ParentUnionId)
					return AllUnions.FirstOrDefault();
				else if (item.Id == union.ParentUnionId)
					return item;
			}
			return null;
		}

		async Task ExecuteLoadCompetitionsCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			try
			{
				AllCompetitions = await SportnetStore.GetActiveCompetitionsAsync(App.Seasons.First(x => x.IsActual));
				AllUnions = await SportnetStore.GetUnionsAsync();
				Union = AllUnions.FirstOrDefault();
				ReloadLists();
				IsLoaded = true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
				var log = new ErrorLog()
				{
					ExceptionType = ex.GetType().ToString(),
					Status = ErrorLog.LogStatus.Unread,
					Message = ex.Message,
					Action = "Loading competitions",
					Datetime = DateTime.Now,
				};
				await LogError(log);
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
