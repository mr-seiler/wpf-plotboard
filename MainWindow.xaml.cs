using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlottingBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            drawGridLines(GridCanvas, Brushes.LightGreen, 120, true, 10);

        }

        private void drawGridLines(Canvas canvas, Brush brush, int numLines, bool indexLines, int indexInterval)
        {
            // alias canvas size
            double w = canvas.Width;
            double h = canvas.Height;

            // figure the space between each line, using the smaller of the two canvas dimensions
            double gap = Math.Min(w, h) / numLines;
            
            // draw vertical lines
            double y1 = 0, y2 = h;
            // we don't know if the canvas is square; figure out the number of lines to draw based on the width
            int actualLineCount = (int)Math.Round(w / gap);

            Console.WriteLine(String.Format("{0} x {1}, asked for {3} getting {4} (gap of {2})", w, h, gap, numLines, actualLineCount));

            for (int i = 0; i <= actualLineCount; i += 1)
            {
                double x = i * gap;
                Line l = new Line();
                l.X1 = x;
                l.X2 = x;
                l.Y1 = y1;
                l.Y2 = y2;
                l.Stroke = brush;
                l.StrokeThickness = 1;
                if (indexLines && i % indexInterval == 0)
                {
                    l.StrokeThickness = 2;
                }
                canvas.Children.Add(l);
            }

            // draw horizontal lines
            double x1 = 0, x2 = w;
            // again, since we don't know if the canvas is square...
            actualLineCount = (int)Math.Round(h / gap);

            for (int i = 0; i <= actualLineCount; i += 1)
            {
                double y = i * gap;
                Line l = new Line();
                l.X1 = x1;
                l.X2 = x2;
                l.Y1 = y;
                l.Y2 = y;
                l.Stroke = brush;
                l.StrokeThickness = (indexLines && i % indexInterval == 0 ? 2 : 1);
                canvas.Children.Add(l);
            }

        }

    }

}
