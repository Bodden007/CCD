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
    /// Логика взаимодействия для IoConfigCG.xaml
    /// </summary>
    public partial class IoConfigCG : Window
    {
        public IoConfigCG()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            IoConfig9B ioConfig9B = new IoConfig9B();
            ioConfig9B.Show();
            Close();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            IoConfigHSave ioConfigHSave = new IoConfigHSave();
            ioConfigHSave.Show();
            Close();
        }
    }
}
