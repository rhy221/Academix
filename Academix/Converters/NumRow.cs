using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Academix.Converters
{
    public class NumerRow : IValueConverter
    {
        public object Convert(object? value, Type TargetType, object? parameter, CultureInfo culture)
        {
            try
            {
                if (value is DataGridRow row)
                {
                    return (row.GetIndex() + 1).ToString();
                }
                else
                {
                    return "*";
                }
            }
            catch
            {
                return "*";
            }
        }

        public object ConvertBack(object? value, Type TargetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
