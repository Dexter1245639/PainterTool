using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
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
using System.Windows.Shapes;
using Emgu.CV;
using Microsoft.Win32;

namespace Projekt_Czesc1_PH
{
    /// <summary>
    /// Logika interakcji dla klasy EmguFilters.xaml
    /// </summary>
    public partial class EmguFilters : Window
    {
        string selectedImagePath="";

        public EmguFilters()
        {
            InitializeComponent();

            resetImage.IsEnabled = false;
            sobelFilter.IsEnabled = false;
            GuassianBlur.IsEnabled = false;
            CannyEdge.IsEnabled = false;
            BlackWhite.IsEnabled = false;
            ColorSharpness.IsEnabled = false;
        }

        private void loadImage_Click(object sender, RoutedEventArgs e)
        {
            selectedImagePath = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
            openFileDialog.ShowDialog();

            selectedImagePath = openFileDialog.FileName;

            var imagePath = selectedImagePath;
            if (!string.IsNullOrEmpty(imagePath))
            {
                var srcImage = new Mat(imagePath);
                DisplayImage(srcImage, 500);
            }

            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                resetImage.IsEnabled = true;
                sobelFilter.IsEnabled = true;
                GuassianBlur.IsEnabled = true;
                CannyEdge.IsEnabled = true;
                BlackWhite.IsEnabled = true;
                ColorSharpness.IsEnabled = true;
            }
            else
            {
                resetImage.IsEnabled = false;
                sobelFilter.IsEnabled = false;
                GuassianBlur.IsEnabled = false;
                CannyEdge.IsEnabled = false;
                BlackWhite.IsEnabled = false;
                ColorSharpness.IsEnabled = false;
            }
        }

        private void resetImage_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = selectedImagePath;
            var srcImage = new Mat(imagePath, Emgu.CV.CvEnum.ImreadModes.Color);

            imageSurface.Children.Clear();
            imageSurface.Background = new SolidColorBrush(Colors.Black);
            DisplayImage(srcImage, 500);
        }

        private void sobelFilter_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = selectedImagePath;
            var srcImage = new Mat(imagePath, Emgu.CV.CvEnum.ImreadModes.Grayscale);

            var sobelImage = new Mat();
            CvInvoke.Sobel(srcImage, sobelImage, Emgu.CV.CvEnum.DepthType.Cv8U, 1, 0, 3);


            DisplayImage(sobelImage, 500);
        }

        private void GuassianBlur_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = selectedImagePath;
            var srcImage = new Mat(imagePath, Emgu.CV.CvEnum.ImreadModes.Color);

            var blurredImage = new Mat();
            CvInvoke.GaussianBlur(srcImage, blurredImage, new System.Drawing.Size(5, 5), 10);

            DisplayImage(blurredImage, 500);
        }

        private void CannyEdge_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = selectedImagePath;
            var srcImage = new Mat(imagePath, Emgu.CV.CvEnum.ImreadModes.Color);

            var cannyImage = new Mat();
            CvInvoke.Canny(srcImage, cannyImage, 100, 200);

            DisplayImage(cannyImage, 500);
        }

        private void BlackWhite_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = selectedImagePath;
            var srcImage = new Mat(imagePath, Emgu.CV.CvEnum.ImreadModes.Grayscale);

            var thresholdBlackWhiteImage = new Mat();
            CvInvoke.Threshold(srcImage, thresholdBlackWhiteImage, 127, 255, Emgu.CV.CvEnum.ThresholdType.Binary);

            DisplayImage(thresholdBlackWhiteImage, 500);
        }

        private void ColorSharpness_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = selectedImagePath;
            var srcImage = new Mat(imagePath, Emgu.CV.CvEnum.ImreadModes.Color);

            var thresholdSharpeningImage = new Mat();
            CvInvoke.Threshold(srcImage, thresholdSharpeningImage, 127, 255, Emgu.CV.CvEnum.ThresholdType.Binary);

            DisplayImage(thresholdSharpeningImage, 500);
        }

        private void DisplayImage(Mat processedImage, double maxDimension)
        {
            using (var bitmap = BitmapExtension.ToBitmap(processedImage))
            {
                var bitmapImage = new BitmapImage();
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    stream.Seek(0, SeekOrigin.Begin);

                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = stream;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                }

                double scale = Math.Min(maxDimension / bitmapImage.PixelWidth, maxDimension / bitmapImage.PixelHeight);
                double scaledWidth = bitmapImage.PixelWidth * scale;
                double scaledHeight = bitmapImage.PixelHeight * scale;

                Image image = new Image
                {
                    Source = bitmapImage,
                    Width = scaledWidth,
                    Height = scaledHeight
                };

                double canvasCenterX = (imageSurface.ActualWidth - image.Width) / 2;
                double canvasCenterY = (imageSurface.ActualHeight - image.Height) / 2;

                Canvas.SetLeft(image, canvasCenterX);
                Canvas.SetTop(image, canvasCenterY);

                imageSurface.Background = new SolidColorBrush(Colors.White);
                imageSurface.Children.Clear();
                imageSurface.Children.Add(image);
            }
        }
    }
}
