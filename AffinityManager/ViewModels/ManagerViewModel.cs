using Privatest;
using System.Collections.Generic;

namespace AffinityManager.ViewModels
{
	internal sealed class ManagerViewModel : BaseViewModel
	{
		[BackingField] private IReadOnlyCollection<RuleViewModel> _rules = new List<RuleViewModel>();
		public IReadOnlyCollection<RuleViewModel> Rules
		{
			get => _rules;
			set => Set(ref _rules, value);
		}

		public ManagerViewModel()
		{
			Rules = new List<RuleViewModel>
			{
				new RuleViewModel(),
				new RuleViewModel(),
				new RuleViewModel()
			};
		}
	}
}
