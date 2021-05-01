using Privatest;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AffinityManager.Controls
{
	public sealed class DragAndDropPanel : Panel
	{
		private sealed record DragState(DragHandle Handle, UIElement Row, Point StartPosition)
		{
			public Point Position { get; init; } = StartPosition;
		}

		[BackingField] private DragState? _state;
		private DragState? State
		{
			get => _state;
			set
			{
				if (_state?.Handle != value?.Handle)
				{
					if (_state is not null)
					{
						_state.Handle.ReleaseMouseCapture();
						_state.Handle.MouseMove -= OnDrag;
					}

					if (value is not null)
					{
						value.Handle.CaptureMouse();
						value.Handle.MouseMove += OnDrag;
					}
				}

				if (_state?.Row != value?.Row)
				{
					_state?.Row?.SetValue(ZIndexProperty, 0);
					value?.Row?.SetValue(ZIndexProperty, 100);
				}

				_state = value;

				InvalidateArrange();
			}
		}

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
					child == State?.Row ? State.Position.X - State.StartPosition.X : 0,
					child == State?.Row ? top + State.Position.Y - State.StartPosition.Y : top,
					child.DesiredSize.Width,
					child.DesiredSize.Height);

				top += child.DesiredSize.Height;

				child.Arrange(rect);
			}

			return DesiredSize;
		}

		internal void StartDrag(DragHandle dragHandle)
		{
			var row = Children.OfType<UIElement>().First(row => row.IsAncestorOf(dragHandle));
			var mousePosition = Mouse.GetPosition(this);

			State = new DragState(dragHandle, row, mousePosition);
		}

		internal void StopDrag()
		{
			State = null;
		}

		private void OnDrag(object sender, MouseEventArgs e)
		{
			if (State == null) return;

			State = State with { Position = Mouse.GetPosition(this) };
		}
	}
}
