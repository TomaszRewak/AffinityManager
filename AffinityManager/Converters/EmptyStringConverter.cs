using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace AffinityManager.Converters
{
	internal sealed class EmptyStringConverter : MarkupExtension, IValueConverter
	{
		public object WhenEmpty { get; set; }
		public object WhenNotEmpty { get; set; }

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value switch
			{
				string text when text.Length > 0 => WhenNotEmpty,
				_ => WhenEmpty
			};
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
