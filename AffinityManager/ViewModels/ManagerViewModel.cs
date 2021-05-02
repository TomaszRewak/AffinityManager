using AffinityManager.Common;
using AffinityManager.Controls;
using AffinityManager.Data;
using AffinityManager.Managers;
using Privatest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;

namespace AffinityManager.ViewModels
{
	internal sealed class ManagerViewModel : BaseViewModel
	{
		private readonly DispatcherTimer _timer;
		private readonly ProcessManager _processManager;

		public ObservableCollection<RuleViewModel> Rules { get; } = new ObservableCollection<RuleViewModel>();

		[BackingField] private int _numberOfProcesses;
		public int NumberOfProcesses
		{
			get => _numberOfProcesses;
			set => Set(ref _numberOfProcesses, value);
		}

		[BackingField] private int _numberOfCores;
		public int NumberOfCores
		{
			get => _numberOfCores;
			set => Set(ref _numberOfCores, value);
		}

		[BackingField] private int _numberOfUnmatchedProcesses;
		public int NumberOfUnmatchedProcesses
		{
			get => _numberOfUnmatchedProcesses;
			set => Set(ref _numberOfUnmatchedProcesses, value);
		}

		[CommandGetter] public ICommand ApplyChangesCommand { get; }
		private void ApplyChanges()
		{
			foreach (var rule in Rules)
				rule.Changed = false;

			UpdateProcesses();
		}

		[CommandGetter] public ICommand AddRuleCommand { get; }
		private void AddRule()
		{
			Rules.Add(new RuleViewModel());
		}

		[CommandGetter] public ICommand DragCommand { get; }
		private void Drag(DragAndDropPanel.DragEventArgs eventArgs)
		{
			Rules.Move(eventArgs.StartIndex, eventArgs.EndIndex);
		}

		public ManagerViewModel(ProcessManager processManager)
		{
			_processManager = processManager;

			AddRule();
			AddRule();
			AddRule();

			_timer = new DispatcherTimer(TimeSpan.FromSeconds(10), DispatcherPriority.Background, (e, a) => UpdateProcesses(), Dispatcher.CurrentDispatcher);
			_timer.Start();

			Rules.CollectionChanged += (s, e) => UpdateProcesses();
			UpdateProcesses();

			NumberOfCores = Environment.ProcessorCount;

			ApplyChangesCommand = new Command(ApplyChanges);
			AddRuleCommand = new Command(AddRule);
			DragCommand = new Command<DragAndDropPanel.DragEventArgs>(Drag);
		}

		private void UpdateProcesses()
		{
			NumberOfProcesses = Process.GetProcesses().Length;

			var processes = _processManager.GetProcesses();

			var rules = Rules
				.Select(r => new AffinityRule(new System.Text.RegularExpressions.Regex(r.Pattern), new IntPtr(0xfffff)))
				.ToArray();

			foreach (var rule in Rules)
			{
				var affinityRule = new AffinityRule(new System.Text.RegularExpressions.Regex(rule.Pattern), new IntPtr(0xfffff));

				var matchingProcesses = processes
					.Where(p => affinityRule.Pattern.IsMatch(p.ProcessName))
					.ToList();

				rule.MatchingProcesses = matchingProcesses.Count;

				processes = processes.Except(matchingProcesses).ToList();
			}
		}
	}
}
