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

namespace CCD.IOConfig
{
    /// <summary>
    /// Логика взаимодействия для IoConfig58.xaml
    /// </summary>
    public partial class IoConfig58 : Window
    {
        public IoConfig58()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            CCD.IOConfig.IoConfig14 ioConfig14 = new CCD.IOConfig.IoConfig14();
            ioConfig14.Show();
            Close();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            CCD.IOConfig.IoConfig9B ioConfig9B = new IoConfig9B();
            ioConfig9B.Show();
            Close();

        }
    }
}
