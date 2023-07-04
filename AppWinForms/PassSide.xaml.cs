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
    /// Логика взаимодействия для PassSide.xaml
    /// </summary>
    public partial class PassSide : Window
    {
        public PassSide()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
        }
        private void SetPSZero_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearSets_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetDSZero_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
