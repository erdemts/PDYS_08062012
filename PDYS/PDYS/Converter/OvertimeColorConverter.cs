


using System.Windows.Data;
using System.Globalization;
using System;
using System.Drawing;

namespace PDYS.Converter
{
    public class OvertimeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is bool)
            {
                if (!((bool)value))
                {

                    return new SolidBrush(Color.Red);
                }
                return new SolidBrush(Color.Black);
            }

            return new SolidBrush(Color.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
