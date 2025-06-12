using CCD.Models.ModelsForms;
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
    /// Логика взаимодействия для ManualControl.xaml
    /// </summary>
    public partial class ManualControl : Window
    {
        private readonly ManualControlViewModel _viewModel;
        public ManualControl()
        {
            InitializeComponent();
           
            _viewModel = new ManualControlViewModel();

            DataContext = _viewModel;

            Loaded += PassSideWindow_Loaded;
            Closed += PassSideWindow_Closed;

            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;

            this.KeyDown += MainWindow_KeyDown;
        }

        private async void PassSideWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.StartPollingAsync();
        }

        private async void PassSideWindow_Closed(object sender, EventArgs e)
        {
            _viewModel?.StopPolling();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();

                // Если нужно открыть модально (как диалог):
                // newWindow.ShowDialog();
            }
        }

        private void AllValvesExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
