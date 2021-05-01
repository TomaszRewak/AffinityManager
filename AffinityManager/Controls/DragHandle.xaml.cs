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
		private static DependencyProperty ContainerProperty = DependencyProperty.Register(nameof(Container), typeof(DragAndDropPanel), typeof(DragHandle));
		public DragAndDropPanel Container
		{
			get => GetValue(ContainerProperty) as DragAndDropPanel;
			set => SetValue(ContainerProperty, value);
		}

		private static DependencyProperty RowProperty = DependencyProperty.Register(nameof(Row), typeof(UserControl), typeof(DragHandle));
		public UserControl Row
		{
			get => GetValue(RowProperty) as UserControl;
			set => SetValue(RowProperty, value);
		}

		public DragHandle()
		{
			InitializeComponent();

			//SetBinding(ContainerProperty, new Binding
			//{
			//	RelativeSource = new RelativeSource
			//	{
			//		Mode = RelativeSourceMode.FindAncestor,
			//		AncestorType = typeof(DragAndDropPanel),
			//		AncestorLevel = 1
			//	}
			//});
		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);

			Container.StartDrag(this);
		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			base.OnMouseUp(e);

			Container.StopDrag();
		}
	}
}
