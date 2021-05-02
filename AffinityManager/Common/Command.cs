using System;
using System.Windows.Input;

namespace AffinityManager.Common
{
	internal sealed class Command : ICommand
	{
		private readonly Action _action;

		public Command(Action action)
		{
			_action = action;
		}

		public bool CanExecute(object? parameter)
		{
			return true;
		}

		public void Execute(object? parameter)
		{
			_action();
		}

		public event EventHandler? CanExecuteChanged;
	}

	internal sealed class Command<T> : ICommand
	{
		private readonly Action<T> _action;

		public Command(Action<T> action)
		{
			_action = action;
		}

		public bool CanExecute(object? parameter)
		{
			return parameter is T;
		}

		public void Execute(object? parameter)
		{
			if (parameter is T value)
				_action(value);
		}

		public event EventHandler? CanExecuteChanged;
	}

	[AttributeUsage(AttributeTargets.Property)]
	sealed class CommandGetterAttribute : Attribute
	{ }
}
