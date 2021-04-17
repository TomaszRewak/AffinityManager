using AffinityManager.ViewModels;
using AffinityManager.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AffinityManager
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var managerViewModel = new ManagerViewModel();
			var managerWindow = new ManagerWindow
			{
				DataContext = managerViewModel
			};

			managerWindow.Show();
			MainWindow = managerWindow;
		}
	}
}
