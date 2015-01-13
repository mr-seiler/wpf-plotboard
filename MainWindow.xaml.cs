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
            drawGridLines();

        }

        private void drawGridLines()
        {
            int w = (int)GridCanvas.Width;
            int h = (int)GridCanvas.Height;

            Console.WriteLine(String.Format("Canvas size: {0} x {1}", w, h));

            Brush b = Brushes.LightGreen;
            
            // draw vertical lines
            int y1 = 0, y2 = h;
            int numLines = w / 10;
            Console.WriteLine(String.Format("Num of vertical lines: {0}", numLines));
            for (int i = 0; i < numLines; i += 1)
            {
                int x = i * 10;
                Line l = new Line();
                l.X1 = x;
                l.X2 = x;
                l.Y1 = y1;
                l.Y2 = y2;
                l.Stroke = b;
                l.StrokeThickness = (i % 10 == 0 ? 2 : 1);
                GridCanvas.Children.Add(l);
            }

            // draw horizontal lines
            int x1 = 0, x2 = w;
            numLines = h / 10;

            for (int i = 0; i < numLines; i += 1)
            {
                int y = i * 10;
                Line l = new Line();
                l.X1 = x1;
                l.X2 = x2;
                l.Y1 = y;
                l.Y2 = y;
                l.Stroke = b;
                l.StrokeThickness = (i % 10 == 0 ? 2 : 1);
                GridCanvas.Children.Add(l);
            }

        }

    }

}
