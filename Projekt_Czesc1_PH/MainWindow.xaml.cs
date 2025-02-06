using System;
using System.Collections.Generic;
using System.IO;
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
using Emgu;
using Microsoft.Win32;

namespace Projekt_Czesc1_PH
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point currentPoint = new Point();
        Point startPoint = new Point();
        Point endPoint = new Point();
        Line draw_line = new Line();
        Line selectedLine = null;
        int drawStyle = 1;

        Color selectedColor = Color.FromRgb(0, 0, 0);
        public Brush brushSelectedColor = new SolidColorBrush(Color.FromRgb(0, 0, 0));
        public MainWindow()
        {
            InitializeComponent();
        }

        private void paintSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed && drawStyle == 1)
            {
                Line line = new Line();
                line.Stroke = brushSelectedColor;
                line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(this).X;
                line.Y2 = e.GetPosition(this).Y;

                currentPoint = e.GetPosition(this);

                paintSurface.Children.Add(line);
            }

            if (e.LeftButton == MouseButtonState.Pressed && drawStyle == 3 && draw_line != null)
            {
                Point currentPosition = e.GetPosition(paintSurface);
                draw_line.X2 = currentPosition.X;
                draw_line.Y2 = currentPosition.Y;
            }

            if (drawStyle == 4 && selectedLine != null && e.LeftButton == MouseButtonState.Pressed)
            {
                Point currentPosition = e.GetPosition(paintSurface);

                if (Keyboard.IsKeyDown(Key.Q))
                {
                    selectedLine.X1 = currentPosition.X;
                    selectedLine.Y1 = currentPosition.Y;
                }
                else if (Keyboard.IsKeyDown(Key.E))
                {
                    selectedLine.X2 = currentPosition.X;
                    selectedLine.Y2 = currentPosition.Y;
                }
            }
        }

        private void paintSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(drawStyle == 2)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 6;
                ellipse.Height = 8;

                Canvas.SetTop(ellipse, e.GetPosition(this).Y - ellipse.Height / 2);
                Canvas.SetLeft(ellipse, e.GetPosition(this).X - ellipse.Width / 2);

                ellipse.Fill = SystemColors.WindowFrameBrush;

                paintSurface.Children.Add(ellipse);
            }

            if(drawStyle == 3)
            {
                startPoint = e.GetPosition(paintSurface);

                draw_line.X1 = startPoint.X;
                draw_line.Y1 = startPoint.Y;
                draw_line.Stroke = brushSelectedColor;
                draw_line.StrokeThickness = 3;


                if (draw_line.Parent != null)
                {
                    ((Canvas)draw_line.Parent).Children.Remove(draw_line);
                }

                paintSurface.Children.Add(draw_line);
            }

            if (drawStyle == 4)
            {
                Point clickPosition = e.GetPosition(paintSurface);

                foreach (UIElement element in paintSurface.Children)
                {
                    if (element is Line line)
                    {
                        double distanceToLine = DistanceToLine(line, clickPosition);
                        if (distanceToLine < 5)
                        {
                            selectedLine = line;
                            break;
                        }
                    }
                }
            }

            if (drawStyle == 7)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Width = 80;
                rectangle.Height = 60;

                Canvas.SetTop(rectangle, e.GetPosition(this).Y - rectangle.Height / 2);
                Canvas.SetLeft(rectangle, e.GetPosition(this).X - rectangle.Width / 2);

                rectangle.Stroke = brushSelectedColor;

                paintSurface.Children.Add(rectangle);
            }

            if(drawStyle == 8)
            {
                Polygon polygon = new Polygon();
                double mouseX = e.GetPosition(this).X;
                double mouseY = e.GetPosition(this).Y;

                double polygonSize = 20;

                Point point1 = new Point(mouseX - polygonSize, mouseY + 2*polygonSize);
                Point point2 = new Point(mouseX + polygonSize, mouseY + 2*polygonSize);
                Point point3 = new Point(mouseX + 2*polygonSize, mouseY + 0);
                Point point4 = new Point(mouseX + polygonSize, mouseY - 2*polygonSize);
                Point point5 = new Point(mouseX - polygonSize, mouseY - 2*polygonSize);
                Point point6 = new Point(mouseX - 2*polygonSize, mouseY + 0);

                PointCollection polygonPoints = new PointCollection();
                polygonPoints.Add(point1);
                polygonPoints.Add(point2);
                polygonPoints.Add(point3);
                polygonPoints.Add(point4);
                polygonPoints.Add(point5);
                polygonPoints.Add(point6);

                polygon.Points = polygonPoints;

                polygon.Stroke = brushSelectedColor;

                paintSurface.Children.Add(polygon);
            }

            if (drawStyle == 9)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Width = 125;
                ellipse.Height = 125;

                Canvas.SetTop(ellipse, e.GetPosition(this).Y - ellipse.Height / 2);
                Canvas.SetLeft(ellipse, e.GetPosition(this).X - ellipse.Width / 2);

                ellipse.Stroke = brushSelectedColor;

                paintSurface.Children.Add(ellipse);
            }

            if (drawStyle == 10)
            {
                if (startPoint.X == 0 && startPoint.Y == 0)
                {
                    startPoint = e.GetPosition(paintSurface);
                }
                else
                {
                    endPoint = e.GetPosition(paintSurface);
                    Line curvedLine = new Line();
                    curvedLine.X1 = startPoint.X;
                    curvedLine.Y1 = startPoint.Y;
                    curvedLine.X2 = endPoint.X;
                    curvedLine.Y2 = endPoint.Y;
                    curvedLine.Stroke = brushSelectedColor;
                    curvedLine.StrokeThickness = 3;

                    paintSurface.Children.Add(curvedLine);
                    startPoint = endPoint;
                    endPoint = new Point();
                }
            }

            if (drawStyle == 11)
            {
                Polygon korona = new Polygon();
                double x = e.GetPosition(this).X;
                double y = e.GetPosition(this).Y;

                PointCollection points = new PointCollection
                {
                    new Point(x, y - 50),
                    new Point(x - 40, y + 30),
                    new Point(x + 40, y + 30)
                };

                korona.Points = points;
                korona.Fill = new SolidColorBrush(Colors.Green);

                Rectangle pien = new Rectangle();
                pien.Width = 20;
                pien.Height = 30;
                pien.Fill = new SolidColorBrush(Colors.Brown);

                Canvas.SetLeft(pien, x - 10);
                Canvas.SetTop(pien, y + 30);


                paintSurface.Children.Add(korona);
                paintSurface.Children.Add(pien);
            }

            if (drawStyle == 12)
            {
                double x = e.GetPosition(this).X;
                double y = e.GetPosition(this).Y;

                Rectangle liniaPoziom = new Rectangle();

                liniaPoziom.Width = 60;
                liniaPoziom.Height = 10;
                liniaPoziom.Fill = brushSelectedColor;

                Canvas.SetLeft(liniaPoziom, x - 30);
                Canvas.SetTop(liniaPoziom, y - 5);

                Rectangle liniaPion = new Rectangle();
                liniaPion.Width = 10;
                liniaPion.Height = 60;
                liniaPion.Fill = brushSelectedColor;

                Canvas.SetLeft(liniaPion, x - 5);
                Canvas.SetTop(liniaPion, y - 30);

                paintSurface.Children.Add(liniaPoziom);
                paintSurface.Children.Add(liniaPion);
            }

            if (drawStyle == 13)
            {
                double x = e.GetPosition(this).X;
                double y = e.GetPosition(this).Y;

                Ellipse wisnia = new Ellipse();

                wisnia.Width = 30;
                wisnia.Height = 30;
                wisnia.Fill = new SolidColorBrush(Colors.Red);

                Canvas.SetLeft(wisnia, x - 20);
                Canvas.SetTop(wisnia, y);

                Line lodyga = new Line();
                lodyga.X1 = x - 5;
                lodyga.Y1 = y;
                lodyga.X2 = x - 10;
                lodyga.Y2 = y - 30;
                lodyga.Stroke = new SolidColorBrush(Colors.Green);
                lodyga.StrokeThickness = 2;


                paintSurface.Children.Add(wisnia);
                paintSurface.Children.Add(lodyga);
            }

            if (drawStyle == 14)
            {
                Ellipse sniezka = new Ellipse
                {
                    Width = 50,
                    Height = 50,
                    Fill = new LinearGradientBrush
                    {
                        GradientStops = new GradientStopCollection
                        {
                            new GradientStop(Colors.White, 0.0),
                            new GradientStop(Colors.LightGray, 0.5),
                            new GradientStop(Colors.Gray, 0.75),
                            new GradientStop(Colors.Black, 1.0)
                        },
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(1, 1)
                    },
                Stroke = new LinearGradientBrush
                {
                    GradientStops = new GradientStopCollection
                        {
                            new GradientStop(Colors.White, 0.0),
                            new GradientStop(Colors.LightGray, 0.5),
                            new GradientStop(Colors.Gray, 0.75),
                            new GradientStop(Colors.Black, 1.0)
                        },
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(1, 1)
                },
                    StrokeThickness = 2
                };

                Canvas.SetLeft(sniezka, e.GetPosition(this).X - 25);
                Canvas.SetTop(sniezka, e.GetPosition(this).Y - 25);

                paintSurface.Children.Add(sniezka);
            }

            if (drawStyle == 15)
            {
                var clickedElement = e.Source as FrameworkElement;

                if(clickedElement != null)
                {
                    if (paintSurface.Children.Contains(clickedElement))
                    {
                        paintSurface.Children.Remove(clickedElement);
                    }
                }
            }
        }

        private void paintSurface_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ButtonState == MouseButtonState.Pressed){
                currentPoint = e.GetPosition(this);
            }
        }

        private void point_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 2;
        }

        private void default_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 1;
        }

        private void paintSurface_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (drawStyle == 3 && draw_line != null)
            {
                endPoint = e.GetPosition(paintSurface);
                draw_line.X2 = endPoint.X;
                draw_line.Y2 = endPoint.Y;

                draw_line = new Line();
                startPoint = e.GetPosition(paintSurface);
                endPoint = new Point();
            }

            if (drawStyle == 4 && selectedLine != null)
            {
                selectedLine = null;
            }
        }

        private void drawSegment_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 3;
        }

        private void editSegment_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 4;
        }

        private void drawPolygon_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 7;
        }

        private void drawRectangle_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 8;
        }

        private void drawCircle_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 9;
        }

        private void drawCurvedLine_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 10;
            startPoint = new Point();
            endPoint = new Point();
        }

        private void drawTree_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 11;
        }

        private void drawPlus_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 12;
        }

        private void drawCherry_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 13;
        }

        private void drawSnowball_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 14;
        }

        private double DistanceToLine(Line line, Point point)
        {
            double A = line.Y2 - line.Y1;
            double B = line.X1 - line.X2;
            double C = line.X2 * line.Y1 - line.X1 * line.Y2;

            double distance = Math.Abs(A * point.X + B * point.Y + C) / Math.Sqrt(A * A + B * B);
            return distance;
        }

        private void colorPicker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorPickerWindow  colorPickerWindow = new ColorPickerWindow(this);
            colorPickerWindow.Show();
        }



        private void filterImage_Click(object sender, RoutedEventArgs e)
        {
            EmguFilters emguFiltersWindow = new EmguFilters();
            emguFiltersWindow.Show();
        }

        private void filterMatrix_Click(object sender, RoutedEventArgs e)
        {
            ImageFilter imageFilterWindow = new ImageFilter();
            imageFilterWindow.Show();
        }


        public void SaveToPngFile(Uri path, Canvas canvas)
        {
            if (path is null)
            {
                return;
            }

            Transform transform = canvas.LayoutTransform;
            canvas.LayoutTransform = null;

            Size size = new Size(canvas.ActualWidth, canvas.ActualHeight);

            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96d, 96d, PixelFormats.Pbgra32);

            renderTargetBitmap.Render(canvas);

            using (FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                encoder.Save(outStream);
            }

            canvas.LayoutTransform = transform;
        }

        public void SaveToJpegFile(Uri path, Canvas canvas)
        {
            if (path is null)
            {
                return;
            }

            Transform transform = canvas.LayoutTransform;
            canvas.LayoutTransform = null;

            Size size = new Size(canvas.ActualWidth, canvas.ActualHeight);

            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96d, 96d, PixelFormats.Pbgra32);

            renderTargetBitmap.Render(canvas);

            using (FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();

                encoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                encoder.Save(outStream);
            }

            canvas.LayoutTransform = transform;
        }

        private void file_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image File (*.png)|*.png|" + "JPEG (*.jpeg)|*.jpeg";
            saveFileDialog.FilterIndex = 1;
            Console.WriteLine(saveFileDialog.FileName);
            if (saveFileDialog.ShowDialog() == true)
            {
                Uri newFileUri = new Uri(saveFileDialog.FileName);
                if (newFileUri.ToString().Contains(".png"))
                {
                    SaveToPngFile(newFileUri, paintSurface);
                }
                else
                {
                    SaveToJpegFile(newFileUri, paintSurface);
                }

            }
        }

        private void eraser_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = 15;
        }

        private void clearCanvas_Click(object sender, RoutedEventArgs e)
        {
            paintSurface.Children.Clear();
        }
    }


}
