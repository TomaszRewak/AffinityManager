using System;
using System.Windows;
using System.Windows.Controls;

namespace AffinityManager.Controls
{
	internal sealed class DragAndDropPanel : Panel
	{
		protected override Size MeasureOverride(Size availableSize)
        {
            var rowSizeLimit = new Size(availableSize.Width, 0);
            var size = new Size();

            foreach (UIElement child in Children)
            {
                child.Measure(rowSizeLimit);

                size = new Size(
                    Math.Max(size.Width, child.DesiredSize.Width),
                    size.Height + child.DesiredSize.Height);
            }

            return size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var top = 0;

            foreach (UIElement child in Children)
            {
                var rect = new Rect(
                    0,
                    top,
                    child.DesiredSize.Width,
                    child.DesiredSize.Height);

                child.Arrange(rect);
            }

            return DesiredSize;
        }
    }
}
