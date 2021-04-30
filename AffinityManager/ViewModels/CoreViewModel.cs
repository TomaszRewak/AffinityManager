using AffinityManager.Common;
using Privatest;
using System.Windows.Input;

namespace AffinityManager.ViewModels
{
	internal sealed class CoreViewModel : BaseViewModel
	{
		[BackingField] private int _coreNumber;
		public int CoreNumber
		{
			get => _coreNumber;
			set => Set(ref _coreNumber, value);
		}

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
