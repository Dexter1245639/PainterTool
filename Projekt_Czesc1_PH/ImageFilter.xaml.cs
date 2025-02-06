using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logika interakcji dla klasy ImageFilter.xaml
    /// </summary>
    public partial class ImageFilter : Window
    {
        string selectedImagePath = "";
        public ImageFilter()
        {
            InitializeComponent();
            resetImage.IsEnabled = false;
            filterImage.IsEnabled = false;
        }

        private void allowOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[-]+[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void min_max_value(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (int.TryParse(textBox.Text, out int value) || textBox.Text == "-")
                {
                    if (value >= 10 || value <= -10)
                    {
                        if(value >= 10)
                        {
                            textBox.Text = "9";
                            textBox.CaretIndex = textBox.Text.Length;
                        }
                        else if (value <= -10)
                        {
                            textBox.Text = "-9";
                            textBox.CaretIndex = textBox.Text.Length;
                        }
                    }
                    else if (textBox.Text == "-0")
                    {
                        textBox.Text = "0";
                        textBox.CaretIndex = textBox.Text.Length;
                    }
                }
                else
                {
                    textBox.Text = "1";
                    textBox.CaretIndex = textBox.Text.Length;
                }
            }
        }

        private void matrixLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                    textBox.Text = "1";
            }
        }

        private void loadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
            openFileDialog.ShowDialog();

            selectedImagePath = openFileDialog.FileName;

            var imagePath = selectedImagePath;

            if (!string.IsNullOrEmpty(imagePath))
            {
                var srcImage = new Mat(imagePath);
                DisplayImage(srcImage, 400);
            }

            if (selectedImagePath != "")
            {
                resetImage.IsEnabled = true;
                filterImage.IsEnabled = true;
            }
        }

        private void resetImage_Click(object sender, RoutedEventArgs e)
        {
            var imagePath = selectedImagePath;
            var srcImage = new Mat(imagePath);

            imageSurface.Children.Clear();
            imageSurface.Background = new SolidColorBrush(Colors.Black);
            DisplayImage(srcImage, 400);
        }

        private void filterImage_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedImagePath) && 
                (!string.IsNullOrEmpty(matrix00.Text) && !string.IsNullOrEmpty(matrix01.Text) && !string.IsNullOrEmpty(matrix02.Text) &&
                 !string.IsNullOrEmpty(matrix10.Text) && !string.IsNullOrEmpty(matrix11.Text) && !string.IsNullOrEmpty(matrix12.Text) &&
                 !string.IsNullOrEmpty(matrix20.Text) && !string.IsNullOrEmpty(matrix21.Text) && !string.IsNullOrEmpty(matrix22.Text)) 
               )
            {
                var srcImage = new Mat(selectedImagePath);

                int[,] kernel = new int[3, 3]
                {
                    {int.Parse(matrix00.Text), int.Parse(matrix01.Text), int.Parse(matrix02.Text)},
                    {int.Parse(matrix10.Text), int.Parse(matrix11.Text), int.Parse(matrix12.Text)},
                    {int.Parse(matrix20.Text), int.Parse(matrix21.Text), int.Parse(matrix22.Text)}
                };

                int kernelSum = kernel.Cast<int>().Sum();
                double _ = kernelSum == 0 ? 1 : kernelSum;

                var kernelMat = new Mat(3, 3, Emgu.CV.CvEnum.DepthType.Cv32F, 1);

                float[] kernelValues = new float[9]
                {
                    kernel[0, 0], kernel[0, 1], kernel[0, 2],
                    kernel[1, 0], kernel[1, 1], kernel[1, 2],
                    kernel[2, 0], kernel[2, 1], kernel[2, 2]
                };

                kernelMat.SetTo(kernelValues);


                var dstImage = new Mat(srcImage.Size, srcImage.Depth, srcImage.NumberOfChannels);
                CvInvoke.Filter2D(srcImage, dstImage, kernelMat, new System.Drawing.Point(-1, -1));


                DisplayImage(dstImage, 400);
            }   
        }



        private void DisplayImage(Mat processedImage, double maxDimension)
        {
            using (var bitmap = BitmapExtension.ToBitmap(processedImage))
            {
                var bitmapImage = new BitmapImage();
                using (var stream = new System.IO.MemoryStream())
                {
                    bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Seek(0, System.IO.SeekOrigin.Begin);

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
