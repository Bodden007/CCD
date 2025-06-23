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
    /// Логика взаимодействия для Combined.xaml
    /// </summary>
    public partial class Combined : Window
    {
        public Combined()
        {
            InitializeComponent();

            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;

            this.KeyDown += Combined_KeyDown;
        }

        private void Combined_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Close();
                    break;
            }
        }

        private void EXIT_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
