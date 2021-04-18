﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AffinityManager.ViewModels
{
	internal abstract class BaseViewModel : INotifyPropertyChanged
	{
		protected void Set<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
		{
			if (Equals(field, value)) return;

			field = value;

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
