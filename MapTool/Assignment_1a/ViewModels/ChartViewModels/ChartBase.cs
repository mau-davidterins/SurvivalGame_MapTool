using Assignment_1a.Models;
using Assignment_1a.Services;
using Assignment_1a.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Assignment_1a.ViewModels.ChartViewModels
{

	public abstract class ChartBase : ViewModelBase
	{
		protected ObservableCollection<ChartModel> _chartDataModelCollection;
		protected StatisticsCalculator _statisticsCalculator;
		protected string _title = "No Title";
		protected string _subTitle = "";

		public ChartBase(List<LogFileModel> logDataFiles)
		{
			_statisticsCalculator = new StatisticsCalculator();
			EditInfoViewModel = new ChartEditInfoViewModel(ref logDataFiles);
			_chartDataModelCollection = new ObservableCollection<ChartModel>();
			SetLogFileData(logDataFiles);
			DeleteCommand = new ActionCommand(Delete);
			EditCommand = new ActionCommand(Edit);
		}

		public event EventHandler OnDeleteItemEvent;
		public event EventHandler OnItemFinsihedEditEvent;
		public event EventHandler OnItemEditEvent;


		public ChartEditInfoViewModel EditInfoViewModel { get; }
		public string ChartType { get; protected set; }
		public string Title { get { return _title; } set { _title = value; OnPropertyChanged(nameof(Title)); } }
		public string SubTitle { get { return _subTitle; } set { _subTitle = value; OnPropertyChanged(nameof(SubTitle)); } }

		public ICommand DeleteCommand { get; }
		public ICommand EditCommand { get; }
		public ICommand FinsishEditCommand { get; }
		public ICommand CancelEditCommand { get; }

		public ObservableCollection<ChartModel> ChartDataModelCollection { get { return _chartDataModelCollection; } }

		public virtual void SetLogFileData(List<LogFileModel> logDataFiles)
		{
			_statisticsCalculator.StatParameters(LogType.Death, "", logDataFiles);
			_chartDataModelCollection = _statisticsCalculator.GetAverageResourceChart();
		}

		protected void Delete()
		{
			Console.WriteLine("Delete");
			OnDeleteItemEvent.Invoke(this, new EventArgs());
		}

		protected void FindishEditCommand()
		{
			OnItemFinsihedEditEvent.Invoke(this, new EventArgs());
		}

		protected void Edit()
		{
			Console.WriteLine("Edit");
			OnItemEditEvent.Invoke(this, new EventArgs());

		}
	}

	public class ChartCollection : ObservableCollection<ChartBase>
	{
		public event EventHandler<ChartBase> OnItemEdit;

		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{

			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				ChartBase newChart = (ChartBase)e.NewItems[0];
				newChart.OnDeleteItemEvent += NewChart_OnDeleteItemEvent;
				newChart.OnItemFinsihedEditEvent += NewChart_OnItemFinishedEditEvent;
				newChart.OnItemEditEvent += NewChart_OnItemEditEvent;
			}
			if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				ChartBase newChart = (ChartBase)e.OldItems[0];
				newChart.OnDeleteItemEvent -= NewChart_OnDeleteItemEvent;
				newChart.OnItemFinsihedEditEvent -= NewChart_OnItemFinishedEditEvent;
				newChart.OnItemEditEvent -= NewChart_OnItemEditEvent;

			}

			base.OnCollectionChanged(e);
		}

		public void Replace(ChartBase oldItem, ChartBase newItem)
		{
			int workingIndex = IndexOf(oldItem);
			RemoveItem(workingIndex);
			Insert(workingIndex, newItem);
		}

		private void NewChart_OnItemEditEvent(object sender, EventArgs e)
		{
			OnItemEdit.Invoke(this, (ChartBase)sender);
		}

		private void NewChart_OnItemFinishedEditEvent(object sender, EventArgs e)
		{

		}

		private void NewChart_OnDeleteItemEvent(object sender, EventArgs e)
		{
			Remove((ChartBase)sender);
		}
	}
}
