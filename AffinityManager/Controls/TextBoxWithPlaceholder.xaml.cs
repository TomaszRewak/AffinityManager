using Privatest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AffinityManager.Controls
{
	/// <summary>
	/// Interaction logic for TextBoxWithPlaceholder.xaml
	/// </summary>
	public partial class TextBoxWithPlaceholder : UserControl
	{
		[This(nameof(Text))] private static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(TextBoxWithPlaceholder));
		public string Text
		{
			get => GetValue(TextProperty) as string;
			set => SetValue(TextProperty, value);
		}

		[This(nameof(Placeholder))] private static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(TextBoxWithPlaceholder));
		public string Placeholder
		{
			get => GetValue(PlaceholderProperty) as string;
			set => SetValue(PlaceholderProperty, value);
		}

		public TextBoxWithPlaceholder()
		{
			InitializeComponent();
		}
	}
}
