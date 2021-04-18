using AffinityManager.Common;
using Privatest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AffinityManager.ViewModels
{
	internal sealed class CoreViewModel : BaseViewModel
	{
		[BackingField] private bool _isEnabled;
		public bool IsEnabled
		{
			get => _isEnabled;
			set => Set(ref _isEnabled, value);
		}

		public CoreViewModel()
		{
			ToggleCommand = new Command(Toggle);
		}

		public ICommand ToggleCommand { get; }
		private void Toggle()
		{
			IsEnabled = !IsEnabled;
		}
	}
}
