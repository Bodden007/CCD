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
    /// Логика взаимодействия для BlendData.xaml
    /// </summary>
    public partial class BlendData : Window
    {
        public BlendData()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Quit1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Save1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Quit2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Quit3_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Quit4_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
