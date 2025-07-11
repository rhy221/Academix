﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Academix.Converters
{
    public class DoubleToEmptyStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                if(doubleValue == -1 || doubleValue == Double.MinValue)
                    return string.Empty;
                
            }

            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value as string))
                return Double.MinValue;

            if (double.TryParse(value as string, out double result))
                return result;

            return 0;
        }
    }
}
