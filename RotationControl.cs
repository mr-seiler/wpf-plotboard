using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace PlottingBoard
{
    /// <summary>
    /// Class with an "Angle" property for use in data binding.
    /// Multiple controls bind to this class, then the transform angle is read from here.
    /// 
    /// Although the angle of the actual image is controlled in degrees, the users will primarily being
    /// thinking and working in terms of mils.  So when a hotkey is pressed, for instance, it should rotate "n mils"
    /// not "n degrees", etc.  So, the actual angle value will be stored in mils.
    /// </summary>
    public class RotationControl : INotifyPropertyChanged
    {   
        // private backing data to the AngleMil property
        private int _angleMil = 0;

        /// <summary>
        /// Gets or sets the current angle measurement in Mill(s).
        /// If setting a value < 0 or > 6400, the stored value will be clamped to 0..6400.
        /// </summary>
        public int AngleMil
        {
            get { return this._angleMil; }
            set 
            {
                int oldVal = this._angleMil;
                this._angleMil = mod(value, 6400);
                if (this._angleMil != oldVal)
                {
                    OnPropertyChanged("AngleMil");
                    OnPropertyChanged("AngleDeg");
                }
            }
        }

        /// <summary>
        /// Gets or sets the current angle measurement in floating-point degrees, from 0.0..360.0
        /// If the angle is set < 0 or > 360, then it will be clamped inside 0..360.
        /// The value of this property is converted from/to a base value measured in mills.
        /// </summary>
        public double AngleDeg
        {
            // the get/set do not explicitly clamp the value inside 0..360, b/c the converter methods already do that.
            get { return milToDeg(this.AngleMil); }
            set { this.AngleMil = degToMil(value); }
        }

        internal static double milToDeg(int mils)
        {
            mils = mod(mils, 6400);
            return (((double)mils) * 360 / 6400);
        }

        internal static int degToMil(double deg)
        {
            deg = mod(deg, 360.0);
            return (int)Math.Round(deg * 6400 / 360);
        }

        // for wrapping values from max->min or min->max
        // a proper Euclidean modulus; the returned value will always be positive
        private static int mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        // there is no numeric base class, but doubles support the % op too, so here's an overload:
        private static double mod(double x, double m)
        {
            double r = x % m;
            return r < 0 ? r + m : r;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    /// <summary>
    /// For converting an angle measured in degrees to an angle in "mills" in a data binding
    /// </summary>
    public class DegToMilConverter : IValueConverter
    {
        // converts from degrees to mills
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return RotationControl.degToMil(System.Convert.ToDouble(value));
        }

        // converts from mills to degrees
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return RotationControl.milToDeg(System.Convert.ToInt32(value));
        }
    }

    /// <summary>
    /// For converting and angle measured in "mill" to an angle measured in degrees in a data binding
    /// </summary>
    public class MilToDegConverter : IValueConverter
    {
        // convert from mills (int 0..6400) to degrees (float 0.0..360.0)
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return RotationControl.milToDeg(System.Convert.ToInt32(value));
        }

        // convert from degrees to mills
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return RotationControl.degToMil(System.Convert.ToDouble(value));
        }
    }
}
