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
		private sealed record DragState(DragHandle Handle, UIElement Row, Point StartPosition, Vector MouseOffset)
		{
			public Point Position { get; init; } = StartPosition;
		}

		public sealed class DragEventArgs : EventArgs
		{
			public int StartIndex { get; init; }
			public int EndIndex { get; init; }
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
				switch (State)
				{
					case { Row: var row, Position: var position, MouseOffset: var mouseOffset } when row == child:
						row.Arrange(new Rect(position - mouseOffset, new Size(DesiredSize.Width, row.DesiredSize.Height)));
						continue;
					case { Row: var row, Position: { Y: var y } } when y >= top && y < top + child.DesiredSize.Height:
						top += row.DesiredSize.Height;
						break;
				}

				child.Arrange(new Rect(0, top, DesiredSize.Width, child.DesiredSize.Height));

				top += child.DesiredSize.Height;
			}

			return DesiredSize;
		}

		internal void StartDrag(DragHandle dragHandle)
		{
			var row = Children.OfType<UIElement>().First(row => row.IsAncestorOf(dragHandle));
			var mousePosition = Mouse.GetPosition(this);
			var mouseOffset = Mouse.GetPosition(row);

			State = new DragState(dragHandle, row, mousePosition, new Vector(mouseOffset.X, mouseOffset.Y));
		}

		internal void StopDrag()
		{
			if (State == null) return;

			var startIndex = 0 as int?;
			var endIndex = 0 as int?;

			var top = 0.0;
			var index = 0;

			foreach (UIElement child in Children)
			{
				if (State.Row == child)
					startIndex = index;

				if (State.Position.Y >= top && State.Position.Y < top + child.DesiredSize.Height)
					endIndex = index;

				top += child.DesiredSize.Height;
				index++;
			}

			State = null;

			if (startIndex.HasValue && endIndex.HasValue && startIndex != endIndex)
				ElementDragged?.Invoke(this, new DragEventArgs { StartIndex = startIndex.Value, EndIndex = endIndex.Value });
		}

		private void OnDrag(object sender, MouseEventArgs e)
		{
			if (State == null) return;

			var mousePosition = Mouse.GetPosition(this);
			var clippedPosition = new Point(
				Math.Clamp(mousePosition.X, State.MouseOffset.X, DesiredSize.Width - State.MouseOffset.X),
				Math.Clamp(mousePosition.Y, State.MouseOffset.Y, DesiredSize.Height - State.Row.DesiredSize.Height + State.MouseOffset.Y));

			State = State with { Position = clippedPosition };
		}

		public event EventHandler<DragEventArgs>? ElementDragged;
	}
}
