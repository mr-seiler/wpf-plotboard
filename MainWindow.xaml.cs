using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
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
    public enum MarkColors { RED, GREEN, BLUE, YELLOW, MAGENTA, GRAY };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<MarkColors, Brush> colorBrushes;
        
        public MainWindow()
        {
            InitializeComponent();

            setupHotkeys();

            this.colorBrushes = buildBrushDict();

            drawGridLines(GridCanvas, Brushes.LightGreen, 120, true, 10);
        }

        private Dictionary<MarkColors, Brush> buildBrushDict() 
        {
            Dictionary<MarkColors, Brush> brushes = new Dictionary<MarkColors, Brush>();

            brushes.Add(MarkColors.RED, (Brush)this.FindResource("brush-red"));
            brushes.Add(MarkColors.GREEN, (Brush)this.FindResource("brush-green"));
            brushes.Add(MarkColors.BLUE, (Brush)this.FindResource("brush-blue"));
            brushes.Add(MarkColors.YELLOW, (Brush)this.FindResource("brush-yellow"));
            brushes.Add(MarkColors.MAGENTA, (Brush)this.FindResource("brush-magenta"));
            brushes.Add(MarkColors.GRAY, (Brush)this.FindResource("brush-gray"));

            return brushes;
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


        /* Drag and drop markers */

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

        private void YellowMarkBtn_MouseMove(object sender, MouseEventArgs e)
        {
            MarkBtn_MouseMove(sender, e, MarkColors.YELLOW);
        }

        private void MagentaMarkBtn_MouseMove(object sender, MouseEventArgs e)
        {
            MarkBtn_MouseMove(sender, e, MarkColors.MAGENTA);
        }

        private void GrayMarkBtn_MouseMove(object sender, MouseEventArgs e)
        {
            MarkBtn_MouseMove(sender, e, MarkColors.GRAY);
        }

        // when a marker button in the sidebar is clicked and dragged:
        private void MarkBtn_MouseMove(object sender, MouseEventArgs e, PlottingBoard.MarkColors color)
        {
            WrapPanel btn = sender as WrapPanel;

            if (btn != null && e.LeftButton == MouseButtonState.Pressed)
            {
                // pack up the color we want to use...
                DataObject dragData = new DataObject("addMarker", color);
                // initialize the boogey
                DragDrop.DoDragDrop(btn, dragData, DragDropEffects.Copy);
            }

        }

        // when a marker in the marker canvas is clicked and dragged:
        private void Marker_MouseMove(object sender, MouseEventArgs e)
        {
            Ellipse mark = sender as Ellipse;

            if (mark != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject dragData = new DataObject("moveMarker", mark);
                DragDrop.DoDragDrop(mark, dragData, DragDropEffects.Move);
            }
        }

        private void MarkerArea_DragEnter(object sender, DragEventArgs e)
        {   
            // if the drag data isn't a marker color, then stop the drop
            if (!e.Data.GetDataPresent("addMarker") && !e.Data.GetDataPresent("moveMarker"))
            {
                e.Effects = DragDropEffects.None;
            }
            // can also check source to stop drag/drop onto self:
            // if (!e.Data.GetDataPresent("markColor") || sender == e.Source)
        }

        private void MarkerArea_Drop(object sender, DragEventArgs e)
        {
            // get position of drop relative to marker area (rectangle)
            Point where = e.GetPosition(MarkerArea);

            if (e.Data.GetDataPresent("addMarker"))
            {
                // casting to an enum.  delicious!
                MarkColors color = (MarkColors)e.Data.GetData("addMarker");

                // create marker and save in dictionary
                Ellipse m = buildMarker(color);
                markers.Add(m);

                // add as child of canvas
                MarkerArea.Children.Add(m);

                // set position on canvas
                MainWindow.setMarkerPosition(m, where);
            }
            else if (e.Data.GetDataPresent("moveMarker"))
            {
                Ellipse which = (Ellipse)e.Data.GetData("moveMarker");
                MainWindow.setMarkerPosition(which, where);
            }
            
        }

        private static void setMarkerPosition(Ellipse e, Point p)
        {
            Canvas.SetTop(e, p.Y - (e.Height / 2));
            Canvas.SetLeft(e, p.X - (e.Width / 2));
        }

        /* Keep all the markers here... There very well may be a better place for this. */
        private List<Ellipse> markers = new List<Ellipse>();

        private Ellipse buildMarker(MarkColors color)
        {
            Ellipse mark = new Ellipse();

            // set size
            mark.Width = mark.Height = 12;

            // set fill color
            mark.Fill = colorBrushes[color];

            // center mark on drop coordinates
            mark.RenderTransformOrigin = new Point(0.5, 0.5);

            // add event handler so that markers can be click-n-dragged
            mark.AddHandler(Ellipse.MouseMoveEvent, new MouseEventHandler(Marker_MouseMove));

            return mark;
        }

        private void ClearAllBtn_Click(object sender, RoutedEventArgs e)
        {
            MarkerArea.Children.Clear();
            this.markers.Clear();
        }


        /* Hotkeys and such */

        private void setupHotkeys()
        {
            RoutedCommand decreaseAngle = new RoutedCommand();
            decreaseAngle.InputGestures.Add(new KeyGesture(Key.Left));
            CommandBindings.Add(new CommandBinding(decreaseAngle, handle_decreaseAngle));

            RoutedCommand decreaseAngleMore = new RoutedCommand();
            decreaseAngleMore.InputGestures.Add(new KeyGesture(Key.Left, ModifierKeys.Shift));
            CommandBindings.Add(new CommandBinding(decreaseAngleMore, handle_decreaseAngleMore));

            RoutedCommand increaseAngle = new RoutedCommand();
            increaseAngle.InputGestures.Add(new KeyGesture(Key.Right));
            CommandBindings.Add(new CommandBinding(increaseAngle, handle_increaseAngle));

            RoutedCommand increaseAngleMore = new RoutedCommand();
            increaseAngleMore.InputGestures.Add(new KeyGesture(Key.Right, ModifierKeys.Shift));
            CommandBindings.Add(new CommandBinding(increaseAngleMore, handle_increaseAngleMore));
        }

        private void handle_decreaseAngle(object sender, ExecutedRoutedEventArgs e)
        {
            RotationControl rc = this.FindResource("rotationControl") as RotationControl;
            rc.AngleMill = rc.AngleMill - 1;
        }

        private void handle_decreaseAngleMore(object sender, ExecutedRoutedEventArgs e)
        {
            RotationControl rc = this.FindResource("rotationControl") as RotationControl;
            rc.AngleMill = rc.AngleMill - 20;
        }

        private void handle_increaseAngle(object sender, ExecutedRoutedEventArgs e)
        {
            RotationControl rc = this.FindResource("rotationControl") as RotationControl;
            rc.AngleMill = rc.AngleMill + 1;
        }

        private void handle_increaseAngleMore(object sender, ExecutedRoutedEventArgs e)
        {
            RotationControl rc = this.FindResource("rotationControl") as RotationControl;
            rc.AngleMill = rc.AngleMill + 20;
        }

        


        

    }

}
