using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace PlottingBoard
{
    /// <summary>
    /// Class with a Scale property for UI elements to bind to;
    /// used to control the scale of the main window area and provide a basic form of "zoom"
    /// </summary>
    public class ScaleControl : INotifyPropertyChanged
    {

        private double _scaleDef = 0.9,
                       _scaleMin = 0.5,
                       _scaleMax = 2.5,
                       _scale = 0.9;

        /// <summary>
        /// Used to reset the scale to its starting value
        /// </summary>
        public double ScaleDefault
        {
            get
            {
                return this._scaleDef;
            }
            set
            {
                double oldVal = this._scaleDef;
                if (oldVal != this._scaleDef)
                {
                    OnPropertyChanged("ScaleDefault");
                }
            }
        }

        /// <summary>
        /// Provides a minimum value for validation and sliders and such
        /// </summary>
        public double ScaleMin
        {
            get
            {
                return this._scaleMin;
            }
            set
            {
                double oldVal = this._scaleMin;
                if (oldVal != this._scaleMin)
                {
                    OnPropertyChanged("ScaleMin");
                }
            }
        }

        /// <summary>
        /// Provides a maximum value for validation and sliders and such
        /// </summary>
        public double ScaleMax
        {
            get
            {
                return this._scaleMax;
            }
            set
            {
                double oldVal = this._scaleMax;
                if (oldVal != this._scaleMax)
                {
                    OnPropertyChanged("ScaleMax");
                }
            }
        }

        /// <summary>
        /// Scaling factor, relatively near to 1.0
        /// </summary>
        public double Scale
        {
            get
            {
                return this._scale;
            }
            set
            {
                double oldVal = this._scale;
                this._scale = clamp(value, this.ScaleMin, this.ScaleMax);
                if (oldVal != this._scale)
                {
                    OnPropertyChanged("Scale");
                }
            }
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

        private static double clamp(double val, double min, double max)
        {
            if (val < min)
            {
                return min;
            }
            else if (val > max)
            {
                return max;
            }
            else
            {
                return val;
            }
        }
    }
}
