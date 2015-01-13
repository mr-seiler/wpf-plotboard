using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PlottingBoard
{
    /* Convert Mills to Degrees in a Data Binding
     * Mil = integer 0..6400
     * Deg = float 0.0..360.0
     * 360 Deg == 6400 Mill
     */
    public class MillToDegreeConverter : IValueConverter
    {
        // convert from mills (int 0..6400) to degrees (float 0.0..360.0)
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int mills = System.Convert.ToInt32(value);
            return (((double)mills) * 360 / 6400);

        }

        // convert from degrees to mills
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double deg = System.Convert.ToDouble(value);
            return (int)(deg * 6400 / 360);
        }
    }
}
