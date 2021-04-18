using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace AffinityManager.Converters
{
	internal sealed class BooleanConverter : MarkupExtension, IValueConverter
	{
		public object WhenTrue { get; set; }
		public object WhenFalse { get; set; }

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is true
				? WhenTrue
				: WhenFalse;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
