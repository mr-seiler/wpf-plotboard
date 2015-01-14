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
    public enum MarkColors { RED, GREEN, BLUE };

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

        /* seperate mousemove handlers, so we can tell which button is which without hacks or a custom compund control (extending button) */

        private void RedMarkBtn_MouseMove(object sender, MouseEventArgs e)
        {
            MarkBtn_MouseMove(sender, e, MarkColors.RED);
        }

        private void GreenMarkBtn_MouseMove(object sender, MouseEventArgs e)
        {
            MarkBtn_MouseMove(sender, e, MarkColors.GREEN);
        }

        private void BlueMarkBtn_MouseMove(object sender, MouseEventArgs e)
        {
            MarkBtn_MouseMove(sender, e, MarkColors.BLUE);
        }

        private void MarkBtn_MouseMove(object sender, MouseEventArgs e, PlottingBoard.MarkColors color)
        {
            WrapPanel btn = sender as WrapPanel;

            Console.WriteLine("In MouseMove, " + BlueMarkBtn.ToString() + " " + e.LeftButton);

            if (btn != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Console.WriteLine("DODRAGDROP");

                // pack up the color we want to use...
                DataObject dragData = new DataObject("markColor", color);
                // initialize the boogey
                DragDrop.DoDragDrop(btn, dragData, DragDropEffects.Copy);
            }

        }

        private void MarkerArea_DragEnter(object sender, DragEventArgs e)
        {
            Console.WriteLine("DragEnter");
            
            // if the drag data isn't a marker color, then stop the drop
            if (!e.Data.GetDataPresent("markColor"))
            {
                Console.WriteLine("Setting effect to none");
                e.Effects = DragDropEffects.None;
            }
            // can also check source to stop drag/drop onto self:
            // if (!e.Data.GetDataPresent("markColor") || sender == e.Source)
        }

        private void MarkerArea_Drop(object sender, DragEventArgs e)
        {
            Console.WriteLine("In Drop");

            // casting to an enum.  delicious!
            MarkColors color = (MarkColors)e.Data.GetData("markColor");

            // get position of drop relative to marker area (rectangle)
            Point where = e.GetPosition(MarkerArea);

            // we know the color (sort of), and we know where to put it.
            // create a new ellipse (how?)
            // add it to a dictionary, so we can keep track of it.
            // add it as a child of the rectangle.  maybe.
            Ellipse m = buildMarker(color);
            markers.Add(m);

            MarkerArea.Children.Add(m);
            Canvas.SetTop(m, where.Y - (m.Height / 2));
            Canvas.SetLeft(m, where.X - (m.Width / 2));
        }

        private List<Ellipse> markers = new List<Ellipse>();

        private static Ellipse buildMarker(MarkColors color)
        {
            Ellipse mark = new Ellipse();

            mark.Width = 20;
            mark.Height = 20;

            switch (color)
            {
                case MarkColors.BLUE:
                    mark.Fill = Brushes.Blue;
                    break;
                case MarkColors.GREEN:
                    mark.Fill = Brushes.Green;
                    break;
                case MarkColors.RED:
                    mark.Fill = Brushes.Red;
                    break;
                default:
                    mark.Fill = Brushes.Gray;
                    break;
            }

            mark.RenderTransformOrigin = new Point(0.5, 0.5);

            return mark;
        }

    }

}
