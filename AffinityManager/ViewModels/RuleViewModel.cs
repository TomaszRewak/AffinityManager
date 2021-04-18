using Privatest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		[BackingField] private IReadOnlyCollection<bool> _cores = new List<bool>();
		public IReadOnlyCollection<bool> Cores
		{
			get => _cores;
			set => Set(ref _cores, value);
		}

		public RuleViewModel()
		{
			Cores = Enumerable.Range(0, 10).Select(_ => new Random().Next() % 2 == 0).ToList();
		}
	}
}
