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
    /// Логика взаимодействия для IoConfigHSave.xaml
    /// </summary>
    public partial class IoConfigHSave : Window
    {
        public IoConfigHSave()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            IoConfigCG ioConfigCG = new IoConfigCG();
            ioConfigCG.Show();
            Close();
        }

        private void SaveIoConfig_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
