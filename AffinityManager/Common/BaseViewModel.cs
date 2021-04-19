using System;
using System.ComponentModel;

namespace AffinityManager.ViewModels
{
	internal abstract class BaseViewModel : INotifyPropertyChanged
	{
		protected ViewModelChange Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
		{
			if (Equals(field, value))
				return new ViewModelChange(false);

			field = value;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));

			return new ViewModelChange(true);
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}

	internal sealed record ViewModelChange(bool Changed)
	{
		public ViewModelChange Then(Action action)
		{
			if (Changed)
				action();

			return this;
		}
	}
}
