using Privatest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	public partial class DragHandle : UserControl
	{
		[This(nameof(Container))] private static DependencyProperty ContainerProperty = DependencyProperty.Register(nameof(Container), typeof(StackPanel), typeof(DragHandle));
		public StackPanel Container
		{
			get => GetValue(ContainerProperty) as StackPanel;
			set => SetValue(ContainerProperty, value);
		}

		[This(nameof(Row))] private static DependencyProperty RowProperty = DependencyProperty.Register(nameof(Row), typeof(UserControl), typeof(DragHandle));
		public UserControl Row
		{
			get => GetValue(RowProperty) as UserControl;
			set => SetValue(RowProperty, value);
		}

		public DragHandle()
		{
			InitializeComponent();
		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);

			CaptureMouse();
		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			base.OnMouseUp(e);

			ReleaseMouseCapture();
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			var mousePosition = e.GetPosition(Container);

			for (int i = 0; i < Container.Children.Count; i++)
			{

				var child = Container.Children[i];
				var childPosition = child.TranslatePoint(new Point(), Container).Y;
				var childSize = child.DesiredSize.Height;

				if (child.IsAncestorOf(Row))
				{
					Trace.WriteLine(i);
				}
			}
		}
	}
}
