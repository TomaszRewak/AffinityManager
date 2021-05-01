using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AffinityManager.Controls
{
	public sealed class DragAndDropPanel : Panel
	{
		private DragHandle _dragHandle;
		private UIElement _dragRow;
		private Point _dragStartPosition;
		private Point _dragPosition;

		public void Tets()
		{
			InvalidateArrange();
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			var size = new Size();

			foreach (UIElement child in Children)
			{
				child.Measure(availableSize);

				size = new Size(
					Math.Max(size.Width, child.DesiredSize.Width),
					size.Height + child.DesiredSize.Height);
			}

			return size;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			var top = 0.0;

			foreach (UIElement child in Children)
			{
				var rect = new Rect(
					child == _dragRow ? _dragPosition.X - _dragStartPosition.X : 0,
					child == _dragRow ? top + _dragPosition.Y - _dragStartPosition.Y : top,
					child.DesiredSize.Width,
					child.DesiredSize.Height);

				top += child.DesiredSize.Height;

				child.Arrange(rect);
			}

			return DesiredSize;
		}

		internal void StartDrag(DragHandle dragHandle)
		{
			_dragHandle = dragHandle;
			_dragHandle.CaptureMouse();
			_dragHandle.MouseMove += OnDrag;

			_dragRow = Children.OfType<UIElement>().First(row => row.IsAncestorOf(dragHandle));
			_dragRow.SetValue(ZIndexProperty, 100);

			_dragStartPosition = Mouse.GetPosition(this);
			_dragPosition = _dragStartPosition;
		}

		internal void StopDrag()
		{
			_dragHandle.MouseMove -= OnDrag;
			_dragHandle.ReleaseMouseCapture();
			_dragHandle = null;

			_dragRow.SetValue(ZIndexProperty, 0);
			_dragRow = null;

			_dragStartPosition = new Point();
			_dragPosition = new Point();

			InvalidateArrange();
		}

		private void OnDrag(object sender, System.Windows.Input.MouseEventArgs e)
		{
			_dragPosition = Mouse.GetPosition(this);

			InvalidateArrange();
		}
	}
}
