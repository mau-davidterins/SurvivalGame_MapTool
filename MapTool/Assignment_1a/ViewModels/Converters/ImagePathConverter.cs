using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Assignment_1a.ViewModels.Converters
{
	class ImagePathConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if(value != null)
			{
				string val = (string)value;
				if (!string.IsNullOrEmpty(val))
				{
					return val;
				}
			}
			return "/Resources/NoImage.jpg";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
