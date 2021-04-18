using Privatest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AffinityManager.ViewModels
{
	internal sealed class RuleViewModel : BaseViewModel
	{
		[BackingField] private string _name;
		public string Name
		{
			get => _name;
			set => Set(ref _name, value);
		}

		[BackingField] private IReadOnlyCollection<CoreViewModel> _cores = new List<CoreViewModel>();
		public IReadOnlyCollection<CoreViewModel> Cores
		{
			get => _cores;
			set => Set(ref _cores, value);
		}

		public RuleViewModel()
		{
			Cores = Enumerable.Range(0, 10).Select(_ => new CoreViewModel { IsEnabled = new Random().Next() % 2 == 0 }).ToList();
		}
	}
}
