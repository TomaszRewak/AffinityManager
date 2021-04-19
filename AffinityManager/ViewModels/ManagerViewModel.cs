using Privatest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Threading;

namespace AffinityManager.ViewModels
{
	internal sealed class ManagerViewModel : BaseViewModel
	{
		private readonly DispatcherTimer _timer;

		[BackingField] private IReadOnlyCollection<RuleViewModel> _rules = new List<RuleViewModel>();
		public IReadOnlyCollection<RuleViewModel> Rules
		{
			get => _rules;
			set => Set(ref _rules, value);
		}

		[BackingField] private int _numberOfProcesses;
		public int NumberOfProcesses
		{
			get => _numberOfProcesses;
			set => Set(ref _numberOfProcesses, value);
		}

		public ManagerViewModel()
		{
			Rules = new List<RuleViewModel>
			{
				new RuleViewModel(),
				new RuleViewModel(),
				new RuleViewModel()
			};

			_timer = new DispatcherTimer(TimeSpan.FromSeconds(10), DispatcherPriority.Background, (e, a) => UpdateProcesses(), Dispatcher.CurrentDispatcher);
			_timer.Start();
		}

		private void UpdateProcesses()
		{
			NumberOfProcesses = Process.GetProcesses().Length;
		}
	}
}
