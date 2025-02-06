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
using Emgu;

namespace Projekt_Czesc1_PH
{
    /// <summary>
    /// Logika interakcji dla klasy ColorPickerWindow.xaml
    /// </summary>
    public partial class ColorPickerWindow : Window
    {
        private MainWindow mainWindow;

        double R_prim = 0.0;
        double G_prim = 0.0;
        double B_prim = 0.0;

        double m_max = 0.0;
        double m_min = 0.0;

        double delta = 0.0;

        double H = 0.0;
        double S = 0.0;
        double V = 0.0;
        public ColorPickerWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void allowOnlyNumbers (object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void min_max_RGB_value(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox) 
            { 
                if (int.TryParse(textBox.Text, out int value))
                {
                    if (value > 255)
                    {
                        textBox.Text = "255";
                        textBox.CaretIndex = textBox.Text.Length;
                    }
                }
                else
                {
                    textBox.Text = "0";
                    textBox.CaretIndex = textBox.Text.Length;
                }
            }
        }


        private void showColor_Click(object sender, RoutedEventArgs e)
        {
            R_prim = Convert.ToDouble(tbRGB_R.Text);
            G_prim = Convert.ToDouble(tbRGB_G.Text);
            B_prim = Convert.ToDouble(tbRGB_B.Text);

            m_max = Math.Max(R_prim, Math.Max(G_prim, B_prim));
            m_min = Math.Min(R_prim, Math.Min(G_prim, B_prim));

            delta = m_max - m_min;

            if (delta == 0)
            {
                H = 0;
            }
            else if (m_max == R_prim)
            {
                H = 60 * (((G_prim - B_prim) / delta) % 6);
            }
            else if (m_max == G_prim)
            {
                H = 60 * (((B_prim - R_prim) / delta) + 2);
            }
            else if (m_max == B_prim)
            {
                H = 60 * (((R_prim - G_prim) / delta) + 4);
            }

            if (m_max == 0)
            {
                S = 0;
            }
            else
            {
                S = delta / m_max;
            }

            V = m_max;

            H = Math.Round(H, 2);
            S = Math.Round(S, 2);
            V = Math.Round(V, 2);

            tbHSV_H.Text = H.ToString();
            tbHSV_S.Text = S.ToString();
            tbHSV_V.Text = V.ToString();


            colorPicked.Fill = new SolidColorBrush(Color.FromRgb(Convert.ToByte(R_prim), Convert.ToByte(G_prim), Convert.ToByte(B_prim)));
        }

        private void acceptColor_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.brushSelectedColor = new SolidColorBrush(Color.FromRgb(Convert.ToByte(tbRGB_R.Text), Convert.ToByte(tbRGB_G.Text), Convert.ToByte(tbRGB_B.Text)));
            mainWindow.colorPicker.Fill = new SolidColorBrush(Color.FromRgb(Convert.ToByte(tbRGB_R.Text), Convert.ToByte(tbRGB_G.Text), Convert.ToByte(tbRGB_B.Text)));
            Close();
        }
    }
}
