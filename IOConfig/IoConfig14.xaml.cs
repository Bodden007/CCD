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
    /// Логика взаимодействия для IoConfig14.xaml
    /// </summary>
    public partial class IoConfig14 : Window
    {
        public IoConfig14()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            CCD.IOConfig.IoConfig58 ioConfig58 = new CCD.IOConfig.IoConfig58();
            ioConfig58.Show();
            Close();

        }
    }
}
