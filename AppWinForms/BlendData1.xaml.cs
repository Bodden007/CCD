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
using System.Windows.Shapes;

namespace CCD.AppWinForms
{
    /// <summary>
    /// Логика взаимодействия для BlendData1.xaml
    /// </summary>
    public partial class BlendData1 : Window
    {
        public BlendData1()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
        }

        private void Quit2_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Quit1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            CCD.AppWinForms.BlendData blendData = new CCD.AppWinForms.BlendData();
            blendData.Show();
            Close();
        }
    }
}
